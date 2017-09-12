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
                new AnimationModel { AnimationFilename = "birds.json", Description = "Track leads." },
                new AnimationModel { AnimationFilename = "nudge.json", Description = "Manage follow-ups." },
                new AnimationModel { AnimationFilename = "stopwatch.json", Description = "Zero learning curve!" },
            };
        }

        public List<AnimationModel> WelcomeAnimations { get; set; }
    }
}