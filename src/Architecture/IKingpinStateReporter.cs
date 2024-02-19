using GAAPICommon.Messages;

namespace GACore.Architecture;

public interface IKingpinStateReporter
{
    public KingpinState? KingpinState { get; }
}