using GAAPICommon.Enums;
using GAAPICommon.Messages;
using System.Text.RegularExpressions;

namespace GACore;

public static partial class Tools
{
    public static Random Random { get; } = new Random();

    private static Regex _versionRegex { get; } = VersionRegex();

    public static SemVerDto? ParseVersionString(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        Match match = _versionRegex.Match(value);

        if (match.Success)
        {
            int major = int.Parse(match.Groups["major"].Value);
            int minor = int.Parse(match.Groups["minor"].Value);
            int patch = int.Parse(match.Groups["patch"].Value);
            ReleaseFlag releaseFlag = (ReleaseFlag)int.Parse(match.Groups["releaseFlag"].Value);

            return new SemVerDto(major, minor, patch, releaseFlag);
        }

        return null;
    }

    public static T? RandomEnumValue<T>() where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        return (T?)values.GetValue(Random.Next(values.Length));
    }

    [GeneratedRegex(@"(?<major>\d+)(?:.)(?<minor>\d+)(?:.)(?<patch>\d+)(?:.)(?<releaseFlag>\d)")]
    private static partial Regex VersionRegex();
}