using NUnit.Framework;
using System.Net;

namespace GACore.Test;

[TestFixture]
public class TGenericMailbox
{
    [Test]
    public void InitNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            new FooMailbox(1, null);
        });
    }

    [Test]
    public void Equals()
    {
        HashSet<FooMailbox> mailboxes = [];

        FooMailbox fooMailbox = new(1, IPAddress.Loopback);

        Assert.That(mailboxes.Add(fooMailbox));
        Assert.That(!mailboxes.Add(fooMailbox));

        IPAddress oneNineTwo = IPAddress.Parse("192.168.0.1");

        fooMailbox.Update(oneNineTwo);
        Assert.That(oneNineTwo, Is.EqualTo(fooMailbox.Mail));
        Assert.That(!mailboxes.Add(fooMailbox));
    }
}