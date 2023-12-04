using System.Collections.ObjectModel;

namespace GACore.Architecture;

/// <summary>
/// A viewmodel with a self maintaining observable collection of other viewmodels. 
/// </summary>
/// <typeparam name="T">The model this viewmodel exposes, e.g. something with a collection</typeparam>
/// <typeparam name="U">The viewmodels we expose as a collection.</typeparam>
/// <typeparam name="V">The model for the viewmodels, e.g. U is a viewmodel for a model V</typeparam>
public interface ICollectionViewModel<T, U, V> : IViewModel<T>
    where T : class, IModelCollection<V>
    where U : IViewModel<V>
    where V : class
{
    public ReadOnlyObservableCollection<U> ViewModels { get; }

    public U GetViewModelForModel(V model);
}