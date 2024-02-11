using NLog;

namespace GACore.NLog;

/// <summary>
/// Factory class for standard loggers. 
/// </summary>
public static class LoggerFactory
{
	public static Logger? GetStandardLogger(StandardLogger standardLogger)
	{
		return NLogManager.Instance?.GetFileTargetLogger(standardLogger.ToString());
	}
}