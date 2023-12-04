using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;

namespace GACore.Extensions.Test;

[TestFixture]
[Category("ExtensionMethods")]
[SupportedOSPlatform("windows")]
public class TQueue_ExtensionMethods
{
    private static readonly int[] _additions = [1, 2, 3, 4, 5, 6];

    [Test]
    public void DequeueMatching()
    {
        Queue<int> queue = new();
        queue.EnqueueAll(_additions);
        Assert.That(6, Is.EqualTo(queue.Count));
        Assert.That(queue, Does.Contain(4));

        Assert.Throws(typeof(ArgumentOutOfRangeException), delegate { queue.DequeueMatching(i => i == 7); });
        int dequeued = queue.DequeueMatching(i => i == 4);

        Assert.That(dequeued, Is.EqualTo(4));
        Assert.That(queue.Count, Is.EqualTo(5));

        Assert.That(queue, Does.Not.Contain(4));

        Assert.That(queue, Does.Contain(1));
        Assert.That(queue, Does.Contain(2));
        Assert.That(queue, Does.Contain(3));
        Assert.That(queue, Does.Contain(5));
        Assert.That(queue, Does.Contain(6));
    }
}