namespace GACore.Controls;

public static class ExtensionMethods
{
	public static object? GetProperty(this BrushCollection brushCollection, BrushCollectionProperty brushCollectionProperty)
	{
		switch (brushCollectionProperty)
		{
			case BrushCollectionProperty.Text: return brushCollection.Text;

			case BrushCollectionProperty.Foreground: return brushCollection.Foreground;

			case BrushCollectionProperty.Background: return brushCollection.Background;

			default: return null;
		}
	}
}