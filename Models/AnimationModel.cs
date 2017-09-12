using Prism.Mvvm;

namespace SampleApplication.Models
{
    public class AnimationModel : BindableBase
    {
        private string _animationFilename;
        private string _description;

        public string AnimationFilename
        {
            get { return _animationFilename; }
            set { SetProperty(ref _animationFilename, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
    }
}