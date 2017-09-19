using Autofac;
using Core;
using Prism.Commands;
using SampleApplication.AppServices;
using System;
using System.Windows.Input;

namespace SampleApplication
{
    public class AuthViewModel : ViewModelBase
    {
        public AuthViewModel()
        {
            SignInCommand = new DelegateCommand(SignIn);
            ShowHelpCommand = new DelegateCommand(ShowHelp);
            ShowPrivacyCommand = new DelegateCommand(ShowPrivacy);
            ShowTermsCommand = new DelegateCommand(ShowTerms);
        }

        public ICommand ShowHelpCommand { get; private set; }

        public ICommand ShowPrivacyCommand { get; private set; }

        public ICommand ShowTermsCommand { get; private set; }

        public ICommand SignInCommand { get; private set; }

        private IShareService ShareService
        {
            get { return CC.IoC.Resolve<IShareService>(); }
        }

        private void ShowHelp()
        {
            ShareService.OpenUri(new Uri(Constants.ShareLinks.HighriseHelp));
        }

        private void ShowPrivacy()
        {
            ShareService.OpenUri(new Uri(Constants.ShareLinks.PrivacyPolicy));
        }

        private void ShowTerms()
        {
            ShareService.OpenUri(new Uri(Constants.ShareLinks.TermsOfService));
        }

        private void SignIn()
        {
            Navigation.NavigateAsync(Constants.Navigation.MainPage, null, false, false, true);
        }
    }
}