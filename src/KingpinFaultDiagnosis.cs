using GAAPICommon;
using GAAPICommon.Enums;
using GAAPICommon.Interfaces;
using System.Text;

namespace GACore;

/// <summary>
/// An an object to capture the current fault state of a Kingpin
/// </summary>
public class KingpinFaultDiagnosis
{
    public KingpinFaultDiagnosis(IKingpinState kingpinState)
    {
        ArgumentNullException.ThrowIfNull(kingpinState);

        DynamicLimiterFault = kingpinState.DynamicLimiterStatus.IsFault() ?
            kingpinState.DynamicLimiterStatus : null;

        ExtendedDataFault = kingpinState.ExtendedDataFaultStatus.IsFault() ?
            kingpinState.ExtendedDataFaultStatus : null;

        NavigationFault = kingpinState.NavigationStatus.IsFault() ?
            kingpinState.NavigationStatus : null;

        PCSFault = kingpinState.PositionControlStatus.IsFault() ?
            kingpinState.PositionControlStatus : null;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(DynamicLimiterFault, ExtendedDataFault, NavigationFault, PCSFault);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not KingpinFaultDiagnosis other) return false;

        return DynamicLimiterFault.Equals(other.DynamicLimiterFault)
            && ExtendedDataFault.Equals(other.ExtendedDataFault)
            && NavigationFault.Equals(other.NavigationFault)
            && PCSFault.Equals(other.PCSFault);
    }

    public string ToDiagnosticString()
    {
        if (IsInFault())
        {
            StringBuilder builder = new();
            builder.Append("Kingpin faults detected:");

            if (DynamicLimiterFault != null) builder.AppendFormat(" {0}", DynamicLimiterFault);
            if (ExtendedDataFault != null) builder.AppendFormat(" {0}", ExtendedDataFault);
            if (NavigationFault != null) builder.AppendFormat(" {0}", NavigationFault);
            if (PCSFault != null) builder.AppendFormat(" {0}", PCSFault);

            return builder.ToString();
        }
        else
        {
            return "No kingpin fault detected";
        }
    }

    public override string ToString()
    {
        return ToDiagnosticString();
    }

    public bool IsInFault()
    {
        return (PCSFault != null || ExtendedDataFault != null
        || NavigationFault != null || DynamicLimiterFault != null);
    }

    public PositionControlStatus? PCSFault { get; }

    public ExtendedDataFaultStatus? ExtendedDataFault { get; }

    public NavigationStatus? NavigationFault { get; }

    public DynamicLimiterStatus? DynamicLimiterFault { get; }
}