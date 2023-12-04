using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GACore.Controls;

public partial class IPV4Control : UserControl
{
    public static readonly DependencyProperty IPV4StringProperty =
        DependencyProperty.Register(
        "IPV4String", typeof(string),
        typeof(IPV4Control),
        new FrameworkPropertyMetadata("127.0.0.1")
        );

    public string IPV4String
    {
        get { return (string)GetValue(IPV4StringProperty); }
        set { SetValue(IPV4StringProperty, value); }
    }

    public IPV4Control()
    {
        InitializeComponent();
    }

    public IPAddress? ToIPAddress()
    {
        IPAddress.TryParse(IPV4String, out IPAddress? ipAddress);
        return ipAddress;
    }

    private void IpV4TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IPAddress.TryParse(IPV4String, out _)) ipV4TextBox.Background = Brushes.Crimson;
        else ipV4TextBox.Background = Brushes.White;
    }
}