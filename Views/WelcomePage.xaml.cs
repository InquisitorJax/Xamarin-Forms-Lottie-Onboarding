using Core;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage, IView
    {
        public WelcomePage()
        {
            InitializeComponent();
            ViewModel = new WelcomeViewModel();
            BindingContext = ViewModel;
        }

        public IViewModel ViewModel { get; set; }
    }
}