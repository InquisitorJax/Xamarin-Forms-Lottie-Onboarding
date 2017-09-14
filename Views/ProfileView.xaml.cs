using Core.Controls;
using SampleApplication.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileView : CommandView
    {
        public static readonly BindableProperty ValueProperty = BindableProperty.Create("Value", typeof(HighriseUser), typeof(ProfileView), null);

        public ProfileView()
        {
            InitializeComponent();
        }

        public HighriseUser Value
        {
            get { return (HighriseUser)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
    }
}