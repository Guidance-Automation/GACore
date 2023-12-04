using GAAPICommon.Architecture;
using GACore.Architecture;
using System.Net;

namespace GACore.Controls.ViewModel;

public class KingpinStateReporterViewModel : AbstractViewModel<IKingpinStateReporter>, IRefresh
{
    protected override void HandleModelUpdate(IKingpinStateReporter? oldValue, IKingpinStateReporter? newValue)
    {
        base.HandleModelUpdate(oldValue, newValue);
        Refresh();
    }

    private string _alias = string.Empty;

    public string Alias
    {
        get { return _alias; }
        private set
        {
            if (_alias != value)
            {
                _alias = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    private IPAddress? _ipAddress = null;

    public IPAddress? IPAddress
    {
        get { return _ipAddress; }
        private set
        {
            if (_ipAddress != value)
            {
                _ipAddress = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    private MovementType _currentMovementType = MovementType.Stationary;

    public MovementType CurrentMovementType
    {
        get { return _currentMovementType; }
        private set
        {
            if (_currentMovementType != value)
            {
                _currentMovementType = value;
                OnNotifyPropertyChanged();
            }
        }
    }


    private float _x = float.NaN;

    public float X
    {
        get { return _x; }
        private set
        {
            if (_x != value)
            {
                _x = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    private float _y = float.NaN;

    public float Y
    {
        get { return _x; }
        private set
        {
            if (_y != value)
            {
                _y = value;
                OnNotifyPropertyChanged();
            }
        }
    }


    private float _heading = float.NaN;

    public float Heading
    {
        get { return _heading; }
        private set
        {
            if (_heading != value)
            {
                _heading = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    private bool _isInFault = false;

    public bool IsInFault
    {
        get { return _isInFault; }
        private set
        {
            if (_isInFault != value)
            {
                _isInFault = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    public void Refresh()
    {
        IKingpinState? toProcess = Model?.KingpinState;

        if (toProcess != null)
        {
            Alias = toProcess.Alias;
            _ipAddress = toProcess.IPAddress;
            IsVirtual = toProcess.IsVirtual;

            CurrentMovementType = toProcess.CurrentMovementType;

            X = toProcess.X;
            Y = toProcess.Y;
            Heading = toProcess.Heading;

            DynamicLimiterStatus = toProcess.DynamicLimiterStatus;
            NavigationStatus = toProcess.NavigationStatus;
            PositionControlStatus = toProcess.PositionControlStatus;
            IsInFault = toProcess.IsInFault();
        }
        else
        {
            Alias = string.Empty;
            IPAddress = null;
            IsVirtual = false;

            CurrentMovementType = MovementType.Stationary;

            X = float.NaN;
            Y = float.NaN;
            Heading = float.NaN;

            DynamicLimiterStatus = DynamicLimiterStatus.Unknown;
            NavigationStatus = NavigationStatus.Unknown;
            PositionControlStatus = PositionControlStatus.Unknown;
            IsInFault = false;
        }


    }

    private DynamicLimiterStatus _dynamicLimiterStatus = DynamicLimiterStatus.Unknown;

    private NavigationStatus _navigationStatus = NavigationStatus.Unknown;

    private PositionControlStatus _positionControlStatus = PositionControlStatus.Unknown;

    public NavigationStatus NavigationStatus
    {
        get { return _navigationStatus; }
        private set
        {
            if (_navigationStatus != value)
            {
                _navigationStatus = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    public PositionControlStatus PositionControlStatus
    {
        get { return _positionControlStatus; }
        private set
        {
            if (_positionControlStatus != value)
            {
                _positionControlStatus = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    private bool _isVirtual = false;

    public bool IsVirtual
    {
        get { return _isVirtual; }
        private set
        {
            if (_isVirtual != value)
            {
                _isVirtual = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    public DynamicLimiterStatus DynamicLimiterStatus
    {
        get { return _dynamicLimiterStatus; }
        private set
        {
            if (_dynamicLimiterStatus != value)
            {
                _dynamicLimiterStatus = value;
                OnNotifyPropertyChanged();
            }
        }
    }
}