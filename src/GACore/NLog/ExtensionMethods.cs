using NLog;
using NLog.Targets;
using System;
using System.Diagnostics;

namespace GACore.NLog;

public static class ExtensionMethods
{
	public static void StartFilePathLoggerProcess(this Logger logger)
	{
		string filePath = logger.GetFilePath();

		if (!string.IsNullOrEmpty(filePath)) 
			Process.Start(filePath);
	}

	public static string GetFilePath(this Logger logger)
	{
		Target target = LogManager.Configuration.FindTargetByName(logger.Name);

		if (target != null && target is FileTarget)
		{
			FileTarget fileTarget = target as FileTarget;
			LogEventInfo eventInfo = new() { };
			return fileTarget.FileName.Render(eventInfo);
		}

		return null;
	}

	public static void WriteEnvironment(this Logger logger)
	{
		logger.Info("UserName: {0}", Environment.UserName);
		logger.Info("MachineName: {0}", Environment.MachineName);
		logger.Info("OSVersion: {0}", Environment.OSVersion);
	}

	public static void WriteValidateLoglevels(this Logger logger)
	{
		logger.Fatal("Fatal Test Message");
		logger.Error("Error Test Message");
		logger.Warn("Warn Test Message");
		logger.Info("Info Test Message");
		logger.Debug("Debug Test Message");
		logger.Trace("Trace Test Message");
	}
}