using GAAPICommon.Messages;

namespace GACore.Architecture;

public static class ExtensionMethods
{
	/// <summary>
	/// Converts to string representation
	/// </summary>
	/// <param name="semVer"></param>
	/// <returns>string in the format v1.2.3.2 (Major = 1, Minor = 2, Patch = 3, Release Candidate</returns>
	public static string ToSemVerVersionString(this SemVer semVer)
	{
        ArgumentNullException.ThrowIfNull(semVer);

        return $"v{semVer.Major}.{semVer.Minor}.{semVer.Patch}.{semVer.ReleaseFlag}";
	}
}