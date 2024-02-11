namespace GACore.Generics;

/// <summary>
/// Construct an instance of a quandary object, allowing the clear definition
/// of an initial and final state in a system.
/// </summary>
/// <param name="initial">Instance of object defining the inital state.</param>
/// <param name="final">Instance of object defining the final state.</param>
public class Quandary<T>(T? initial, T? final)
{
    public Quandary() : this(default, default)
    {

    }

    public T? Final { get; set; } = final;

    public T? Initial { get; set; } = initial;
}