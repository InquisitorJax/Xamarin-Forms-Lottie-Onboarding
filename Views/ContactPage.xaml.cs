using Core;

using Xamarin.Forms;

namespace SampleApplication.Views
{
    public partial class ContactPage : ContentPage, IView
    {
        public ContactPage()
        {
            InitializeComponent();
        }

        public IViewModel ViewModel { get; set; }
    }
}