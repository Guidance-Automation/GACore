using NLog;

namespace GACore;

/// <summary>
/// Creates a temporary file with the specified extension in the system's temporary directory.
/// </summary>
public class TemporaryFile : IDisposable
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly int _deleteMaxRetries;
    private readonly int _deleteRetryDelay;

    /// <summary>
    /// Gets the full path to the temporary file created by this instance.
    /// </summary>
    public string FilePath { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TemporaryFile"/> class.
    /// Creates a temporary file with the specified extension in the system's temporary directory.
    /// </summary>
    /// <param name="extension">
    /// The file extension to use for the temporary file (e.g., ".txt"). 
    /// Defaults to ".tmp" if no extension is provided.
    /// </param>
    /// <param name="deleteMaxRetries">
    /// Upon calling Dispose, how many times to reattempt the delete of the file.
    /// </param>
    /// <param name="deleteRetryDelay">
    /// The delay in seconds between each delete attempt.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown if the provided extension contains invalid characters.
    /// </exception>
    /// <exception cref="UnauthorizedAccessException">
    /// Thrown if the application does not have permission to create files in the temporary directory.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the application fails to retrieve the directory of the created file, hence cannot create a watcher.
    /// </exception>
    public TemporaryFile(string extension = ".tmp", int deleteMaxRetries = 10, int deleteRetryDelay = 1000)
    {
        extension = string.IsNullOrWhiteSpace(extension) ? ".tmp" : extension;
        if (!extension.StartsWith('.'))
        {
            extension = "." + extension;
        }

        _deleteMaxRetries = deleteMaxRetries;
        _deleteRetryDelay = deleteRetryDelay;
        FilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + extension);
        using (File.Create(FilePath)) { }
    }

    ~TemporaryFile()
    {
        Dispose();
    }

    /// <summary>
    /// Deletes the temporary file associated with this instance.
    /// This method is automatically called when the instance is disposed.
    /// </summary>
    public void Dispose()
    {
        try
        {
            TryDeleteFile();
        }
        catch (Exception e)
        {
            _logger.Error($"Exception occurred when deleting: {e.Message}");
        }
        finally
        {
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Attempts to delete the file, retrying if it is currently in use.
    /// </summary>
    private void TryDeleteFile()
    {
        Task.Run(() =>
        {
            if (File.Exists(FilePath))
            {
                for (int attempt = 0; attempt < _deleteMaxRetries; attempt++)
                {
                    try
                    {
                        File.Delete(FilePath);
                        return;
                    }
                    catch (IOException)
                    {
                        int delay = _deleteRetryDelay * attempt;
                        _logger.Warn($"File is locked. Retry {attempt} in {delay} ms...");
                        Thread.Sleep(delay);
                    }
                }
            }
        });
    }

    /// <summary>
    /// Implicitly converts a <see cref="TemporaryFile"/> instance to its file path.
    /// This allows a <see cref="TemporaryFile"/> object to be used as a string directly.
    /// </summary>
    /// <param name="tempFile">The <see cref="TemporaryFile"/> instance to convert.</param>
    /// <returns>The full path to the temporary file.</returns>
    public static implicit operator string(TemporaryFile tempFile) => tempFile.FilePath;
}