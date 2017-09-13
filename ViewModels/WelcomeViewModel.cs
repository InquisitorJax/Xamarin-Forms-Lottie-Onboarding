using Core;
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
        }

        public ICommand SignInCommand { get; private set; }
        public ICommand SignUpCommand { get; private set; }
        public List<AnimationModel> WelcomeAnimations { get; set; }

        private void NavigateToAuthPage(bool signUp)
        {
            string authParamsValue = signUp ? Constants.ParameterValues.AuthOptionSignUp : Constants.ParameterValues.AuthOptionSignIn;
            var args = new Dictionary<string, string>
            {
                { Constants.Parameters.AuthOption, authParamsValue }
            };
            Navigation.NavigateAsync(Constants.Navigation.AuthPage, args);
        }
    }
}