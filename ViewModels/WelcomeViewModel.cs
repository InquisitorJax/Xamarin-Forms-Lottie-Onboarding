using Core;
using SampleApplication.Models;
using System.Collections.Generic;

namespace SampleApplication
{
    public class WelcomeViewModel : ViewModelBase
    {
        public WelcomeViewModel()
        {
            WelcomeAnimations = new List<AnimationModel>
            {
                new AnimationModel { AnimationFilename = "birds.json" }
            };
        }

        public List<AnimationModel> WelcomeAnimations { get; set; }
    }
}