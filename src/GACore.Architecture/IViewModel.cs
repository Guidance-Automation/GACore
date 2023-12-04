using System.ComponentModel;

namespace GACore.Architecture;

public interface IViewModel<T> : INotifyPropertyChanged where T : class
{
    public T Model { get; }
}