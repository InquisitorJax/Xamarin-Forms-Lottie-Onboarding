using Acr.UserDialogs;
using Autofac;
using Plugin.Toasts;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Core
{
    public class XFormsUserNotifier : IUserNotifier
    {
        #region IUserNotifier implementation

        public async Task<bool> ShowConfirmAsync(string message, string caption, string okText = "ok", string cancelText = "cancel")
        {
            bool confirmed = false;
            var notificator = UserDialogs.Instance;
            confirmed = await notificator.ConfirmAsync(message, caption, okText, cancelText);
            return confirmed;
        }

        public async Task ShowMessageAsync(string message, string caption, string acceptButtonText = "Ok")
        {
            Page currentPage = (Page)CC.Navigation.Current;
            await currentPage.DisplayAlert(caption, message, acceptButtonText);
        }

        public async Task<UserPromptResult> ShowPromptAsync(UserPromptConfig config)
        {
            UserPromptResult retInput = null;
            var notificator = UserDialogs.Instance;
            PromptConfig pConfig = new PromptConfig
            {
                Message = config.Message,
                InputType = InputType.Default,
                Title = config.Caption,
                Placeholder = config.LabelText,
                Text = config.DefaultInput,
                IsCancellable = config.CanCancel,
                OkText = config.OkText,
                CancelText = config.CancelText
            };

            if (config.InputCompulsory)
            {
                pConfig.OnTextChanged = args =>
                {
                    args.IsValid = !string.IsNullOrWhiteSpace(args.Value);
                };
            }

            PromptResult promptResult = await notificator.PromptAsync(pConfig);

            retInput = new UserPromptResult
            {
                Cancelled = !promptResult.Ok,
                InputText = promptResult.Text
            };

            return retInput;
        }

        public async Task ShowToastAsync(string message, string caption = "", int durationInSeconds = 2)
        {
            TimeSpan duration = TimeSpan.FromSeconds(durationInSeconds);
            var notificator = CC.IoC.Resolve<IToastNotificator>();
            var options = new NotificationOptions() { Description = message, Title = caption };
            await notificator.Notify(options);
        }

        #endregion IUserNotifier implementation
    }
}