using GAAPICommon.Architecture;
using GACore.Architecture;
using System.Runtime.Versioning;

namespace GACore.DemoApp;

public class FooKingpin : IKingpinStateReporter
{
	private IKingpinState _kingpinState = null;

	public IKingpinState KingpinState
	{
		get { return _kingpinState; }
		set 
		{ 
			_kingpinState = value; 
		}
	}

    [SupportedOSPlatform("windows")]
    public void Randomize()
	{
		KingpinState = new FooKingpinState();
	}

    [SupportedOSPlatform("windows")]
    public void SetGood()
	{
		KingpinState = FooKingpinState.FromGood();
	}
}