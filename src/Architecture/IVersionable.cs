using GAAPICommon.Messages;

namespace GACore.Architecture;

public interface IVersionable
{
    public SemVerDto Version { get; }
}