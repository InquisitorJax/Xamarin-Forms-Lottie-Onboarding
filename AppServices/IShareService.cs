using Plugin.Share;
using Plugin.Share.Abstractions;
using System;

namespace SampleApplication.AppServices
{
    public interface IShareService
    {
        void OpenUri(Uri uri);

        void Share(string shareMessage, string title = null, string url = null);
    }

    public class ShareService : IShareService
    {
        public void OpenUri(Uri uri)
        {
            Xamarin.Forms.Device.OpenUri(uri);
        }

        public void Share(string shareMessage, string title = null, string url = null)
        {
            ShareMessage message = new ShareMessage { Text = shareMessage, Title = title, Url = url };
            ShareOptions options = new ShareOptions { ChooserTitle = "Highrise is awesome!" };
            CrossShare.Current.Share(message, options);
        }
    }
}