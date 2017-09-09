using Prism.Mvvm;

namespace SampleApplication.Models
{
    public class AnimationModel : BindableBase
    {
        private string _animationFilename;

        public string AnimationFilename
        {
            get { return _animationFilename; }
            set { SetProperty(ref _animationFilename, value); }
        }
    }
}