using System.Threading.Tasks;

namespace Core
{
    public interface IUserNotifier
    {
        Task<bool> ShowConfirmAsync(string message, string caption, string okText = "ok", string cancelText = "cancel");

        Task ShowMessageAsync(string message, string caption, string acceptButtonText = "Ok");

        Task<UserPromptResult> ShowPromptAsync(UserPromptConfig config);

        Task ShowToastAsync(string message, string caption = "", int durationInSeconds = 2);
    }

    public class UserPromptConfig
    {
        public UserPromptConfig()
        {
            OkText = "ok";
            CancelText = "cancel";
            CanCancel = true;
        }

        public bool CanCancel { get; set; }
        public string CancelText { get; set; }
        public string Caption { get; set; }
        public string DefaultInput { get; set; }
        public bool InputCompulsory { get; set; }
        public string LabelText { get; set; }
        public string Message { get; set; }

        public string OkText { get; set; }
    }

    public class UserPromptResult
    {
        public bool Cancelled { get; set; }

        public string InputText { get; set; }
    }
}