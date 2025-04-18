using System.Collections.ObjectModel;

namespace GACore.Architecture;

/// <summary>
/// A view model with a self maintaining observable collection of other view models. 
/// </summary>
/// <typeparam name="T">The model this view model exposes, e.g. something with a collection</typeparam>
/// <typeparam name="U">The view models we expose as a collection.</typeparam>
/// <typeparam name="V">The model for the view models, e.g. U is a view model for a model V</typeparam>
public interface ICollectionViewModel<T, U, V> : IViewModel<T>
    where T : class, IModelCollection<V>
    where U : IViewModel<V>
    where V : class
{
    public ReadOnlyObservableCollection<U> ViewModels { get; }

    public U GetViewModelForModel(V model);
}