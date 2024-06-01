using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSelector : NodeComposite
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
                idxChild++;
                break;
            case Status.Running:
                return Status.Running;
            case Status.Success:
                return Status.Success;
        }

        return idxChild.Equals(children.Count) ? Status.Failure : Status.Running;
    }

    protected override void OnStop()
    {
    }
}