using System.Windows.Media;

namespace GACore;

/// <summary>
/// Lightweight structure to tightly couple a foreground and background brush, and associated text.
/// </summary>
public readonly struct BrushCollection
{
	public BrushCollection(string text, Brush foreground, Brush background)
	{
		if (string.IsNullOrEmpty(text)) throw new ArgumentOutOfRangeException(nameof(text));

        ArgumentNullException.ThrowIfNull(foreground);

        ArgumentNullException.ThrowIfNull(background);

        Text = text;
		Foreground = foreground;
		Background = background;
	}

    public readonly string ToBrushCollectionString()
    {
        return string.Format("Text:{0} Foreground:{1} Background:{2}", Text, Foreground, Background);
    }

    public override readonly string ToString()
    {
        return ToBrushCollectionString();
    }

    public Brush Background { get; }

	public Brush Foreground { get; }

	public string Text { get; }
}