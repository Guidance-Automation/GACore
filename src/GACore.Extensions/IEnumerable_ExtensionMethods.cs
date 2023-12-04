using System;
using System.Collections.Generic;
using System.Linq;

namespace GACore.Extensions;

public static class IEnumerable_ExtensionMethods
{
    internal static Random Random { get; } = new Random(Guid.NewGuid().GetHashCode());

    /// <summary>
    /// Randomizes the contents of an IEnumerable
    /// </summary>
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> enumerable)
	{
		if (enumerable == null) return null;

		List<T> dataSet = new(enumerable);
		List<T> randomDataSet = [];

		for (int i = dataSet.Count; i > 0; i--)
		{
			int randomIndex = Random.Next(0, i);
			randomDataSet.Add(dataSet[randomIndex]);
			dataSet.RemoveAt(randomIndex);
		}

		return randomDataSet;
	}

    /// <summary>
    /// Clones an IEnumerable<T> where T : ICloneable
    /// </summary>
    public static IEnumerable<T> Clone<T>(this IEnumerable<T> enumerableToClone) where T : ICloneable
    {
        return enumerableToClone.ToList().Select(e => (T)e.Clone()).ToList();
    }
}