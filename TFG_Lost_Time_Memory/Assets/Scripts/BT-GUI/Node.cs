using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : ScriptableObject
{
    public enum Status
    {
        Running,
        Failure,
        Success
    }

    public Status status = Status.Running;
    public bool started = false;

    public Status Update()
    {
        if (!started)
        {
            OnStart();
            started = true;
        }

        status = OnUpdate();

        if (Status.Failure.Equals(status) || Status.Success.Equals((status)))
        {
            OnStop();
            started = false;
        }

        return status;
    }

    protected abstract void OnStart();
    protected abstract Status OnUpdate();
    protected abstract void OnStop();
}
