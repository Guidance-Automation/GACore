using System;
using System.Linq;

namespace GACore;

/// <summary>
/// Generic mailbox structure for storing updates for a unique object.
/// </summary>
/// <typeparam name="T">Key (struct)</typeparam>
/// <typeparam name="U">Mail (class)</typeparam>
public abstract class GenericMailbox<T, U> where U : class
{
	public GenericMailbox(T key, U mail)
	{
		if (key == null) throw new ArgumentNullException(nameof(key));

		Key = key;
		Mail = mail ?? throw new ArgumentNullException(nameof(mail));
		LastUpdate = DateTime.Now;
	}

	public U Mail { get; private set; } = null;

	public DateTime LastUpdate { get; private set; } = DateTime.MinValue;

	public override int GetHashCode() => Key.GetHashCode();

	public override bool Equals(object obj)
	{
		if ((obj == null) || !this.GetType().Equals(obj.GetType()))
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

    public event Action Updated;

	private void OnUpdated()
	{
		if(Updated != null)
        {
            foreach (Action handler in Updated.GetInvocationList().Cast<Action>())
			{
				handler.BeginInvoke(null, null);
			}
		}
	}

	public void Update(U newMail)
	{
		Mail = newMail ?? throw new ArgumentNullException(nameof(newMail));
		LastUpdate = DateTime.Now;
		OnUpdated();
	}

	public T Key { get; }
}