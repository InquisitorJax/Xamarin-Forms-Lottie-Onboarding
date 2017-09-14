using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : PageBase
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(1000);
            await _btnSignIn.FadeTo(1, 750, Easing.Linear);
        }

        protected override void OnPageOrientationUpdated()
        {
            base.OnPageOrientationUpdated();

            switch (PageOrientation)
            {
                case PageOrientation.Landscape:
                    _footerContainer.HeightRequest = 20;
                    _logo.WidthRequest = 40;
                    _logo.HeightRequest = 40;
                    _helpContainer.Orientation = StackOrientation.Horizontal;
                    break;

                case PageOrientation.Portrait:
                default:
                    _footerContainer.HeightRequest = 100;
                    _logo.WidthRequest = 100;
                    _logo.HeightRequest = 100;
                    _helpContainer.Orientation = StackOrientation.Vertical;
                    break;
            }
        }
    }
}