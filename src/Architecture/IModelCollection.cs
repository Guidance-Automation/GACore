namespace GACore.Architecture;

public interface IModelCollection<T>
{
    public event Action<T> Added;

    public event Action<T> Removed;

    public IEnumerable<T> GetModels();
}