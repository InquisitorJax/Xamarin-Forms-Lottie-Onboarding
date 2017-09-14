using Core;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage, IView
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        public IViewModel ViewModel { get; set; }
    }
}