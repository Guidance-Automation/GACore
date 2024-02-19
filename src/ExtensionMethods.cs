using System.Drawing;
using GAAPICommon.Messages;

namespace GACore;

public static class ExtensionMethods
{
    public static Color ToColor(this LightState? lightState)
    {
        switch (lightState)
        {
            case LightState.Green:
                return Color.Green;

            case LightState.Amber:
                return Color.Orange;

            case LightState.Red:
                return Color.Red;

            case LightState.Off:
            default:
                return Color.White;
        }
    }

    public static KingpinFaultDiagnosis Diagnose(this KingpinStateDto kingpinState) => new(kingpinState);

    public static BrushCollection GetBrushCollection<T>(this Dictionary<T, BrushCollection> dictionary, T key) where T : notnull
    {
        if (dictionary.TryGetValue(key, out BrushCollection value))
            return value;

        return new BrushCollection("Unknown", Color.Crimson, Color.White);
    }
}