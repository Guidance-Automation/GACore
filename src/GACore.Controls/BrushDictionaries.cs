using GAAPICommon.Architecture;
using System.Collections.Generic;
using System.Windows.Media;

namespace GACore.Controls;

public static class BrushDictionaries
{
    public static Dictionary<PositionControlStatus, BrushCollection> PositionControlStatusBackgroundBrushCollectionDictionary { get; } = new Dictionary<PositionControlStatus, BrushCollection>
    {
        {PositionControlStatus.OK, new BrushCollection("OK", Brushes.Black, Brushes.Green) },
        {PositionControlStatus.Disabled, new BrushCollection("Disabled", Brushes.Silver, Brushes.Black) },
        {PositionControlStatus.Disabling, new BrushCollection("Disabling", Brushes.Silver, Brushes.Black) },
        {PositionControlStatus.NoWaypoints, new BrushCollection("No Waypoints", Brushes.Silver, Brushes.Yellow) },
        {PositionControlStatus.OutOfPosition, new BrushCollection("Out Of Position", Brushes.Black, Brushes.Orange) },
        {PositionControlStatus.WaypointDiscontinuity, new BrushCollection("Waypoint Discontinuity", Brushes.Black, Brushes.Crimson) }
    };
    public static Dictionary<DynamicLimiterStatus, BrushCollection> DynamicLimiterStatusBrushCollectionDictionary { get; } = new Dictionary<DynamicLimiterStatus, BrushCollection>
    {
        { DynamicLimiterStatus.OK, new BrushCollection("OK", Brushes.Black, Brushes.Green) },
        { DynamicLimiterStatus.SafetySensor, new BrushCollection("Safety Sensor", Brushes.Black, Brushes.Yellow) },
        { DynamicLimiterStatus.Warning_1, new BrushCollection("Warning 1", Brushes.Black, Brushes.Yellow) },
        { DynamicLimiterStatus.Warning_2, new BrushCollection("Warning 2", Brushes.Black, Brushes.Yellow) },
        { DynamicLimiterStatus.MotorFault, new BrushCollection("Motor Fault", Brushes.Black, Brushes.Crimson) },
        { DynamicLimiterStatus.FastStop, new BrushCollection("Fast Stop", Brushes.Black, Brushes.Yellow) },
        { DynamicLimiterStatus.GoSlow, new BrushCollection("Go Slow", Brushes.Black, Brushes.Yellow) }
    };
    public static Dictionary<NavigationStatus, BrushCollection> NavigationStatusBackgroundBrushCollectionDictionary { get; } = new Dictionary<NavigationStatus, BrushCollection>
    {
        { NavigationStatus.OK, new BrushCollection("OK", Brushes.Black, Brushes.Green) },
        { NavigationStatus.Lost, new BrushCollection("Lost", Brushes.Black, Brushes.Crimson) },
        { NavigationStatus.AssociationFailure, new BrushCollection("Association Failure", Brushes.Black, Brushes.Crimson) },
        { NavigationStatus.HighUncertainty, new BrushCollection("High Uncertainty", Brushes.Black, Brushes.Orange) },
        { NavigationStatus.PoorAssociaton, new BrushCollection("Poor Association", Brushes.Black, Brushes.Yellow) },
        { NavigationStatus.NoResponse, new BrushCollection("No Response", Brushes.Black, Brushes.Crimson) },
        { NavigationStatus.NoScannerData, new BrushCollection("No Scanner Data", Brushes.Black, Brushes.Crimson) }
    };
}