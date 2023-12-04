using System.Windows;
using System.Windows.Controls;

namespace GACore.Controls;

public class CallButtonControl : Control
{
    public static readonly DependencyProperty LightStateProperty =
        DependencyProperty.Register("LightState", typeof(LightState?),
        typeof(CallButtonControl), new PropertyMetadata(null));

    public static readonly DependencyProperty IsPressedProperty =
        DependencyProperty.Register("IsPressed", typeof(bool?),
        typeof(CallButtonControl), new PropertyMetadata(null));

    public static readonly DependencyProperty AliasProperty =
        DependencyProperty.Register("Alias", typeof(string),
        typeof(CallButtonControl), new PropertyMetadata("Call Button"));

    public LightState? LightState
    {
        get { return (LightState?)GetValue(LightStateProperty); }
        set { SetValue(LightStateProperty, value); }
    }

    public bool? IsPressed
    {
        get { return (bool?)GetValue(IsPressedProperty); }
        set { SetValue(IsPressedProperty, value); }
    }

    public string Alias
    {
        get { return (string)GetValue(AliasProperty); }
        set { SetValue(AliasProperty, value); }
    }

    static CallButtonControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CallButtonControl), new FrameworkPropertyMetadata(typeof(CallButtonControl)));
    }
}