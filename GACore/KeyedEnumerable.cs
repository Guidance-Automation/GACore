﻿using GACore.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GACore
{
	public class KeyedEnumerable<T> : IKeyedEnumerable<T>
	{
		public static IKeyedEnumerable<T> Empty()
		{
			return new KeyedEnumerable<T>(Guid.Empty, Enumerable.Empty<T>());
		}

		public KeyedEnumerable(Guid key, IEnumerable<T> items)
		{
			if (items == null) throw new ArgumentNullException("items");

			if (items.Any() && key == Guid.Empty) throw new ArgumentOutOfRangeException("Key cannot be empty for populated items");

			Key = key;
			Items = items;
		}

		public IEnumerable<T> Items { get; }

		public override string ToString() => ToSummaryString();

		public string ToSummaryString() => string.Format("Key:{0} Count:{1}", Key, Items.Count());

		public Guid Key { get; }
	}
}
