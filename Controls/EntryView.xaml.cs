using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApplication.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryView : ContentView
    {
        public static readonly BindableProperty EntryTextProperty = BindableProperty.Create(nameof(EntryText), typeof(string), typeof(EntryView), null, BindingMode.TwoWay, propertyChanged: OnEntryTextChanged);

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(EntryView), false, BindingMode.Default, null, (bindable, oldValue, newValue) =>
            {
                var ctrl = (EntryView)bindable;
                ctrl.IsPassword = (bool)newValue;
            });

        public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(EntryView), null);

        public static BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(EntryView), Keyboard.Default,
                BindingMode.Default, null, (bindable, oldValue, newValue) =>
                {
                    var ctrl = (EntryView)bindable;
                    ctrl._entry.Keyboard = (Keyboard)newValue;
                });

        public EntryView()
        {
            InitializeComponent();
            _entry.Focused += Entry_Focused;
            _entry.Unfocused += Entry_Unfocused;
        }

        public string EntryText
        {
            get { return (string)GetValue(EntryTextProperty); }
            set { SetValue(EntryTextProperty, value); }
        }

        public bool IsPassword
        {
            get { return (bool)base.GetValue(Entry.IsPasswordProperty); }
            set
            {
                base.SetValue(Entry.IsPasswordProperty, value);
                _entry.IsPassword = value;
            }
        }

        public Keyboard Keyboard
        {
            get { return (Keyboard)GetValue(KeyboardProperty); }
            set { SetValue(KeyboardProperty, value); }
        }

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        //TODO: Make use of Validation Callbacks? https://developer.xamarin.com/guides/xamarin-forms/xaml/bindable-properties/#validation
        //OR: INotifyDataErrorInfo behaviour: https://blogs.msdn.microsoft.com/premier_developer/2017/04/03/validate-input-in-xamarin-forms-using-inotifydataerrorinfo-custom-behaviors-effects-and-prism/
        private static async void OnEntryTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var entry = (EntryView)bindable;
            if (!string.IsNullOrEmpty((string)newValue))
            {
                int y = Device.RuntimePlatform == Device.Android ? -12 : Device.RuntimePlatform == Device.iOS ? -20 : -25;
                await entry._label.TranslateTo(-2, y);
            }
            else
            {
                await entry._label.TranslateTo(-2, 5);
            }
        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            _label.TextColor = Color.Accent;
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            _label.TextColor = Color.Default;
        }
    }
}