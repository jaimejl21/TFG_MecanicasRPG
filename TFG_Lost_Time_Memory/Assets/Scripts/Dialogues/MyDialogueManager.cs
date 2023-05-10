using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyDialogueManager : MonoBehaviour
{
    string converName;

    public DialogueSystemTrigger dst;

    [Tooltip("Typically leave unticked so temporary Dialogue Managers don't unregister your functions.")]
    public bool unregisterOnDisable = false;

    void Start()
    {
        converName = GameManager.inst.converName;

        dst.conversation = converName;
    }

    void OnEnable()
    {
        Lua.RegisterFunction("ChangeToScene", this, SymbolExtensions.GetMethodInfo(() => ChangeToScene(string.Empty)));
    }

    void OnDisable()
    {
        if (unregisterOnDisable)
        {
            Lua.UnregisterFunction("ChangeToScene");
        }
    }

    public void DebugLog(string message)
    {
        Debug.Log(message);
    }

    public double AddOne(double value)
    { // Note: Lua always passes numbers as doubles.
        return value + 1;
    }
    public void ChangeToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


}
