using NUnit.Framework;

namespace GACore.Test;

[TestFixture]
public class TTemporaryFile
{
    [Test]
    public void ShouldDeleteAfterUsingBlock()
    {
        string tempPath = string.Empty;
        string extension = ".txt";
        using (TemporaryFile tempFile = new(extension))
        {
            tempPath = tempFile;
            Assert.Multiple(() =>
            {
                Assert.That(tempFile.FilePath, Is.Not.Null);
                Assert.That(File.Exists(tempFile.FilePath));
                Assert.That(tempFile.FilePath, Does.EndWith(extension));
            });
        }
        Assert.That(File.Exists(tempPath), Is.False);
    }

    [Test]
    public void SupportsImplicitPathConversion()
    {
        using TemporaryFile tempFile = new(".log");
        string path = tempFile;
        Assert.Multiple(() =>
        {
            Assert.That(tempFile.FilePath, Is.EqualTo(path));
            Assert.That(File.Exists(path));
        });
    }
}
