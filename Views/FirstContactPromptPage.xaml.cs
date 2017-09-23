using Core;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstContactPromptPage : ContentPage, IView
    {
        public FirstContactPromptPage()
        {
            InitializeComponent();
        }

        public IViewModel ViewModel { get; set; }
    }
}