using GAAPICommon.Architecture;

namespace GACore.Architecture;

public interface IKingpinStateReporter
{
    public IKingpinState? KingpinState { get; }
}