using Autofac;
using Core;
using Prism.Commands;
using SampleApplication.AppServices;
using SampleApplication.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace SampleApplication
{
    public class WelcomeViewModel : ViewModelBase
    {
        public WelcomeViewModel()
        {
            WelcomeAnimations = new List<AnimationModel>
            {
                new AnimationModel { AnimationFilename = "birds.json", Description = "Track leads." },
                new AnimationModel { AnimationFilename = "nudge.json", Description = "Manage follow-ups." },
                new AnimationModel { AnimationFilename = "stopwatch.json", Description = "Zero learning curve!" },
            };

            SignUpCommand = new DelegateCommand(SignUp);
            SignInCommand = new DelegateCommand(SignIn);
            OpenHighriseHelpCommand = new DelegateCommand(OpenHighriseHelp);
        }

        public ICommand OpenHighriseHelpCommand { get; private set; }

        public ICommand SignInCommand { get; private set; }

        public ICommand SignUpCommand { get; private set; }

        public List<AnimationModel> WelcomeAnimations { get; set; }

        private IShareService ShareService
        {
            get { return CC.IoC.Resolve<IShareService>(); }
        }

        private void NavigateToAuthPage(bool signUp)
        {
            string page = signUp ? Constants.Navigation.RegisterPage : Constants.Navigation.AuthPage;
            Navigation.NavigateAsync(page, null, true, true);
        }

        private void OpenHighriseHelp()
        {
            ShareService.OpenUri(new System.Uri(Constants.ShareLinks.HighriseHelp));
        }

        private void SignIn()
        {
            NavigateToAuthPage(false);
        }

        private void SignUp()
        {
            NavigateToAuthPage(true);
        }
    }
}