using NLog;
using NLog.Config;
using NLog.Targets;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GACore.NLog;

/// <summary>
/// Singleton wrapper around NLog to manage loggers.
/// </summary>
public class NLogManager : INotifyPropertyChanged
{
    private LogLevel _logLevel = LogLevel.Info;
    private string _logDir = Path.Combine([Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"Guidance Automation", @"Logs"]);

    public event PropertyChangedEventHandler? PropertyChanged;

    public string LogDir
    {
        get { return _logDir; }

        set
        {
            if (string.IsNullOrEmpty(value)) throw new InvalidOperationException($"{nameof(_logDir)} failed to set.");

            if (_logDir != value)
            {
                _logDir = value;
                OnNotifyPropertyChanged();
            }
        }
    }

    public static NLogManager Instance { get; } = new();

    private readonly object _lockObject = new();

    public NLogManager()
    {
    }

    public static IEnumerable<Logger> GetLoggers()
    {
        List<Logger> loggers = [];

        if (LogManager.Configuration != null)
        {
            foreach (Target target in LogManager.Configuration.AllTargets.ToList())
            {
                loggers.Add(LogManager.GetLogger(target.Name));
            }
        }

        return loggers;
    }

    public LogLevel LogLevel
    {
        get { return _logLevel; }

        set
        {
            lock (_lockObject)
            {
                if (_logLevel != value)
                {
                    _logLevel = value;

                    if (LogManager.Configuration == null)
                    {
                        HandleNullConfiguration();
                    }
                    foreach (LoggingRule rule in LogManager.Configuration!.LoggingRules)
                    {
                        rule.SetLoggingLevels(_logLevel, LogLevel.Fatal);
                    }

                    LogManager.ReconfigExistingLoggers();
                    OnNotifyPropertyChanged();
                }
            }
        }
    }

    public Logger? GetFileTargetLogger(string name)
    {
        lock (_lockObject)
        {
            if (LogManager.Configuration == null) HandleNullConfiguration();

            name = name.ToLowerInvariant();
            Logger? existing = GetLoggers().FirstOrDefault(e => e.Name == name);

            if (existing != null) return existing;

            string fileName = Path.Combine([LogDir, name + ".log"]);

            FileInfo info = new(fileName);

            if (info.Directory != null && !info.Directory.Exists) Directory.CreateDirectory(info.Directory.FullName);

            FileTarget target = TargetFactory.GetDefaultFileTarget(name, fileName);

            LoggingRule rule = new(name, LogLevel, target);
            LogManager.Configuration?.LoggingRules.Add(rule);
            LogManager.Configuration?.AddTarget(target);

            LogManager.ReconfigExistingLoggers();

            return GetLoggers().FirstOrDefault(e => e.Name == name);
        }
    }

    private void HandleNullConfiguration()
    {
        LogManager.Configuration = new LoggingConfiguration();
        Directory.CreateDirectory(LogDir);
    }

    protected void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}