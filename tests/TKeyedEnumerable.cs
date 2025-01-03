﻿using NUnit.Framework;
using GACore.Architecture;

namespace GACore.Test;

[TestFixture]
public class TKeyedEnumerable
{
    [Test]
    public void Empty()
    {
        IKeyedEnumerable<int> empty = KeyedEnumerable<int>.Empty();

        Assert.Multiple(() =>
        {
            Assert.That(Guid.Empty, Is.EqualTo(empty.Key));
            Assert.That(empty.Items, Is.Empty);
        });
    }

    [Test]
    public void ArgumentOutOfRange()
    {
        List<int> values = [0, 1, 2, 3];
        Assert.Throws<InvalidOperationException>(() =>
        {
            new KeyedEnumerable<int>(Guid.Empty, values);
        });
    }

    [Test]
    public void Populated()
    {
        List<int> values = [0, 1, 2, 3];

        KeyedEnumerable<int> keyed = new(Guid.NewGuid(), values);

        Assert.Multiple(() =>
        {
            Assert.That(Guid.Empty, Is.Not.EqualTo(keyed.Key));
            Assert.That(values, Is.EqualTo(keyed.Items));
        });
    }
}