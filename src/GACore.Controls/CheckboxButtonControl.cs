using System.Windows;
using System.Windows.Controls;

namespace GACore.Controls;

public class CheckboxButtonControl : Button
{
    public static readonly DependencyProperty IsCheckedProperty =
        DependencyProperty.Register("IsChecked", typeof(bool),
        typeof(CheckboxButtonControl), new PropertyMetadata(false));

    public bool IsChecked
    {
        get { return (bool)GetValue(IsCheckedProperty); }
        set { SetValue(IsCheckedProperty, value); }
    }

    public string CheckedText
    {
        get { return (string)GetValue(CheckedTextProperty); }
        set { SetValue(CheckedTextProperty, value); }
    }

    public string UncheckedText
    {
        get { return (string)GetValue(UncheckedTextProperty); }
        set { SetValue(UncheckedTextProperty, value); }
    }

    public static readonly DependencyProperty CheckedTextProperty =
        DependencyProperty.Register("CheckedText", typeof(string),
        typeof(CheckboxButtonControl), new PropertyMetadata("Checked"));

    public static readonly DependencyProperty UncheckedTextProperty =
        DependencyProperty.Register("UncheckedText", typeof(string),
        typeof(CheckboxButtonControl), new PropertyMetadata("Unchecked"));

    static CheckboxButtonControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckboxButtonControl), new FrameworkPropertyMetadata(typeof(CheckboxButtonControl)));
    }
}
