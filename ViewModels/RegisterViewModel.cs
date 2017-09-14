using Core;
using Prism.Commands;
using System.Windows.Input;

namespace SampleApplication
{
    public class RegisterViewModel : ViewModelBase
    {
        public RegisterViewModel()
        {
            SignUpCommand = new DelegateCommand(SignUp);
        }

        public ICommand SignUpCommand { get; private set; }

        private void SignUp()
        {
            Navigation.NavigateAsync(Constants.Navigation.MainPage, null, false, false, true);
        }
    }
}