using GAAPICommon.Architecture;
using NUnit.Framework;

namespace GACore.Test;

[TestFixture]
public class TSemVer
{
    [TestCase(1, 2, 3, ReleaseFlag.Alpha, 1, 2, 3, ReleaseFlag.Alpha, 0)]
    [TestCase(2, 2, 3, ReleaseFlag.Alpha, 1, 2, 3, ReleaseFlag.Beta, 1)]
    [TestCase(1, 2, 4, ReleaseFlag.Alpha, 1, 2, 3, ReleaseFlag.Release, 1)]
    [TestCase(1, 3, 3, ReleaseFlag.Beta, 1, 2, 3, ReleaseFlag.Beta, 1)]
    [TestCase(1, 2, 3, ReleaseFlag.Alpha, 2, 2, 3, ReleaseFlag.Beta, -1)]
    [TestCase(1, 2, 3, ReleaseFlag.Beta, 1, 3, 3, ReleaseFlag.Release, -1)]
    [TestCase(1, 2, 3, ReleaseFlag.Release, 1, 2, 4, ReleaseFlag.Alpha, -1)]
    public void CompareTo(int majorA, int minorA, int patchA, ReleaseFlag releaseFlagA, int majorB, int minorB, int patchB, ReleaseFlag releaseFlagB, int expected)
    {
        SemVer semVerA = new(majorA, minorA, patchA, releaseFlagA);

        SemVer semVerB = new(majorB, minorB, patchB, releaseFlagB);

        Assert.That(expected, Is.EqualTo(semVerA.CompareTo(semVerB)));
    }
}