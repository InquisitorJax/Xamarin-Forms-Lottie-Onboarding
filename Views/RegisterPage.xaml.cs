using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : PageBase
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(1000);
            await _btnSignUp.FadeTo(1, 750, Easing.Linear);
        }

        protected override void OnPageOrientationUpdated()
        {
            base.OnPageOrientationUpdated();

            switch (PageOrientation)
            {
                case PageOrientation.Landscape:
                    _logo.IsVisible = _height > 400;
                    _logo.WidthRequest = 40;
                    _logo.HeightRequest = 40;
                    break;

                case PageOrientation.Portrait:
                default:
                    _logo.IsVisible = true;
                    _logo.WidthRequest = 100;
                    _logo.HeightRequest = 100;
                    break;
            }
        }
    }
}