using GAAPICommon.Architecture;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace GACore.DemoApp;

/// <summary>
/// Just makes a random kingpin state
/// </summary>
[SupportedOSPlatform("windows")]
public class FooKingpinState : IKingpinState
{
    private PositionControlStatus _positionControlStatus = Tools.RandomEnumValue<PositionControlStatus>();

    public PositionControlStatus PositionControlStatus
    {
        get { return _positionControlStatus; }
        set
        {
            if (_positionControlStatus != value)
            {
                _positionControlStatus = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    public NavigationStatus navigationStatus = Tools.RandomEnumValue<NavigationStatus>();

    public NavigationStatus NavigationStatus
    {
        get { return navigationStatus; }
        set
        {
            if (navigationStatus != value)
            {
                navigationStatus = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    public DynamicLimiterStatus dynamicLimiterStatus = Tools.RandomEnumValue<DynamicLimiterStatus>();

    public DynamicLimiterStatus DynamicLimiterStatus
    {
        get { return dynamicLimiterStatus; }
        set
        {
            if (dynamicLimiterStatus != value)
            {
                dynamicLimiterStatus = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    public string Alias => "Foo Kingpin";

    public bool IsVirtual { get; } = Tools.Random.Next(0, 2) > 0;

    public byte Tick => throw new NotImplementedException();

    private float _x = Tools.Random.Next(-10, 10);

    public float X
    {
        get { return _x; }
        set
        {
            if (_x != value)
            {
                _x = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    private float _y = Tools.Random.Next(-10, 10);

    public float Y
    {
        get { return _y; }
        set
        {
            if (_y != value)
            {
                _y = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    private float _heading = Tools.Random.Next(-3, 3);

    public float Heading
    {
        get { return _heading; }
        set
        {
            if (_heading != value)
            {
                _heading = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    public MovementType currentMovementType = Tools.RandomEnumValue<MovementType>();

    public MovementType CurrentMovementType
    {
        get { return currentMovementType; }
        set
        {
            if (currentMovementType != value)
            {
                currentMovementType = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    public IPAddress IPAddress => IPAddress.Loopback;

    public byte[] StateCastExtendedData => throw new NotImplementedException();

    public double Speed => throw new NotImplementedException();

    public int WaypointLastId => throw new NotImplementedException();

    public int WaypointNextId => throw new NotImplementedException();

    public AgvMode AgvMode => throw new NotImplementedException();

    public double BatteryChargePercentage => throw new NotImplementedException();

    public ExtendedDataFaultStatus ExtendedDataFaultStatus => ExtendedDataFaultStatus.OK;

    public FrozenState FrozenState => throw new NotImplementedException();

    public bool IsCharging => throw new NotImplementedException();

    public static IKingpinState FromGood()
    {
        return new FooKingpinState()
        {
            PositionControlStatus = PositionControlStatus.OK,
            NavigationStatus = NavigationStatus.OK,
            DynamicLimiterStatus = DynamicLimiterStatus.OK
        };
    }

    public int LastCompletedInstructionId
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public TimeSpan Stationary
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public byte[] CurrentWaypointExtendedData
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}