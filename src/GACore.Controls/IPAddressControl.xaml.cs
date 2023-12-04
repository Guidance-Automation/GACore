using GACore.Extensions;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace GACore.Controls;

/// <summary>
/// This is really outdated and very old. Should be factored out. 
/// </summary>
public partial class IPAddressControl : UserControl, INotifyPropertyChanged
{

    private int _byteA = 0;
    private int _byteB = 0;
    private int _byteC = 0;
    private int _byteD = 0;

    public static readonly DependencyProperty IPAddressProperty = DependencyProperty.Register
        ("IPAddress",
        typeof(IPAddress),
        typeof(IPAddressControl),
        new FrameworkPropertyMetadata(IPAddress.None, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged)
        );

    public event PropertyChangedEventHandler PropertyChanged;

    public static readonly DependencyProperty MaximumIPAddressProperty = DependencyProperty.Register
        ("MaximumIPAddress",
        typeof(IPAddress),
        typeof(IPAddressControl),
        new FrameworkPropertyMetadata(IPAddress.Parse("255.255.255.255"), OnLimitsChanged)
        );

    public static readonly DependencyProperty MinimumIPAddressProperty = DependencyProperty.Register
        ("MinimumIPAddress",
        typeof(IPAddress),
        typeof(IPAddressControl),
        new FrameworkPropertyMetadata(IPAddress.Parse("0.0.0.0"), OnLimitsChanged)
        );

    public IPAddress MaximumIPAddress
    {
        get { return (IPAddress)GetValue(MaximumIPAddressProperty); }
        set
        {
            SetValue(MaximumIPAddressProperty, value);
            UpdateToolTips();
        }
    }

    public IPAddress MinimumIPAddress
    {
        get { return (IPAddress)GetValue(MinimumIPAddressProperty); }
        set
        {
            SetValue(MinimumIPAddressProperty, value);
            UpdateToolTips();
        }
    }

    private void UpdateToolTips()
    {
        ByteA = ClampIPByte(ByteA, 0);
        ByteB = ClampIPByte(ByteB, 1);
        ByteC = ClampIPByte(ByteC, 2);
        ByteD = ClampIPByte(ByteD, 3);

        byte[] minimum = MinimumIPAddress.GetAddressBytes();
        byte[] maximum = MaximumIPAddress.GetAddressBytes();

        byteAUpDown.ToolTip = string.Format("{0}-{1}", minimum[0], maximum[0]);
        byteBUpDown.ToolTip = string.Format("{0}-{1}", minimum[1], maximum[1]);
        byteCUpDown.ToolTip = string.Format("{0}-{1}", minimum[2], maximum[2]);
        byteDUpDown.ToolTip = string.Format("{0}-{1}", minimum[3], maximum[3]);
    }

    public IPAddress IPAddress
    {
        get { return (IPAddress)GetValue(IPAddressProperty); }
        set { SetValue(IPAddressProperty, value); }
    }

    private static void OnLimitsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        IPAddressControl control = obj as IPAddressControl;

        if (control.MinimumIPAddress == IPAddress.None)
            control.MinimumIPAddress = IPAddress.Parse("0.0.0.0");


        if (control.MaximumIPAddress == IPAddress.None)
            control.MaximumIPAddress = IPAddress.Parse("255.255.255.255");

        byte[] minimumBytes = control.MinimumIPAddress.GetAddressBytes();
        byte[] maximumBytes = control.MaximumIPAddress.GetAddressBytes();

        for (int i = 0; i <= 3; i++)
        {
            if (minimumBytes[i] > maximumBytes[i])
            {
                minimumBytes[i] = maximumBytes[i];

                IPAddress updatedMinimum = IPAddress.Parse(string.Format("{0}.{1}.{2}.{3}",
                    minimumBytes[0], minimumBytes[1], minimumBytes[2], minimumBytes[3]));

                control.MinimumIPAddress = updatedMinimum;
                return;
            }
        }

        control.UpdateToolTips();
    }

    private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        IPAddressControl control = obj as IPAddressControl;

        IPAddress newValue = (IPAddress)e.NewValue;

        byte[] bytes = newValue.GetAddressBytes();

        control.ByteA = bytes[0];
        control.ByteB = bytes[1];
        control.ByteC = bytes[2];
        control.ByteD = bytes[3];
    }

    private void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public IPAddressControl()
    {
        InitializeComponent();
        UpdateToolTips();
    }



    private int ClampIPByte(int byteValue, int index)
    {
        int minimum = MinimumIPAddress.GetAddressBytes()[index];
        int maximum = MaximumIPAddress.GetAddressBytes()[index];

        return byteValue.Clamp(minimum, maximum);
    }

    public int ByteD
    {
        get { return _byteD; }
        set
        {
            if (_byteD != value)
            {
                _byteD = ClampIPByte(value, 3);
                OnNotifyPropertyChanged();
                IPAddress = IPAddress.Parse(string.Format("{0}.{1}.{2}.{3}", ByteA, ByteB, ByteC, ByteD));
            }
        }
    }

    public int ByteC
    {
        get { return _byteC; }
        set
        {
            if (_byteC != value)
            {
                _byteC = ClampIPByte(value, 2);
                OnNotifyPropertyChanged();
                IPAddress = IPAddress.Parse(string.Format("{0}.{1}.{2}.{3}", ByteA, ByteB, ByteC, ByteD));
            }
        }
    }

    public int ByteB
    {
        get { return _byteB; }
        set
        {
            if (_byteB != value)
            {
                _byteB = ClampIPByte(value, 1);
                OnNotifyPropertyChanged();
                IPAddress = IPAddress.Parse(string.Format("{0}.{1}.{2}.{3}", ByteA, ByteB, ByteC, ByteD));
            }
        }
    }

    public int ByteA
    {
        get { return _byteA; }
        set
        {
            if (_byteA != value)
            {
                _byteA = ClampIPByte(value, 0);
                OnNotifyPropertyChanged();
                IPAddress = IPAddress.Parse(string.Format("{0}.{1}.{2}.{3}", ByteA, ByteB, ByteC, ByteD));
            }
        }
    }
}