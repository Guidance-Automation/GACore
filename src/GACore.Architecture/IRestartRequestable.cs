using System;

namespace GACore.Architecture;

public interface IRestartRequestable
{
    public event Action RestartRequested;

    public void RequestRestart();
}