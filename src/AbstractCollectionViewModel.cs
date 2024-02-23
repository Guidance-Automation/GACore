using GACore.Architecture;
using System.Collections.ObjectModel;

namespace GACore;

public abstract class AbstractCollectionViewModel<T, U, V> : AbstractViewModel<T>, ICollectionViewModel<T, U, V>
        where T : class, IModelCollection<V>
        where U : AbstractViewModel<V>, new()
        where V : class
{
    private readonly SynchronizationContext? _sync;
    protected ObservableCollection<U> viewModels = [];

    public void Dispose() => Dispose(true);

    public AbstractCollectionViewModel()
    {
        _sync = SynchronizationContext.Current;
        ViewModels = new ReadOnlyObservableCollection<U>(viewModels);
    }

    ~AbstractCollectionViewModel()
    {
        Dispose(false);
    }

    public ReadOnlyObservableCollection<U> ViewModels { get; }

    private bool _isDisposed = false;

    private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

    private void HandleAddCollectionItemModel(V collectionItemModel)
    {
        if (collectionItemModel == null)
        {
            Logger?.Warn("[{0}] HandleAddCollectionItemModel() collectionItemModel was null", GetType().Name);
            return;
        }

        _semaphoreSlim.Wait();

        try
        {
            // To solve race condition when getting an item added while the model is changed. 
            if (viewModels.Select(e => e.Model).Any(e => e != null && e.Equals(collectionItemModel)))
            {
                Logger?.Warn("[{0}] HandleAddCollectionItemModel() viewModels already contains a collectionItemViewModel for this collectionItemModel", GetType().Name);
                return;
            }

            U collectionItemViewModel = new() { Model = collectionItemModel };
            viewModels.Add(collectionItemViewModel);

            Logger?.Trace("[{0}] HandleAddCollectionItemModel() added: {1}", GetType().Name, collectionItemModel);
        }
        catch (Exception ex)
        {
            Logger?.Error(ex);
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    protected void HandleCollectionRefresh()
    {
        Logger?.Trace("[{0}] HandleCollectionRefresh()", GetType().Name);


        viewModels.Clear();

        if (Model == null) return;

        IEnumerable<V> existingModels = Model.GetModels();

        Logger?.Trace("[{0}] HandleCollectionRefresh() adding {1} existing model(s)", GetType().Name, existingModels.Count());

        foreach (V model in existingModels)
        {
            if (_sync == null)
                HandleAddCollectionItemModel(model);
            else
                _sync.Post(new SendOrPostCallback((_) => HandleAddCollectionItemModel(model)), null);
        }
    }

    protected override void HandleModelUpdate(T? oldValue, T? newValue)
    {
        if (oldValue != null) oldValue.Added -= Model_Added;
        if (oldValue != null) oldValue.Removed -= Model_Removed;

        if (newValue != null) newValue.Added += Model_Added;
        if (newValue != null) newValue.Removed += Model_Removed;

        base.HandleModelUpdate(oldValue, newValue);
        HandleCollectionRefresh();
    }

    public abstract U GetViewModelForModel(V model);

    private void Model_Removed(V obj)
    {
        if (_sync == null)
            ModelRemovedImpl(obj);
        else
            _sync.Post(new SendOrPostCallback((_) => ModelRemovedImpl(obj)), null);
    }

    private void ModelRemovedImpl(V obj)
    {
        if (obj == null)
        {
            Logger?.Warn("[{0}] Model_Removed() obj was null", GetType().Name);
            return;
        }

        _semaphoreSlim.Wait();

        try
        {
            U viewModel = GetViewModelForModel(obj);

            if (viewModel == null)
            {
                Logger?.Warn("[{0}] Model_Removed() GetViewModelForModel() returned null", GetType().Name);
                return;
            }

            viewModels.Remove(viewModel);
        }
        catch (Exception ex)
        {
            Logger?.Error(ex);
        }

        finally
        {
            _semaphoreSlim.Release();
        }
    }

    private void Model_Added(V obj)
    {
        if (_sync == null)
            ModelAddedImpl(obj);
        else
            _sync.Post(new SendOrPostCallback((_) => ModelAddedImpl(obj)), null);
    }

    private void ModelAddedImpl(V obj)
    {
        Logger?.Trace("[{0}] Model_Added()", GetType().Name);
        if (_sync == null)
            HandleAddCollectionItemModel(obj);
        else
            _sync.Post(new SendOrPostCallback((_) => HandleAddCollectionItemModel(obj)), null);
    }

    protected virtual void Dispose(bool isDisposing)
    {
        if (_isDisposed) return;

        if (isDisposing)
        {
            if (Model != null) Model.Added -= Model_Added;
            if (Model != null) Model.Removed -= Model_Removed;
        }

        _isDisposed = true;
    }
}
