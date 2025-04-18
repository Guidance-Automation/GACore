using GACore.Utility;
using NUnit.Framework;

namespace GACore.Test;

[TestFixture]
public class TMessenger
{
    private List<string> _messagesReceived;

    [SetUp]
    public void Setup()
    {
        _messagesReceived = [];
        Messenger.Unregister(this); 
    }

    [Test]
    public void Register_And_Send_Message()
    {
        Messenger.Register<string>(this, msg => _messagesReceived.Add(msg));

        Messenger.Send("Hello, World!");

        Assert.That(_messagesReceived, Has.Count.EqualTo(1));
        Assert.That(_messagesReceived[0], Is.EqualTo("Hello, World!"));
    }

    [Test]
    public void Send_Message_With_Context()
    {
        Messenger.Register<string>(this, msg => _messagesReceived.Add(msg), "Context1");

        Messenger.Send("Message1", "Context1");
        Messenger.Send("Message2", "Context2"); // Should be ignored

        Assert.That(_messagesReceived, Has.Count.EqualTo(1));
        Assert.That(_messagesReceived[0], Is.EqualTo("Message1"));
    }

    [Test]
    public void Unregister_Removes_Recipient()
    {
        Messenger.Register<string>(this, msg => _messagesReceived.Add(msg));

        Messenger.Unregister(this);
        Messenger.Send("This should not be received");

        Assert.That(_messagesReceived, Is.Empty);
    }

    [Test]
    public void Multiple_Recipients_Receive_Message()
    {
        List<string> messages1 = [];
        List<string> messages2 = [];

        Messenger.Register<string>("Recipient1", msg => messages1.Add(msg));
        Messenger.Register<string>("Recipient2", msg => messages2.Add(msg));

        Messenger.Send("Broadcast");

        Assert.Multiple(() =>
        {
            Assert.That(messages1, Has.Count.EqualTo(1));
            Assert.That(messages2, Has.Count.EqualTo(1));
        });

        Assert.Multiple(() =>
        {
            Assert.That(messages1[0], Is.EqualTo("Broadcast"));
            Assert.That(messages2[0], Is.EqualTo("Broadcast"));
        });
    }

    [Test]
    public void Exception_In_One_Handler_Does_Not_Affect_Others()
    {
        List<string> messages = [];

        Messenger.Register<string>(this, _ => throw new Exception("Test Exception"));
        Messenger.Register<string>("OtherRecipient", msg => messages.Add(msg));

        Assert.DoesNotThrow(() => Messenger.Send("Safe Message"));

        Assert.That(messages, Has.Count.EqualTo(1));
        Assert.That(messages[0], Is.EqualTo("Safe Message"));
    }
}
