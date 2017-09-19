using Core;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SampleApplication.Views
{
    public partial class ItemListPage : ContentPage, IView
    {
        public ItemListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(1000);
            _lblLonely.FadeTo(1, 750, Easing.Linear);
            await Task.Delay(1500);
            await _btnCreateContact.FadeTo(1, 750, Easing.Linear);
        }

        #region IView implementation

        public IViewModel ViewModel { get; set; }

        #endregion IView implementation
    }
}