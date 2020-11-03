﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GACore.Extensions
{
	/// <summary>
	/// Extension methods for List<T> to facliltate group delisting.
	/// </summary>
	/// <typeparam name="T">Generic type specifying type of items in list</typeparam>
	public static class List_ExtensionMethods
	{
		/// <summary>
		/// Returns number of elements in the first group.
		/// </summary>
		public static int CountNextGroup<T>(this IList<T> list)
		{
			if (list.Count <= 1) return list.Count;

			for (int i = 0; i < list.Count - 1; i++)
			{
				Type thisElem = list.ElementAt(i).GetType();
				Type nextElem = list.ElementAt(i + 1).GetType();

				if (thisElem != nextElem) return i + 1;
			}
			return list.Count;
		}

		/// <summary>
		/// Returns total number of groups in the list.
		/// </summary>
		public static int CountTotalGroups<T>(this IList<T> list)
		{
			if (list.Count <= 1) return list.Count;

			int elementsGrouped = 1;

			for (int i = 0; i < list.Count - 1; i++)
			{
				Type thisElem = list.ElementAt(i).GetType();
				Type nextElem = list.ElementAt(i + 1).GetType();

				if (thisElem != nextElem) elementsGrouped++;
			}
			return elementsGrouped;
		}

		/// <summary>
		/// Delists the first group
		/// </summary>
		public static List<T> DelistGroup<T>(this IList<T> list)
		{
			List<T> delisted = new List<T>();

			if (list.Count > 0)
			{
				Type groupType = list[0].GetType();
				while ((list.Count > 0) && (groupType.Equals(list[0].GetType())))
				{
					delisted.Add(list[0]);
					list.RemoveAt(0);
				}
			}

			return delisted;
		}

		public static void AddIfNotNull<T>(this IList<T> list, Nullable<T> item) where T : struct
		{
			if (item != null) list.Add(item.Value);
		}
	}
}