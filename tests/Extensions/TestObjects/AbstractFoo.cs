namespace GACore.Test.Extensions.TestObjects;

public abstract class AbstractFoo
{
    public AbstractFoo()
    {
        Guid = Guid.NewGuid();
    }

    public Guid Guid { get; }

    public override int GetHashCode()
    {
        return Guid.GetHashCode();
    }

    public abstract string ClassType { get; }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;

        AbstractFoo other = (AbstractFoo)obj;

        if (other != null) return Guid == other.Guid;

        return false;
    }
}