namespace GACore;

/// <summary>
/// Creates a temporary file with the specified extension in the system's temporary directory.
/// </summary>
public class TemporaryFile : IDisposable
{
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
    /// Defaults to ".xxx" if no extension is provided.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown if the provided extension contains invalid characters.
    /// </exception>
    /// <exception cref="UnauthorizedAccessException">
    /// Thrown if the application does not have permission to create files in the temporary directory.
    /// </exception>
    public TemporaryFile(string extension = ".xxx")
    {
        FilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + extension);
        using (File.Create(FilePath)) { }
    }

    /// <summary>
    /// Deletes the temporary file associated with this instance.
    /// This method is automatically called when the instance is disposed.
    /// </summary>
    public void Dispose()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
        catch (Exception) { }
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Implicitly converts a <see cref="TemporaryFile"/> instance to its file path.
    /// This allows a <see cref="TemporaryFile"/> object to be used as a string directly.
    /// </summary>
    /// <param name="tempFile">The <see cref="TemporaryFile"/> instance to convert.</param>
    /// <returns>The full path to the temporary file.</returns>
    public static implicit operator string(TemporaryFile tempFile) => tempFile.FilePath;
}