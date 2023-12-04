using System;
using System.Collections.Generic;
using System.Linq;

namespace GACore.Extensions;

/// <summary>
/// Extension methods for Queue<T> to facilitate group dequeuing.
/// </summary>
/// <typeparam name="T">Generic type specifying type of items in queue</typeparam>
public static class QueueExtensions
{
    public static void EnqueueAll<T>(this Queue<T> queue, IEnumerable<T> additions)
    {
        foreach (T item in additions)
        {
            queue.Enqueue(item);
        }
    }

    /// <summary>
    /// Dequeues the item which matches the element
    /// </summary>
    public static T DequeueMatching<T>(this Queue<T> queue, Predicate<T> predicate)
    {
        T item = default;
        bool found = false;
        Queue<T> removed = new();
        while (queue.Count > 0)
        {
            T i = queue.Dequeue();
            if (predicate.Invoke(i))
            {
                item = i;
                found = true;
            }
            else
            {
                removed.Enqueue(i);
            }
        }

        queue.EnqueueAll(removed);

        if (!found) throw new InvalidOperationException("Unable to match element");

        return item;
    }

    /// <summary>
    /// Returns number of elements in the first group.
    /// </summary>
    public static int CountNextGroup<T>(this Queue<T> queue)
    {
        if (queue.Count <= 1) return queue.Count;

        for (int i = 0; i < queue.Count - 1; i++)
        {
            Type thisElem = queue.ElementAt(i).GetType();
            Type nextElem = queue.ElementAt(i + 1).GetType();

            if (thisElem != nextElem) return i + 1;
        }
        return queue.Count;
    }

    /// <summary>
    /// Returns total number of groups in the queue.
    /// </summary>
    public static int CountTotalGroups<T>(this Queue<T> queue)
    {
        if (queue.Count <= 1) return queue.Count;

        int elementsGrouped = 1;

        for (int i = 0; i < queue.Count - 1; i++)
        {
            Type thisElem = queue.ElementAt(i).GetType();
            Type nextElem = queue.ElementAt(i + 1).GetType();

            if (thisElem != nextElem) elementsGrouped++;
        }

        return elementsGrouped;
    }

    /// <summary>
    /// Dequeues the first group.
    /// </summary>
    public static Queue<T> DequeGroup<T>(this Queue<T> queue)
    {
        Queue<T> dequeued = new();
        int numGroupElements = queue.CountNextGroup();

        for (int i = 0; i < numGroupElements; i++) dequeued.Enqueue(queue.Dequeue());

        return dequeued;
    }
}