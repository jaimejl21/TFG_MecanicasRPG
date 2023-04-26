using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDialogueManager : MonoBehaviour
{
    string converName;

    public DialogueSystemTrigger dst;

    void Start()
    {
        converName = GameManager.inst.converName;

        dst.conversation = converName;
    }


}
