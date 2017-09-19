using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Core.Controls;

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
    }
}