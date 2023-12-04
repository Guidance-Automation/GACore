using GAAPICommon.Architecture;
using GAAPICommon.Core.Dtos;
using GACore.Architecture;
using NUnit.Framework;

namespace GACore.Test;

[TestFixture]
public class TExtensionMethods
{
    [Test]
    public void ToSemVerDto()
    {
        ISemVer semVer = new SemVer(1, 2, 3, ReleaseFlag.Beta);

        SemVerDto semVerDto = semVer.ToSemVerDto();

        Assert.That(semVerDto, Is.Not.Null);

        Assert.That(semVerDto.Major, Is.EqualTo(1));
        Assert.That(semVerDto.Minor, Is.EqualTo(2));
        Assert.That(semVerDto.Patch, Is.EqualTo(3));

        Assert.That(string.Equals(semVerDto.ReleaseFlag, "Beta", StringComparison.OrdinalIgnoreCase));
    }
}