using System.Drawing;

namespace GACore;

/// <summary>
/// Lightweight structure to tightly couple a foreground and background brush, and associated text.
/// </summary>
public readonly struct BrushCollection(string text, Color foreground, Color background)
{
    public readonly string ToBrushCollectionString()
    {
        return string.Format("Text:{0} Foreground:{1} Background:{2}", Text, Foreground, Background);
    }

    public override readonly string ToString()
    {
        return ToBrushCollectionString();
    }

    public Color Background { get; } = background;

    public Color Foreground { get; } = foreground;

    public string Text { get; } = text;
}