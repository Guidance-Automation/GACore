namespace GACore.Architecture;

public interface IKeyedEnumerable<T>
{
    public Guid Key { get; }

    public IEnumerable<T> Items { get; }
}