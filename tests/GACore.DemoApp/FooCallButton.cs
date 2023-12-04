using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace GACore.DemoApp;

public class FooCallButton : INotifyPropertyChanged
{
	private LightState? _lightState = null;

	private bool? _isPressed = null;

	[SupportedOSPlatform("windows")]
	public void Randomize()
	{
		LightState = Tools.RandomEnumValue<LightState>();
		IsPressed = Tools.Random.Next(0, 2) < 1;
	}

	public LightState? LightState
	{
		get { return _lightState; }
		set
		{
			if (_lightState != value)
			{
				_lightState = value;
				OnNotifyPropertyChanged();
			}
		}
	}

	public bool? IsPressed
	{
		get { return _isPressed; }
		set
		{
			if (_isPressed != value)
			{
				_isPressed = value;
				OnNotifyPropertyChanged();
			}
		}
	}

	public event PropertyChangedEventHandler? PropertyChanged;

	private void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}