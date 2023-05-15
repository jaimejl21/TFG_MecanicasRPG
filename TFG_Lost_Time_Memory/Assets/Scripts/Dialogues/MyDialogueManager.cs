using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

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
        //Lua.RegisterFunction("AddOne", this, SymbolExtensions.GetMethodInfo(() => AddOne((double)0)));
        Lua.RegisterFunction("ChangeToScene", this, SymbolExtensions.GetMethodInfo(() => ChangeToScene(string.Empty)));
        Lua.RegisterFunction("ReturnToNodesMap", this, SymbolExtensions.GetMethodInfo(() => ReturnToNodesMap()));
        Lua.RegisterFunction("HelpAction", this, SymbolExtensions.GetMethodInfo(() => HelpAction()));
        Lua.RegisterFunction("ChangeToFightScene", this, SymbolExtensions.GetMethodInfo(() => ChangeToFightScene((int)0)));
        Lua.RegisterFunction("NMObjectAlert", this, SymbolExtensions.GetMethodInfo(() => NMObjectAlert()));
    }

    void OnDisable()
    {
        if (unregisterOnDisable)
        {
            Lua.UnregisterFunction("ChangeToScene");
            Lua.UnregisterFunction("ReturnToNodesMap"); 
            Lua.UnregisterFunction("HelpAction"); 
            Lua.UnregisterFunction("ChangeToFightScene"); 
            Lua.UnregisterFunction("NMObjectAlert");
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

    public void ChangeToFightScene(int enemyTeam)
    {
        SceneManager.LoadScene("Fight");
        GameManager.inst.enemyTeam = enemyTeam;
    }

    public void NMObjectAlert()
    {
        GameManager.inst.objectAlert = true;
    }

    public void HelpAction()
    {
        int rnd = new Random().Next(0, 2);
        switch(rnd)
        {
            case 0:
                ChangeToFightScene(new Random().Next(0, 2));
                break;
            case 1:
                NMObjectAlert();
                ReturnToNodesMap();
                break;
            default:
                break;
        }
    }

    public void ReturnToNodesMap()
    {
        int nNodesMaps = PlayerPrefs.GetInt("nNodesMaps");
        SceneManager.LoadScene("NodesMap" + nNodesMaps);
    }
}
