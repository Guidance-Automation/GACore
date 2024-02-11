using GACore.Architecture;
using GACore.NLog;
using NLog;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GACore;

/// <summary>
/// Boilerplate ViewModel for WPF applications
/// </summary>
public abstract class AbstractViewModel<T> : IViewModel<T> where T : class
{
	private T? _model;

	public T? Model
	{
		get { return _model; }
		set
		{
			T? oldValue = _model;
			_model = value;
			HandleModelUpdate(oldValue, _model);
			OnNotifyPropertyChanged();

			Logger?.Debug("[{0}] Model updated: {1}", GetType().Name, value != null ? value.GetType().Name : "null" );
		}
	}
		
	public Logger? Logger { get; } = LoggerFactory.GetStandardLogger(StandardLogger.ViewModel);

	protected virtual void HandleModelUpdate(T? oldValue, T? newValue)
	{
		Logger?.Trace("[{0}] HandleModelUpdate() oldValue: {1}, newValue: {2}",
			GetType().Name, 
			oldValue == null? "null" : oldValue.ToString(),
			newValue == null ? "null" : newValue.ToString());
	}

	public InvokeBehavior InvokeBehavior { get; set; } = InvokeBehavior.Invoke;

	public AbstractViewModel()
	{
	}

	public event PropertyChangedEventHandler? PropertyChanged;

	protected void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
	{
		Logger?.Trace("[{0}] OnNotifyPropertyChanged() propertyName:{1}", GetType().Name, propertyName);

		// This should be an invoke becuase we want to tell the view to update, usually on a CompositionTarger.RenderFrame
		// Doing this as BeginInvoke adds far too many messages to the message queue.

		switch (InvokeBehavior)
		{
			case InvokeBehavior.BeginInvoke:
				PropertyChanged?.BeginInvoke(this, new PropertyChangedEventArgs(propertyName), null, null);
				break;

			case InvokeBehavior.Invoke:
			default:
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
				break;
		}
	}
}