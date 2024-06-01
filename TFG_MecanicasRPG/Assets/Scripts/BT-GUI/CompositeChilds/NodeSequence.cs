using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSequence : NodeComposite
{
    private int idxChild;
    protected override void OnStart()
    {
        idxChild = 0;
    }

    protected override Status OnUpdate()
    {
        var currentChild = children[idxChild];
        switch (currentChild.Update())
        {
            case Status.Failure:
                return Status.Failure;
            case Status.Running:
                return Status.Running;
            case Status.Success:
                idxChild++;
                break;
        }

        return idxChild.Equals(children.Count) ? Status.Success : Status.Running;
    }

    protected override void OnStop()
    {
       
    }
}