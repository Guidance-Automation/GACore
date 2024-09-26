using GAAPICommon.Interfaces;

namespace GACore.Architecture;

public interface IKingpinStateReporter
{
    public IKingpinState? KingpinState { get; }
}