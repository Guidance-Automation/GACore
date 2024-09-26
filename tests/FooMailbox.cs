using System.Net;

namespace GACore.Test;

/// <summary>
/// Dev class for testing generic mailbox functionality.
/// </summary>
internal class FooMailbox(int id, IPAddress? current) : GenericMailbox<int, IPAddress>(id, current)
{

}