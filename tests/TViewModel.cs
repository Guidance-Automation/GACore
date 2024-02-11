using NUnit.Framework;
using NLog;

namespace GACore.Test;

[TestFixture]
public class TViewModel
{
    private readonly AutoResetEvent _propertyChangedSet = new(false);

    private TimeSpan Timeout { get; } = TimeSpan.FromSeconds(5);

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        GACore.NLog.NLogManager.Instance.LogLevel = LogLevel.Trace;
    }

    [Test]
    public void PropertyChanged()
    {
        FooModel fooModel = new();
        FooViewModel fooViewModel = new();
        fooViewModel.PropertyChanged += FooViewModel_PropertyChanged;

        Assert.That(fooViewModel.Model, Is.Null);

        fooViewModel.Model = fooModel;

        Assert.That(_propertyChangedSet.WaitOne(Timeout));
        Assert.That(fooModel, Is.EqualTo(fooViewModel.Model));

        fooViewModel.Model = null;

        Assert.That(_propertyChangedSet.WaitOne(Timeout));
        Assert.That(fooViewModel.Model, Is.Null);
    }

    private void FooViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        Assert.That(e.PropertyName, Is.EqualTo("Model"));
        _propertyChangedSet.Set();
    }
}