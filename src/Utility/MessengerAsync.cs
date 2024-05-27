using System.Collections.Concurrent;

namespace GACore.Utility;

/// <summary>
/// Taken from practical MVVM Gill Cleeren
/// </summary>
public class MessengerAsync
{
    private static readonly ConcurrentDictionary<MessengerKey, object> _dictionary = new();

    /// <summary>
    /// Gets the single instance of the Messenger.
    /// </summary>
    public static MessengerAsync Default { get; } = new();

    /// <summary>
    /// Initializes a new instance of the Messenger class.
    /// </summary>
    private MessengerAsync()
    {
    }

    /// <summary>
    /// Registers a recipient for a type of message T. The action parameter will be executed
    /// when a corresponding message is sent.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="recipient"></param>
    /// <param name="action"></param>
    public static void Register<T>(object recipient, Func<T, Task> action)
    {
        Register(recipient, action, null);
    }

    /// <summary>
    /// Registers a recipient for a type of message T and a matching context. The action parameter will be executed
    /// when a corresponding message is sent.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="recipient"></param>
    /// <param name="action"></param>
    /// <param name="context"></param>
    public static void Register<T>(object recipient, Func<T, Task> action, object? context)
    {
        MessengerKey key = new(recipient, context);
        _dictionary.TryAdd(key, action);
    }

    /// <summary>
    /// Unregisters a messenger recipient completely. After this method is executed, the recipient will
    /// no longer receive any messages.
    /// </summary>
    /// <param name="recipient"></param>
    public static void Unregister(object recipient)
    {
        Unregister(recipient, null);
    }

    /// <summary>
    /// Unregisters a messenger recipient with a matching context completely. After this method is executed, the recipient will
    /// no longer receive any messages.
    /// </summary>
    /// <param name="recipient"></param>
    /// <param name="context"></param>
    public static void Unregister(object recipient, object? context)
    {
        MessengerKey key = new(recipient, context);
        _dictionary.TryRemove(key, out _);
    }

    /// <summary>
    /// Sends a message to registered recipients asynchronously. The message will reach all recipients that are
    /// registered for this message type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    public static Task SendAsync<T>(T message)
    {
        return SendAsync(message, null);
    }

    /// <summary>
    /// Sends a message to registered recipients asynchronously. The message will reach all recipients that are
    /// registered for this message type and matching context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="context"></param>
    public static async Task SendAsync<T>(T message, object? context)
    {
        IEnumerable<KeyValuePair<MessengerKey, object>> result;

        if (context == null)
        {
            // Get all recipients where the context is null.
            result = from r in _dictionary where r.Key.Context == null select r;
        }
        else
        {
            // Get all recipients where the context is matching.
            result = from r in _dictionary where r.Key.Context != null && r.Key.Context.Equals(context) select r;
        }

        IEnumerable<Task> tasks = result.Select(x => x.Value).OfType<Func<T, Task>>().Select(action => action(message));
        await Task.WhenAll(tasks);
    }
}