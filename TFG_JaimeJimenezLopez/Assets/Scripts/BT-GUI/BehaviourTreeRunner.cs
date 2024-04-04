using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    BehaviourTree _behaviourTree;
    // Start is called before the first frame update
    void Start()
    {
        _behaviourTree = ScriptableObject.CreateInstance<BehaviourTree>();
        
        var debug = ScriptableObject.CreateInstance<NodeDebugger>();
        debug.log = " TEST 1";
        
        var debug2 = ScriptableObject.CreateInstance<NodeDebugger>();
        debug2.log = " TEST 2";
        
        var debug3 = ScriptableObject.CreateInstance<NodeDebugger>();
        debug3.log = " TEST 3";

        var debugPause = ScriptableObject.CreateInstance<NodeDelay>();

        var sequencer = ScriptableObject.CreateInstance<NodeSequence>();
        sequencer.children.Add(debug);
        sequencer.children.Add(debugPause);
        sequencer.children.Add(debug2);
        sequencer.children.Add(debugPause);
        sequencer.children.Add(debug3);
        sequencer.children.Add(debugPause);
        
        var loop = ScriptableObject.CreateInstance<NodeRepeat>();
        loop.child = sequencer;

        _behaviourTree.root = loop;
    }

    // Update is called once per frame
    void Update()
    {
        _behaviourTree.Update();
    }
}
