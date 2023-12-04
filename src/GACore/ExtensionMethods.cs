using GACore.Architecture;
using System.Windows.Media;
using GAAPICommon.Core.Dtos;
using GAAPICommon.Architecture;

namespace GACore;

public static class ExtensionMethods
{
    private static readonly Dictionary<ReleaseFlag, string> _releaseFlagDictionary = new()
    {
        {ReleaseFlag.Alpha, "Alpha" },
        {ReleaseFlag.Beta, "Beta" },
        {ReleaseFlag.ReleaseCandidate, "Release candidate" },
        {ReleaseFlag.Release, "Release" }
    };

    public static FleetManagementMetadataDto ToFleetManagementMetadataDto(this FleetManagementMetadata metadata)
    {
        ArgumentNullException.ThrowIfNull(metadata);

        return new FleetManagementMetadataDto()
        {
            ProductName = metadata.ProductName,
            SemVer = metadata.Version.ToSemVerDto()
        };
    }

    public static SemVerDto ToSemVerDto(this ISemVer semVer)
    {
        ArgumentNullException.ThrowIfNull(semVer);

        return new SemVerDto()
        {
            Major = semVer.Major,
            Minor = semVer.Minor,
            Patch = semVer.Patch,
            ReleaseFlag = _releaseFlagDictionary[semVer.ReleaseFlag]
        };
    }

    public static Color ToColor(this LightState? lightState)
    {
        switch (lightState)
        {
            case LightState.Green:
                return Colors.Green;

            case LightState.Amber:
                return Colors.Orange;

            case LightState.Red:
                return Colors.Red;

            case LightState.Off:
            default:
                return Colors.White;
        }
    }

    public static KingpinFaultDiagnosis Diagnose(this IKingpinState kingpinState) => new(kingpinState);

    public static BrushCollection GetBrushCollection<T>(this Dictionary<T, BrushCollection> dictionary, T key) where T : notnull
    {
        if (dictionary.TryGetValue(key, out BrushCollection value))
            return value;

        return new BrushCollection("Unknown", Brushes.Crimson, Brushes.White);
    }
}