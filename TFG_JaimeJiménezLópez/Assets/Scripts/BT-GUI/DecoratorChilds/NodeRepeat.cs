using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRepeat : NodeDecorator
{
    protected override void OnStart()
    {
      
    }

    protected override Status OnUpdate()
    {
        child.Update();
        return Status.Running;
    }

    protected override void OnStop()
    {
       
    }
}
