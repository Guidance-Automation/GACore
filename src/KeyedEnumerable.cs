using GACore.Architecture;

namespace GACore;

public class KeyedEnumerable<T> : IKeyedEnumerable<T>
{
	public static IKeyedEnumerable<T> Empty()
	{
		return new KeyedEnumerable<T>(Guid.Empty, []);
	}

	public KeyedEnumerable(Guid key, IEnumerable<T> items)
	{
        ArgumentNullException.ThrowIfNull(items);

        if (items.Any() && key == Guid.Empty) throw new InvalidOperationException("Key cannot be empty for populated items");

		Key = key;
		Items = items;
	}

	public IEnumerable<T> Items { get; }

    public override string ToString()
    {
        return ToSummaryString();
    }

    public string ToSummaryString()
    {
        return string.Format("Key:{0} Count:{1}", Key, Items.Count());
    }

    public Guid Key { get; }
}