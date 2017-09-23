using Prism.Mvvm;
using System;

namespace SampleApplication
{
    public class HighriseUser : BindableBase
    {
        private string _description;
        private bool _hasShownFirstContactAchievementPrompt;
        private bool _isLoggedIn;
        private DateTimeOffset _lastLogin;
        private string _name;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public bool HasShownFirstContactAchievementPrompt
        {
            get { return _hasShownFirstContactAchievementPrompt; }
            set { SetProperty(ref _hasShownFirstContactAchievementPrompt, value); }
        }

        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set { SetProperty(ref _isLoggedIn, value); }
        }

        public DateTimeOffset LastLogin
        {
            get { return _lastLogin; }
            set { SetProperty(ref _lastLogin, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
    }
}