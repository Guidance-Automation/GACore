using GACore.NLog;
using NLog;
using NUnit.Framework;

namespace GACore.Test.NLog;

[TestFixture]
public class TNLogManager
{
    [Test]
    public void GetFileTargetLogger()
    {
        NLogManager manager = NLogManager.Instance;

        manager.LogDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        Logger? logger = manager.GetFileTargetLogger("TNLogManager_GetFileTargetLogger");

        logger?.WriteValidateLoglevels();
        Assert.That(File.Exists(logger?.GetFilePath()));
    }
}