using Core;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage, IView
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        public IViewModel ViewModel { get; set; }
    }
}