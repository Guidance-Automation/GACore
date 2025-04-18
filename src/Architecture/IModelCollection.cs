namespace GACore.Architecture;

public interface IModelCollection<T>
{
    public event Action<T> Added;

    public event Action<T> Removed;

    public event Action<T> Updated;

    public IEnumerable<T> GetModels();
}