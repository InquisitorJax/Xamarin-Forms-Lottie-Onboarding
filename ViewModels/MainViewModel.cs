using Core;
using Prism.Commands;
using Prism.Events;
using SampleApplication.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SampleApplication
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IRepository _repository;

        private bool _listRefreshing;

        private bool _mainMenuOpen;
        private SubscriptionToken _modelUpdatedEventToken;

        private ObservableCollection<SampleItem> _sampleItems;

        private SampleItem _selectedSampleItem;

        public MainViewModel(IRepository repository)
        {
            _repository = repository;
            FetchSampleItemsCommand = new DelegateCommand(FetchSampleItems);
            OpenSelectedSampleItemCommand = new DelegateCommand(OpenSelectedSampleItemAsync);
            CreateSampleItemNavigationCommand = new DelegateCommand(CreateSampleItemNavigateAsync);
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

        public ICommand CreateSampleItemNavigationCommand { get; private set; }
        public ICommand FetchSampleItemsCommand { get; private set; }

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

        public ICommand OpenSelectedSampleItemCommand { get; private set; }

        public ObservableCollection<SampleItem> SampleItems
        {
            get { return _sampleItems; }
            set { SetProperty(ref _sampleItems, value); }
        }

        public SampleItem SelectedSampleItem
        {
            get { return _selectedSampleItem; }
            set { SetProperty(ref _selectedSampleItem, value); }
        }

        public string Title { get; set; }

        public override void Closing()
        {
            CC.EventMessenger.GetEvent<ModelUpdatedMessageEvent<SampleItem>>().Unsubscribe(_modelUpdatedEventToken);
        }

        public override async Task InitializeAsync(System.Collections.Generic.Dictionary<string, string> args)
        {
            _modelUpdatedEventToken = CC.EventMessenger.GetEvent<ModelUpdatedMessageEvent<SampleItem>>().Subscribe(OnSampleItemUpdated);
            await FetchSampleItemsAsync();
        }

        private async void CreateSampleItemNavigateAsync()
        {
            await Navigation.NavigateAsync(Constants.Navigation.ItemPage);
        }

        private async void FetchSampleItems()
        {
            await FetchSampleItemsAsync();
        }

        private async Task FetchSampleItemsAsync()
        {
            ListRefreshing = true;

            try
            {
                FetchModelCollectionResult<SampleItem> fetchResult = await _repository.FetchSampleItemsAsync();

                if (fetchResult.IsValid())
                {
                    SampleItems = fetchResult.ModelCollection.AsObservableCollection();

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

        private void OnSampleItemUpdated(ModelUpdatedMessageResult<SampleItem> updateResult)
        {
            SampleItems.UpdateCollection(updateResult.UpdatedModel, updateResult.UpdateEvent);
        }

        private async void OpenSelectedSampleItemAsync()
        {
            if (SelectedSampleItem != null)
            {
                Dictionary<string, string> args = new Dictionary<string, string>
                {
                    {Constants.Parameters.Id, SelectedSampleItem.Id}
                };

                await Navigation.NavigateAsync(Constants.Navigation.ItemPage, args);
            }
        }

        private async Task SignoutAsync()
        {
            await Navigation.NavigateAsync(Constants.Navigation.AuthPage, null, false, false, true);
        }
    }
}