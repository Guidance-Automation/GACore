using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GACore.Controls;

/// <summary>
/// Interaction logic for LightStateControl.xaml
/// </summary>
public partial class LightStateControl : UserControl
{
    public static readonly DependencyProperty LightStateProperty =
       DependencyProperty.Register("LightState", typeof(LightState?),
       typeof(LightStateControl),
       new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnLightStateChanged)));

    public LightState? LightState
    {
        get { return (LightState?)GetValue(LightStateProperty); }
        set { SetValue(LightStateProperty, value); }
    }

    private static void OnLightStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        LightStateControl lightStateControl = (LightStateControl)d;

        Color target = lightStateControl.LightState.ToColor();

        ColorAnimation colorChangeAnimation = new()
        {
            From = Colors.White,
            To = target,
            Duration = TimeSpan.FromSeconds(1),
            AutoReverse = true,
            RepeatBehavior = RepeatBehavior.Forever
        };

        PropertyPath colorTargetPath = new("(Panel.Background).(SolidColorBrush.Color)");
        Storyboard CellBackgroundChangeStory = new();
        Storyboard.SetTarget(colorChangeAnimation, lightStateControl.canvas);
        Storyboard.SetTargetProperty(colorChangeAnimation, colorTargetPath);
        CellBackgroundChangeStory.Children.Add(colorChangeAnimation);
        CellBackgroundChangeStory.Begin();
    }

    public LightStateControl()
    {
        InitializeComponent();
    }
}