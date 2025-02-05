using NLog;
using System.Collections.Concurrent;

namespace GACore.Utility;

/// <summary>
/// Taken from practical MVVM Gill Cleeren
/// </summary>
public class MessengerAsync
{
    private static readonly ConcurrentDictionary<MessengerKey, object> _dictionary = new();
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Registers a recipient for a type of message T. The action parameter will be executed
    /// when a corresponding message is sent.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="recipient"></param>
    /// <param name="action"></param>
    public static void Register<T>(object recipient, Func<T, Task> action)
    {
        _logger.Trace($"Register {recipient.GetType().Name} for {typeof(T).Name}");
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
        _logger.Trace($"Register {recipient.GetType().Name} for {typeof(T).Name} with context {context?.GetType().Name}");
        MessengerKey key = new(recipient, context);
        _dictionary.AddOrUpdate(key, action, (_, _) => action);
    }

    /// <summary>
    /// Unregisters a messenger recipient completely. After this method is executed, the recipient will
    /// no longer receive any messages.
    /// </summary>
    /// <param name="recipient"></param>
    public static void Unregister(object recipient)
    {
        _logger.Trace($"Unregister {recipient.GetType().Name}");
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
        _logger.Trace($"Unregister {recipient.GetType().Name} with context {context?.GetType().Name}");
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
        _logger.Trace($"Send {typeof(T).Name}");
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
        _logger.Trace($"Send {typeof(T).Name} with context {context?.GetType().Name}");

        List<Func<T, Task>> recipients = [.. _dictionary
            .Where(r => Equals(r.Key.Context, context))
            .Select(x => x.Value)
            .OfType<Func<T, Task>>()];

        await Parallel.ForEachAsync(recipients, async (action, _) =>
        {
            try
            {
                await action(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while handling message asynchronously: {ex.Message}");
            }
        });
    }
}