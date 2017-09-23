using Autofac;
using Core;
using Prism.Commands;
using SampleApplication.AppServices;
using SampleApplication.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SampleApplication
{
    public class FirstContactPromptViewModel : ViewModelBase
    {
        private const string GettingStartedLink = "https://help.highrisehq.com/start/";

        public FirstContactPromptViewModel()
        {
            OpenHelpLinkCommand = new DelegateCommand<HelpItem>(OpenHelpLink);

            HelpItems = new List<HelpItem>
            {
                new HelpItem
                {
                    Title = "Getting Started",
                    Description = "Short videos to help you get started with a new account.",
                    Link = GettingStartedLink
                },
                new HelpItem
                {
                    Title = "Account & Billing",
                    Description = "Adjust credit card information, upgrade, downgrade, or cancel your account.",
                    Link = "https://help.highrisehq.com/account/"
                },
                new HelpItem
                {
                    Title = "How To's",
                    Description = "Examples on how to accomplish things using Highrise.",
                    Link = "https://help.highrisehq.com/how-to/"
                },
                new HelpItem
                {
                    Title = "Users & Teams",
                    Description = "How to invite team members, share information, grant permissions, and more.",
                    Link = "https://help.highrisehq.com/users/"
                },
                new HelpItem
                {
                    Title = "Email",
                    Description = "Track communication in Highrise.",
                    Link = "https://help.highrisehq.com/email/"
                },
                new HelpItem
                {
                    Title = "Broadcast",
                    Description = "Email a group of people in Highrise.",
                    Link = "https://help.highrisehq.com/broadcast/"
                },
                new HelpItem
                {
                    Title = "Tasks",
                    Description = "Keep track of what you need to get done.",
                    Link = "https://help.highrisehq.com/tasks/"
                },
                new HelpItem
                {
                    Title = "Notes",
                    Description = "Share and store information with your team.",
                    Link = "https://help.highrisehq.com/notes/"
                },
                new HelpItem
                {
                    Title = "Contacts",
                    Description = "Organize the people and companies that matter to you.",
                    Link = "https://help.highrisehq.com/contacts/"
                },
                new HelpItem
                {
                    Title = "Imports & Exports",
                    Description = "The best ways to get information in and out of Highrise.",
                    Link = "https://help.highrisehq.com/imports-exports/"
                },
                new HelpItem
                {
                    Title = "Cases & Deals",
                    Description = "File communication with multiple people and manage money-based projects.",
                    Link = "https://help.highrisehq.com/cases-deals/"
                },
                new HelpItem
                {
                    Title = "Mobile",
                    Description = "Using Highrise on smart phones and tablets.",
                    Link = "https://help.highrisehq.com/mobile/"
                },
                new HelpItem
                {
                    Title = "Integrations",
                    Description = "Using Highrise with your favorite apps.",
                    Link = "https://help.highrisehq.com/integrations/"
                },
                new HelpItem
                {
                    Title = "Troubleshooting",
                    Description = "Quick fixes for common situations.",
                    Link = "https://help.highrisehq.com/troubleshooting/"
                },
                new HelpItem
                {
                    Title = "Signup FAQ",
                    Description = "Decide if Highrise is right for your organization.",
                    Link = "https://help.highrisehq.com/signup/"
                }
            };
        }

        public IList<HelpItem> HelpItems { get; private set; }
        public ICommand OpenHelpLinkCommand { get; private set; }

        private IShareService ShareService
        {
            get { return CC.IoC.Resolve<IShareService>(); }
        }

        private void OpenHelpLink(HelpItem item)
        {
            if (item != null)
            {
                ShareService.OpenUri(new Uri(item.Link));
            }
        }
    }
}