using GAAPICommon.Messages;

namespace GACore.Architecture;

public interface IKingpinStateReporter
{
    public KingpinStateDto? KingpinState { get; }
}