using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;

namespace GACore.Extensions.Test;

[TestFixture]
[Category("ExtensionMethods")]
[SupportedOSPlatform("windows")]
public class TIEnumerable_ExtensionMethods
{
    /// <summary>
    /// Tests extension method to randomize an IEnumerable.
    /// </summary>
    [Test]
    public void IEnumerable_Randomize()
    {
        IEnumerable<double> refData = GenerateOrderedIEnumerable(100);
        IEnumerable<double> randomizedData = refData.Randomize();

        foreach (double value in refData)
        {
            Assert.That(randomizedData.Contains(value));
        }

        Assert.That(refData, Is.Not.EqualTo(randomizedData));
    }

    private IEnumerable<double> GenerateOrderedIEnumerable(int numElements = 100)
    {
        List<double> dataSet = [];

        for (int i = 0; i < numElements; i++) dataSet.Add(i);

        return dataSet;
    }
}