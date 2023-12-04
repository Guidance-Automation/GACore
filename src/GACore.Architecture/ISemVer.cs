using GAAPICommon.Architecture;

namespace GACore.Architecture;

/// <summary>
/// SemVer interpretation
/// </summary>
public interface ISemVer : IComparable
{
    public int Major { get; }

    public int Minor { get; }

    public int Patch { get; }

    public ReleaseFlag ReleaseFlag { get; }
}