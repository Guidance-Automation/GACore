using System.Windows;
using System.Windows.Controls;

namespace GACore.Controls;

public class LabelContentControl : ContentControl
{
	public static readonly DependencyProperty HeaderProperty =
				DependencyProperty.Register("Header", typeof(string),
		typeof(LabelContentControl), new PropertyMetadata(string.Empty, HeaderChanged));

	public LabelContentControl()
	{
	}

	public static readonly DependencyProperty LayoutAlignmentProperty =
				DependencyProperty.Register("LayoutAlignment", typeof(HorizontalAlignment),
		typeof(LabelContentControl), new PropertyMetadata(HorizontalAlignment.Right, LayoutAlignmentChanged));

	public HorizontalAlignment LayoutAlignment { get; set; }

	private static void LayoutAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((LabelContentControl)d).LayoutAlignment = (HorizontalAlignment)e.NewValue;
	}

	public string? Header { get; set; }

	private static void HeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((LabelContentControl)d).Header = e.NewValue as string;
	}
}