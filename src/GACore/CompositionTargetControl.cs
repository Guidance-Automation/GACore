using GACore.Architecture;
using System.Windows.Controls;
using System.Windows.Media;

namespace GACore;

public class CompositionTargetControl : UserControl, IDisposable
{
    private byte _frameCount = 0;

    private bool _isDisposed = false;

    public CompositionTargetControl(byte onFrames = 1)
    {
        OnFrames = onFrames;
        CompositionTarget.Rendering += CompositionTarget_Rendering;
    }

    ~CompositionTargetControl()
    {
        Dispose(false);
    }

    public byte OnFrames { get; private set; } = 1;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void CompositionTarget_Rendering(object? sender, EventArgs e)
    {
        if ((_frameCount % OnFrames) == 0 && DataContext is IRefresh refresh)
            refresh.Refresh();

        _frameCount++;
    }

    private void Dispose(bool _)
    {
        if (_isDisposed) return;

        CompositionTarget.Rendering -= CompositionTarget_Rendering;

        _isDisposed = true;
    }
}
