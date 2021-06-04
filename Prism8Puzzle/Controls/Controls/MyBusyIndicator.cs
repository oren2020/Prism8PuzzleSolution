using System.Windows;
using System.Windows.Controls;

namespace Controls
{
    public class MyBusyIndicator : Control
    {
        public static readonly DependencyProperty IsBusyProperty;
        public static readonly DependencyProperty BusyContentProperty;
        public static readonly DependencyProperty AdditionalContentProperty;

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }
        public string BusyContent
        {
            get => (string)GetValue(BusyContentProperty);
            set => SetValue(BusyContentProperty, value);
        }
        public object AdditionalContent
        {
            get => GetValue(AdditionalContentProperty);
            set => SetValue(AdditionalContentProperty, value);
        }

        static MyBusyIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyBusyIndicator), new FrameworkPropertyMetadata(typeof(MyBusyIndicator)));

            AdditionalContentProperty = DependencyProperty.Register("AdditionalContent", typeof(object), typeof(MyBusyIndicator));
            IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(MyBusyIndicator));
            BusyContentProperty = DependencyProperty.Register("BusyContent", typeof(string), typeof(MyBusyIndicator));
        }
    }
}
