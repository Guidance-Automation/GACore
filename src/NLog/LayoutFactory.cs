namespace GACore.NLog;

public static class LayoutFactory
{
    public static string ProcessLayout { get; } = @"${processtime} ${level: padding = -8:padCharacter = } ${message} ${exception:format=tostring}";

    public static string LongDateLayout { get; } = @"${longdate} ${level: padding = -8:padCharacter = } ${message} ${exception:format=tostring}";

    public static string DefaultLayout
    {
        get
        {
            return LongDateLayout;
        }
    }
}