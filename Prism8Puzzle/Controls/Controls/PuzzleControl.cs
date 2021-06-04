using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Controls
{
    public class PuzzleControl : Control
    {
        public static readonly DependencyProperty CustomCommandProperty;
        public static readonly DependencyProperty CustomCommandParameterProperty;
        public static readonly DependencyProperty ItemsSourceProperty;

        public ICommand CustomCommand
        {
            get { return (ICommand)GetValue(CustomCommandProperty); }
            set { SetValue(CustomCommandProperty, value); }
        }
        public object CustomCommandParameter
        {
            get { return (object)GetValue(CustomCommandParameterProperty); }
            set { SetValue(CustomCommandParameterProperty, value); }
        }
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        static PuzzleControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PuzzleControl), new FrameworkPropertyMetadata(typeof(PuzzleControl)));

            CustomCommandProperty = DependencyProperty.Register("CustomCommand", typeof(ICommand), typeof(PuzzleControl));
            CustomCommandParameterProperty = DependencyProperty.Register("CustomCommandParameter", typeof(object), typeof(PuzzleControl));
            ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(PuzzleControl));
        }
    }
}
