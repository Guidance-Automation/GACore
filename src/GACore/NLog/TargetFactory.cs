using NLog.Targets;
using System.IO;

namespace GACore.NLog;

public static class TargetFactory
{
	public static FileTarget GetDefaultFileTarget(string name, string fileName)
	{
		if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

		if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));

		FileTarget target = new(name)
		{
			FileName = fileName,
			Layout = LayoutFactory.DefaultLayout
		};

		target.SetDefaultArchiveSettings(fileName);

		return target;
	}

	private static void SetDefaultArchiveSettings(this FileTarget fileTarget, string fileName)
	{
		string? directory = Path.GetDirectoryName(fileName) ?? throw new InvalidOperationException("File name is invalid");
        string archiveDirectory = Path.Combine(directory, "archives");

		DirectoryInfo info = new(archiveDirectory);

		if (!info.Exists) Directory.CreateDirectory(archiveDirectory);

		string archiveFilename = string.Format(@"{{##}}_{0}", Path.GetFileName(fileName));
		string archiveFullPath = Path.Combine(archiveDirectory, archiveFilename);

		fileTarget.ArchiveFileName = archiveFullPath;
		fileTarget.ArchiveNumbering = ArchiveNumberingMode.Rolling;
		fileTarget.ArchiveAboveSize = 10000000;
		fileTarget.MaxArchiveFiles = 3;
	}
}