namespace GACore;

/// <summary>
/// Generic mailbox structure for storing updates for a unique object.
/// </summary>
/// <typeparam name="T">Key (struct)</typeparam>
/// <typeparam name="U">Mail (class)</typeparam>
public abstract class GenericMailbox<T, U> where U : class
{
    public event Action? Updated;
    public U? Mail { get; private set; }
    public T? Key { get; }
    public DateTime LastUpdate { get; private set; } = DateTime.MinValue;

    public GenericMailbox(T key, U? mail)
	{
		if (key == null) throw new ArgumentNullException(nameof(key));

		Key = key;
		Mail = mail ?? throw new ArgumentNullException(nameof(mail));
		LastUpdate = DateTime.Now;
	}

    public override int GetHashCode()
    {
		if(Key != null)
		{
            return Key.GetHashCode();
        }
		return -1;
    }

    public override bool Equals(object? obj)
	{
		if ((obj == null) || !GetType().Equals(obj.GetType()) || Key == null)
		{
			return false;
		}
		else
		{
			GenericMailbox<T, U> other = (GenericMailbox<T, U>)obj;
			return Key.Equals(other.Key);
		}
	}

    public string ToMailBoxString()
    {
        return string.Format("Mailbox: {0}", Key);
    }

    public override string ToString()
    {
        return ToMailBoxString();
    }

    public void Update(U newMail)
    {
        Mail = newMail ?? throw new ArgumentNullException(nameof(newMail));
        LastUpdate = DateTime.Now;
        OnUpdated();
    }

    private void OnUpdated()
	{
		if(Updated != null)
        {
            foreach (Action handler in Updated.GetInvocationList().Cast<Action>())
			{
				Task.Run(handler.Invoke);    
			}
		}
	}
}