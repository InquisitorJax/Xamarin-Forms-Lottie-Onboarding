using Autofac;
using Core;
using Prism.Commands;
using Prism.Events;
using SampleApplication.AppServices;
using SampleApplication.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SampleApplication
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IRepository _repository;

        private bool _hasActivities;
        private bool _listRefreshing;

        private bool _mainMenuOpen;
        private SubscriptionToken _modelUpdatedEventToken;

        private ObservableCollection<Contact> _recentContacts;

        private Contact _selectedSampleItem;

        private HighriseUser _user;

        public MainViewModel(IRepository repository)
        {
            _repository = repository;
            FetchContactsCommand = new DelegateCommand(FetchContacts);
            OpenSelectedContactCommand = new DelegateCommand<Contact>(OpenSelectedContactAsync);
            CreateContactNavigationCommand = new DelegateCommand(CreateSampleItemNavigateAsync);
            AddQuickNotesCommand = new DelegateCommand<Contact>(AddQuickNote);
            ShareCommand = new DelegateCommand(ShareHighrise);
            RemoveContactCommand = new DelegateCommand<Contact>(RemoveContact);
            MainMenuItemClickCommand = new DelegateCommand<MainMenuItem>(MainMenuItemClick);
            Title = "Xamarin Forms Onboarding with Lottie!";

            MainMenuItems = new List<MainMenuItem>();
            MainMenuItems.Add(new MainMenuItem
            {
                Title = "Contacts",
                IconSource = "ic_customer_dark.png",
                ActionId = Constants.Navigation.GreenLanternPage
            });
            MainMenuItems.Add(new MainMenuItem
            {
                Title = "Tasks",
                IconSource = "ic_tasks_dark.png",
                ActionId = Constants.Navigation.GreenLanternPage
            });
            MainMenuItems.Add(new MainMenuItem
            {
                Title = "Cases",
                IconSource = "ic_case_dark.png",
                ActionId = Constants.Navigation.GreenLanternPage
            });
            MainMenuItems.Add(new MainMenuItem
            {
                Title = "Deals",
                IconSource = "ic_deal_dark.png",
                ActionId = Constants.Navigation.GreenLanternPage
            });
            MainMenuItems.Add(new MainMenuItem
            {
                Title = "Search",
                IconSource = "ic_search_dark.png",
                ActionId = Constants.Navigation.GreenLanternPage
            });
            MainMenuItems.Add(new MainMenuItem
            {
                Title = "Settings",
                IconSource = "ic_settings_dark.png",
                ActionId = Constants.Navigation.GreenLanternPage
            });
            MainMenuItems.Add(new MainMenuItem
            {
                Title = "Help",
                IconSource = "ic_help_dark.png",
                ActionId = Constants.Navigation.GreenLanternPage
            });
            MainMenuItems.Add(new MainMenuItem
            {
                Title = "Sign Out",
                IconSource = "ic_sign_out_dark.png",
                ActionId = Constants.Navigation.SignOut
            });
        }

        public ICommand AddQuickNotesCommand { get; private set; }

        public ICommand CreateContactNavigationCommand { get; private set; }

        public HighriseUser CurrentUser
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public ICommand FetchContactsCommand { get; private set; }

        public bool HasContacts
        {
            get { return _hasActivities; }
            set { SetProperty(ref _hasActivities, value); }
        }

        public bool ListRefreshing
        {
            get { return _listRefreshing; }
            set { SetProperty(ref _listRefreshing, value); }
        }

        public ICommand MainMenuItemClickCommand { get; private set; }

        public IList<MainMenuItem> MainMenuItems { get; private set; }

        public bool MainMenuOpen
        {
            get { return _mainMenuOpen; }
            set { SetProperty(ref _mainMenuOpen, value); }
        }

        public ICommand OpenSelectedContactCommand { get; private set; }

        public ObservableCollection<Contact> RecentContacts
        {
            get { return _recentContacts; }
            set
            {
                if (_recentContacts != null)
                    _recentContacts.CollectionChanged -= RecentContacts_CollectionChanged;

                SetProperty(ref _recentContacts, value);

                if (_recentContacts != null)
                    _recentContacts.CollectionChanged += RecentContacts_CollectionChanged;

                CheckHasContacts();
            }
        }

        public ICommand RemoveContactCommand { get; private set; }

        public Contact SelectedContact
        {
            get { return _selectedSampleItem; }
            set { SetProperty(ref _selectedSampleItem, value); }
        }

        public ICommand ShareCommand { get; private set; }

        public string Title { get; set; }

        private IShareService ShareService
        {
            get { return CC.IoC.Resolve<IShareService>(); }
        }

        public override void Closing()
        {
            CC.EventMessenger.GetEvent<ModelUpdatedMessageEvent<Contact>>().Unsubscribe(_modelUpdatedEventToken);
        }

        public override async Task InitializeAsync(Dictionary<string, string> args)
        {
            _modelUpdatedEventToken = CC.EventMessenger.GetEvent<ModelUpdatedMessageEvent<Contact>>().Subscribe(OnContactUpdated);
            await FetchContactsAsync();

            CurrentUser = await _repository.FetchHighriseUserAsync();
        }

        private async void AddQuickNote(Contact contact)
        {
            UserPromptConfig prompt = new UserPromptConfig
            {
                Caption = "Add Contact Note",
                LabelText = "Contact Note",
                Message = "Add a note for this contact"
            };
            UserPromptResult promptResult = await UserNotifier.ShowPromptAsync(prompt);

            if (!promptResult.Cancelled)
            {
                contact.Notes += Environment.NewLine + promptResult.InputText;
                await _repository.SaveContactAsync(contact, updateEvent: ModelUpdateEvent.Updated);
            }
        }

        private void CheckHasContacts()
        {
            HasContacts = _recentContacts != null && _recentContacts.Count > 0;
        }

        private async void CreateSampleItemNavigateAsync()
        {
            await Navigation.NavigateAsync(Constants.Navigation.ContactPage);
        }

        private async void FetchContacts()
        {
            await FetchContactsAsync();
        }

        private async Task FetchContactsAsync()
        {
            ListRefreshing = true;

            try
            {
                FetchModelCollectionResult<Contact> fetchResult = await _repository.FetchContactsAsync();

                if (fetchResult.IsValid())
                {
                    RecentContacts = fetchResult.ModelCollection.AsObservableCollection();

                    ListRefreshing = false;
                }
                else
                {
                    ListRefreshing = false;
                    await CC.UserNotifier.ShowMessageAsync(fetchResult.Notification.ToString(), "Fetch Sample Items Failed");
                }
            }
            finally
            {
                ListRefreshing = false;
            }
        }

        private async void MainMenuItemClick(MainMenuItem menuItem)
        {
            MainMenuOpen = false;

            if (menuItem != null)
            {
                switch (menuItem.ActionId)
                {
                    case Constants.Navigation.SignOut:
                        await SignoutAsync();
                        break;
                }
            }
        }

        private void OnContactUpdated(ModelUpdatedMessageResult<Contact> updateResult)
        {
            RecentContacts.UpdateCollection(updateResult.UpdatedModel, updateResult.UpdateEvent);

            if (RecentContacts.Count == 1 && !CurrentUser.HasShownFirstContactAchievementPrompt)
            {
                ShowFirstContactPrompt();
            }
        }

        private async void OpenSelectedContactAsync(Contact contact)
        {
            SelectedContact = contact;
            if (SelectedContact != null)
            {
                Dictionary<string, string> args = new Dictionary<string, string>
                {
                    {Constants.Parameters.Id, SelectedContact.Id}
                };

                await Navigation.NavigateAsync(Constants.Navigation.ContactPage, args);
            }
        }

        private void RecentContacts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CheckHasContacts();
        }

        private async void RemoveContact(Contact contact)
        {
            var result = await UserNotifier.ShowConfirmAsync("Are your sure you want to delete this contact?", "delete contact", "yes");

            if (result)
            {
                var deleteResult = await _repository.DeleteContactAsync(contact);
                if (deleteResult.IsValid())
                {
                    RecentContacts.UpdateCollection(contact, ModelUpdateEvent.Deleted);
                }
            }
        }

        private void ShareHighrise()
        {
            ShareService.Share("Amazingly simple CRM - Try Highrise!", "Highrise CRM is awesome :)", Constants.ShareLinks.Highrise);
        }

        private void ShowFirstContactPrompt()
        {
            //UserNotifier.ShowToastAsync("Achievement unlocked! First contact made!!");
            Navigation.NavigateAsync(Constants.Navigation.FirstContactPromptPage, null, true);
        }

        private async Task SignoutAsync()
        {
            await Navigation.NavigateAsync(Constants.Navigation.AuthPage, null, false, false, true);
        }
    }
}