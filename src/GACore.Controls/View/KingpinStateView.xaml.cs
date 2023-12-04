using GACore.Architecture;
using System.Windows.Controls;
using System.Windows.Media;

namespace GACore.Controls.View;

/// <summary>
/// Interaction logic for KingpinStateView.xaml
/// </summary>
public partial class KingpinStateView : UserControl
{
    public KingpinStateView()
    {
        InitializeComponent();

#if DEBUG
        CompositionTarget.Rendering += CompositionTarget_Rendering;
#endif
    }
#if DEBUG
    private void CompositionTarget_Rendering(object? sender, EventArgs e)
    {
#warning assumed parent will do this

        if (DataContext is IRefresh refresh)
            refresh.Refresh();
    }
#endif

}