using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BehaviourTree : ScriptableObject
{
    public Node root;
    public Node.Status statusBehaviorTree = Node.Status.Running;

    public Node.Status Update()
    {
        if (Node.Status.Running.Equals(root.status))
        {
            statusBehaviorTree = root.Update();
        }

        return statusBehaviorTree;
    }
    
}
