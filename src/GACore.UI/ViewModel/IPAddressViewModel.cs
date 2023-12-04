using GACore.Architecture;
using System.Net;
using System.Runtime.Versioning;

namespace GACore.UI.ViewModel;

[SupportedOSPlatform("windows")]
public class IPAddressViewModel : AbstractViewModel<IIPAddressable>
{
    private byte[] _ipAddressBytes = [169, 254, 0, 0];

    public int ByteA
    {
        get { return _ipAddressBytes[0]; }
        set
        {
            if (_ipAddressBytes[0] != value)
            {
                _ipAddressBytes[0] = (byte)value;
                OnNotifyPropertyChanged();
            }
        }
    }

    public int ByteB
    {
        get { return _ipAddressBytes[1]; }
        set
        {
            if (_ipAddressBytes[1] != value)
            {
                _ipAddressBytes[1] = (byte)value;
                OnNotifyPropertyChanged();
            }
        }
    }

    public int ByteC
    {
        get
        {
            return _ipAddressBytes[2];
        }
        set
        {
            if (_ipAddressBytes[2] != value)
            {
                _ipAddressBytes[2] = (byte)value;
                OnNotifyPropertyChanged();
            }
        }
    }

    public int ByteD
    {
        get { return _ipAddressBytes[3]; }
        set
        {
            if (_ipAddressBytes[3] != value)
            {
                _ipAddressBytes[3] = (byte)value;
                OnNotifyPropertyChanged();
            }
        }
    }

    private void NotifyByteUpdates()
    {
        OnNotifyPropertyChanged(nameof(ByteA));
        OnNotifyPropertyChanged(nameof(ByteB));
        OnNotifyPropertyChanged(nameof(ByteC));
        OnNotifyPropertyChanged(nameof(ByteD));
    }

    public void ApplyChanges()
    {
        Logger?.Trace("[IPAddressViewModel] ApplyChanges()");
        if (Model != null) Model.IPAddress = IPAddress;
    }

    public IPAddress? IPAddress
    {
        get { return new IPAddress(_ipAddressBytes); }
        set
        {
            byte[] ipV4ByteValue = (value == null) ? [169, 254, 0, 0] : value.MapToIPv4().GetAddressBytes();

            if (_ipAddressBytes != ipV4ByteValue)
            {
                _ipAddressBytes = ipV4ByteValue;
                OnNotifyPropertyChanged();
                NotifyByteUpdates();
            }
        }
    }


    internal IPAddressViewModel()
    {
    }

    protected override void HandleModelUpdate(IIPAddressable? oldValue, IIPAddressable? newValue)
    {
        IPAddress = newValue?.IPAddress ?? null;
        base.HandleModelUpdate(oldValue, newValue);
    }
}
