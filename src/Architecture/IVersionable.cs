using GAAPICommon.Messages;

namespace GACore.Architecture;

public interface IVersionable
{
    public SemVer Version { get; }
}