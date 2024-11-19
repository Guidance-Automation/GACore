using GACore.Generics;
using NUnit.Framework;

namespace GACore.Test.Generics;

[TestFixture]
[Category("Generics")]
public class TQuandary
{
    [Test]
    [TestCase(0, 1)]
    public void Quandary_Init(int initial, int final)
    {
        Quandary<int> quandary = new(initial, final);

        Assert.Multiple(() =>
        {
            Assert.That(quandary.Initial, Is.EqualTo(initial));
            Assert.That(quandary.Final, Is.EqualTo(final));
        });
    }

    [Test]
    public void Quandary_Set_Final()
    {
        int final = 0;
        Quandary<int> quandary = new(int.MinValue, final);
        Assert.Multiple(() =>
        {
            Assert.That(quandary.Initial, Is.EqualTo(int.MinValue));
            Assert.That(quandary.Final, Is.EqualTo(final));
        });

        final = int.MaxValue;
        quandary.Final = final;
        Assert.Multiple(() =>
        {
            Assert.That(quandary.Initial, Is.EqualTo(int.MinValue));
            Assert.That(quandary.Final, Is.EqualTo(final));
        });
    }

    [Test]
    public void Quandary_Set_Initial()
    {
        int initial = int.MinValue;
        Quandary<int> quandary = new(initial, int.MaxValue);
        Assert.Multiple(() =>
        {
            Assert.That(quandary.Initial, Is.EqualTo(initial));
            Assert.That(quandary.Final, Is.EqualTo(int.MaxValue));
        });

        initial = 0;
        quandary.Initial = initial;
        Assert.Multiple(() =>
        {
            Assert.That(quandary.Initial, Is.EqualTo(initial));
            Assert.That(quandary.Final, Is.EqualTo(int.MaxValue));
        });
    }
}