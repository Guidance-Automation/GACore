using System.Windows;
using System.Windows.Controls;

namespace GACore.Controls;

/// <summary>
/// Interaction logic for IsTrueCircleControl.xaml
/// </summary>
public partial class IsTrueCircleControl : UserControl
{
    public static readonly DependencyProperty IsTrueProperty =
        DependencyProperty.Register(
        "IsTrue", typeof(bool),
        typeof(IsTrueCircleControl)
        );

    public bool IsTrue
    {
        get { return (bool)GetValue(IsTrueProperty); }
        set { SetValue(IsTrueProperty, value); }
    }

    public IsTrueCircleControl()
    {
        InitializeComponent();
    }
}