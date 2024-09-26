using GACore.Extensions.Test.TestObjects;
using NUnit.Framework;
using System.Runtime.Versioning;

namespace GACore.Extensions.Test;

[TestFixture]
[Category("ExtensionMethods")]
[SupportedOSPlatform("windows")]
public class TList_ExtensionMethods
{
    [Test]
    [TestCase(3, 2)]
    public void GroupedList_CountDoubleGroup(int groupACount, int groupBCount)
    {
        int expectedCount = groupACount + groupBCount;
        int expectedGroups = (groupACount > 0 ? 1 : 0) + (groupBCount > 0 ? 1 : 0);

        List<AbstractFoo> sourceList = [];

        for (int i = 0; i < groupACount; i++) sourceList.Add(new FooA());

        for (int i = 0; i < groupBCount; i++) sourceList.Add(new FooB());

        Assert.That(sourceList.Count, Is.EqualTo(expectedCount));
        Assert.That(sourceList.CountNextGroup(), Is.EqualTo(groupACount));
        Assert.That(sourceList.CountTotalGroups(), Is.EqualTo(expectedGroups));
    }

    [Test]
    public void GroupedList_CountSingleGroup()
    {
        List<AbstractFoo> sourceList =
        [
            new FooA(), new FooA(), new FooA()
        ];

        Assert.That(sourceList.Count, Is.EqualTo(3));
        Assert.That(sourceList.CountNextGroup(), Is.EqualTo(3));
        Assert.That(sourceList.CountTotalGroups(), Is.EqualTo(1));
    }

    [Test]
    public void GroupedList_CountTripleGroup()
    {
        List<AbstractFoo> sourceList =
        [
            new FooA(), new FooA(), new FooA(),
            new FooB(), new FooB(),
            new FooA(), new FooA(), new FooA()
        ];

        Assert.That(sourceList.Count, Is.EqualTo(8));
        Assert.That(sourceList.CountNextGroup(), Is.EqualTo(3));
        Assert.That(sourceList.CountTotalGroups(), Is.EqualTo(3));
    }

    [Test]
    public void GroupedList_DelistDoubleGroup()
    {
        List<AbstractFoo> sourceList =
        [
            new FooA(), new FooA(),
            new FooB(), new FooB(), new FooB()
        ];

        IList<AbstractFoo> delisted = sourceList.DelistGroup();

        Assert.That(sourceList.Count, Is.EqualTo(3));
        Assert.That(sourceList.CountNextGroup(), Is.EqualTo(3));
        Assert.That(sourceList.CountTotalGroups(), Is.EqualTo(1));
        Assert.That(delisted.Count, Is.EqualTo(2));
        Assert.That(delisted.CountNextGroup(), Is.EqualTo(2));
        Assert.That(delisted.CountTotalGroups(), Is.EqualTo(1));
    }

    [Test]
    public void GroupedList_Empty()
    {
        List<AbstractFoo> sourceList = [];
        Assert.That(sourceList.CountNextGroup(), Is.EqualTo(0));
    }

    [Test]
    public void GroupedList_DelistEmpty()
    {
        List<AbstractFoo> sourceList = [];

        Assert.That(sourceList.CountNextGroup(), Is.EqualTo(0));

        List<AbstractFoo> delisted = sourceList.DelistGroup();

        Assert.That(delisted.Count, Is.EqualTo(0));
        Assert.That(delisted.CountNextGroup(), Is.EqualTo(0));
    }

    [Test]
    public void GroupedList_DelistSingleGroup()
    {
        List<AbstractFoo> sourceList =
        [
            new FooA(), new FooA()
        ];

        List<AbstractFoo> delisted = sourceList.DelistGroup();

        Assert.That(sourceList.Count, Is.EqualTo(0));
        Assert.That(sourceList.CountNextGroup(), Is.EqualTo(0));

        Assert.That(delisted.Count, Is.EqualTo(2));
        Assert.That(delisted.CountNextGroup(), Is.EqualTo(2));
        Assert.That(delisted.CountTotalGroups(), Is.EqualTo(1));
    }

    [Test]
    public void GroupedList_DelistTripleGroup()
    {
        List<AbstractFoo> sourceList =
        [
            new FooA(),
            new FooA(),
            new FooB(),
            new FooB(),
            new FooB(),
            new FooA(),
            new FooA(),
        ];

        Assert.That(sourceList.CountTotalGroups(), Is.EqualTo(3));

        List<AbstractFoo> delisted = sourceList.DelistGroup();

        Assert.That(sourceList.Count, Is.EqualTo(5));
        Assert.That(sourceList.CountNextGroup(), Is.EqualTo(3));
        Assert.That(sourceList.CountTotalGroups(), Is.EqualTo(2));

        Assert.That(delisted.Count, Is.EqualTo(2));
        Assert.That(delisted.CountNextGroup(), Is.EqualTo(2));
        Assert.That(delisted.CountTotalGroups(), Is.EqualTo(1));
    }
}