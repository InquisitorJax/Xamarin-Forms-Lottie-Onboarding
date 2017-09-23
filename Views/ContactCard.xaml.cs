using Core.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactCard : CommandView
    {
        public static readonly BindableProperty ValueProperty = BindableProperty.Create("Value", typeof(Contact), typeof(ContactCard), null);

        public ContactCard()
        {
            InitializeComponent();
        }

        public Contact Value
        {
            get { return (Contact)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
    }
}