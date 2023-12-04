namespace GACore.UI.Message;

public class DialogShutdownMessage<T>(T dialogSource) where T : System.Enum
{
    public T DialogSource { get; } = dialogSource;
}