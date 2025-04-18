using GACore.Utility;
using NUnit.Framework;

namespace GACore.Test;

[TestFixture]
public class TMessengerAsync
{
    private List<string> _messagesReceived;

    [SetUp]
    public void Setup()
    {
        _messagesReceived = [];
        MessengerAsync.Unregister(this);
    }

    [Test]
    public async Task Register_And_Send_Async_Message()
    {
        ManualResetEventSlim resetEvent = new(false);

        MessengerAsync.Register<string>(this, async msg =>
        {
            _messagesReceived.Add(msg);
            resetEvent.Set();
            await Task.CompletedTask;
        });

        await MessengerAsync.SendAsync("Hello Async");
        resetEvent.Wait();

        Assert.That(_messagesReceived, Has.Count.EqualTo(1));
        Assert.That(_messagesReceived[0], Is.EqualTo("Hello Async"));
    }

    [Test]
    public async Task Send_Async_Message_With_Context()
    {
        ManualResetEventSlim resetEvent = new(false);

        MessengerAsync.Register<string>(this, async msg =>
        {
            _messagesReceived.Add(msg);
            resetEvent.Set();
            await Task.CompletedTask;
        }, "Context1");

        await MessengerAsync.SendAsync("Message1", "Context1");
        await MessengerAsync.SendAsync("Message2", "Context2"); // Should be ignored

        resetEvent.Wait();

        Assert.That(_messagesReceived, Has.Count.EqualTo(1));
        Assert.That(_messagesReceived[0], Is.EqualTo("Message1"));
    }

    [Test]
    public async Task Unregister_Async_Removes_Recipient()
    {
        MessengerAsync.Register<string>(this, async msg =>
        {
            _messagesReceived.Add(msg);
            await Task.CompletedTask;
        });

        MessengerAsync.Unregister(this);
        await MessengerAsync.SendAsync("This should not be received");

        Assert.That(_messagesReceived, Is.Empty);
    }

    [Test]
    public async Task Multiple_Recipients_Receive_Async_Message()
    {
        List<string> messages1 = [];
        List<string> messages2 = [];
        ManualResetEventSlim resetEvent = new(false);

        MessengerAsync.Register<string>("Recipient1", async msg =>
        {
            messages1.Add(msg);
            resetEvent.Set();
            await Task.CompletedTask;
        });

        MessengerAsync.Register<string>("Recipient2", async msg =>
        {
            messages2.Add(msg);
            resetEvent.Set();
            await Task.CompletedTask;
        });

        await MessengerAsync.SendAsync("Async Broadcast");
        resetEvent.Wait();

        Assert.Multiple(() =>
        {
            Assert.That(messages1, Has.Count.EqualTo(1));
            Assert.That(messages2, Has.Count.EqualTo(1));
        });

        Assert.Multiple(() =>
        {
            Assert.That(messages1[0], Is.EqualTo("Async Broadcast"));
            Assert.That(messages2[0], Is.EqualTo("Async Broadcast"));
        });
    }

    [Test]
    public void Exception_In_Async_Handler_Does_Not_Affect_Others()
    {
        List<string> messages = [];
        ManualResetEventSlim resetEvent = new(false);

        MessengerAsync.Register<string>(this, _ =>
        {
            throw new Exception("Test Exception");
        });

        MessengerAsync.Register<string>("OtherRecipient", async msg =>
        {
            messages.Add(msg);
            resetEvent.Set();
            await Task.CompletedTask;
        });

        Assert.DoesNotThrowAsync(async () => await MessengerAsync.SendAsync("Safe Async Message"));
        resetEvent.Wait();

        Assert.That(messages, Has.Count.EqualTo(1));
        Assert.That(messages[0], Is.EqualTo("Safe Async Message"));
    }
}
