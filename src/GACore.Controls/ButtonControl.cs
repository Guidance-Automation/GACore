using System.Windows;
using System.Windows.Controls;

namespace GACore.Controls;

public class ButtonControl : Button
{
    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register("Label", typeof(string),
        typeof(CheckboxButtonControl), new PropertyMetadata("Label"));

    public string Label
    {
        get { return (string)GetValue(LabelProperty); }
        set { SetValue(LabelProperty, value); }
    }

    static ButtonControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonControl), new FrameworkPropertyMetadata(typeof(ButtonControl)));
    }
}
