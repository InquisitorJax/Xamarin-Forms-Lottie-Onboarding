using Core;
using Prism.Commands;
using System.Windows.Input;

namespace SampleApplication
{
    public class AuthViewModel : ViewModelBase
    {
        private const string HelpUrl = "https://help.highrisehq.com/";
        private const string PrivacyUrl = "https://highrisehq.com/privacy/";
        private const string TermsUrl = "https://highrisehq.com/terms/";

        public AuthViewModel()
        {
            SignInCommand = new DelegateCommand(SignIn);
        }

        public ICommand SignInCommand { get; private set; }

        private void SignIn()
        {
            Navigation.NavigateAsync(Constants.Navigation.MainPage);
        }
    }
}