using Core;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : PageBase, IView
    {
        public WelcomePage()
        {
            InitializeComponent();
            ViewModel = new WelcomeViewModel();
            BindingContext = ViewModel;
        }

        public IViewModel ViewModel { get; set; }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(2000);

            await _btnSignIn.FadeTo(1, 750, Easing.Linear);
            await _btnSignUp.FadeTo(1, 750, Easing.Linear);
            await _lblLearnMore.FadeTo(1, 750, Easing.SpringIn);

            await Task.Delay(2000);
            this.rotator.EnableAutoPlay = true;
        }

        protected override void OnPageOrientationUpdated()
        {
            switch (PageOrientation)
            {
                case PageOrientation.Landscape:
                    _lottieContainer.SetValue(Grid.RowProperty, 0);
                    _lottieContainer.SetValue(Grid.ColumnProperty, 1);
                    _lottieContainer.SetValue(Grid.RowSpanProperty, 4);
                    _lottieContainer.WidthRequest = _width * 0.4d;
                    _lottieContainer.HeightRequest = _height * 0.9d;
                    _headerContainer.SetValue(Grid.RowProperty, 1);
                    _logo.WidthRequest = 100;
                    _logo.HeightRequest = 100;
                    _lblLogo.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                    break;

                case PageOrientation.Portrait:
                default:
                    _lottieContainer.SetValue(Grid.RowProperty, 1);
                    _lottieContainer.SetValue(Grid.ColumnProperty, 0);
                    _lottieContainer.SetValue(Grid.RowSpanProperty, 1);
                    _lottieContainer.WidthRequest = SizeNotSet;
                    _lottieContainer.HeightRequest = SizeNotSet;
                    _headerContainer.SetValue(Grid.RowProperty, 0);
                    _logo.WidthRequest = 45;
                    _logo.HeightRequest = 45;
                    _lblLogo.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                    break;
            }
        }
    }
}