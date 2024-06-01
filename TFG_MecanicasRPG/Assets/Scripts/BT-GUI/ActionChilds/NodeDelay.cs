using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDelay : NodeAction
{
    public float duration = 1;
    float timeStart;

    protected override void OnStart()
    {
        timeStart = Time.time;
    }

    protected override Status OnUpdate()
    {
        if (Time.time - timeStart > duration)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnStop()
    {
        
    }
}
