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
    FadeInOut fio;

    [Tooltip("Typically leave unticked so temporary Dialogue Managers don't unregister your functions.")]
    public bool unregisterOnDisable = false;

    void Start()
    {
        converName = GameManager.inst.converName;

        dst.conversation = converName;

        fio = FindObjectOfType<FadeInOut>();
    }

    void OnEnable()
    {
        //Lua.RegisterFunction("AddOne", this, SymbolExtensions.GetMethodInfo(() => AddOne((double)0)));
        Lua.RegisterFunction("ChangeToScene", this, SymbolExtensions.GetMethodInfo(() => ChangeToScene(string.Empty)));
        Lua.RegisterFunction("ChangeToDialogueScene", this, SymbolExtensions.GetMethodInfo(() => ChangeToDialogueScene(string.Empty)));
        Lua.RegisterFunction("ReturnToNodesMap", this, SymbolExtensions.GetMethodInfo(() => ReturnToNodesMap()));
        Lua.RegisterFunction("HelpAction", this, SymbolExtensions.GetMethodInfo(() => HelpAction()));
        Lua.RegisterFunction("ChangeToFightScene", this, SymbolExtensions.GetMethodInfo(() => ChangeToFightScene((double)0)));
        Lua.RegisterFunction("NMObjectAlert", this, SymbolExtensions.GetMethodInfo(() => NMObjectAlert()));
        Lua.RegisterFunction("AddCharacter", this, SymbolExtensions.GetMethodInfo(() => AddCharacter((double)0)));
        
    }

    void OnDisable()
    {
        if (unregisterOnDisable)
        {
            Lua.UnregisterFunction("ChangeToScene");
            Lua.UnregisterFunction("ChangeToDialogueScene");
            Lua.UnregisterFunction("ReturnToNodesMap"); 
            Lua.UnregisterFunction("HelpAction"); 
            Lua.UnregisterFunction("ChangeToFightScene"); 
            Lua.UnregisterFunction("NMObjectAlert");
            Lua.UnregisterFunction("AddCharacter");   
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
        if (fio != null)
        {
            fio.FadeToScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void ChangeToFightScene(double enemyTeam)
    {
        SceneManager.LoadScene("Fight");
        GameManager.inst.enemyTeam = (int)enemyTeam;
        if (fio != null)
        {
            fio.FadeToScene("Fight");
        }
        else
        {
            SceneManager.LoadScene("Fight");
        }
    }

    public void ChangeToDialogueScene(string conver)
    {
        GameManager.inst.converName = conver;
        if (fio != null)
        {
            fio.FadeToScene("Dialogue");
        }
        else
        {
            SceneManager.LoadScene("Dialogue");
        }
    }

    public void NMObjectAlert()
    {
        GameManager.inst.objectAlert = true;
    }

    public void HelpAction()
    {
        int rnd = new Random().Next(0, 2);
        ChangeToFightScene(rnd);
        //switch (rnd)
        //{
        //    case 0:
        //        ChangeToFightScene(rnd);
        //        break;
        //    case 1:
        //        NMObjectAlert();
        //        ReturnToNodesMap();
        //        break;
        //    default:
        //        break;
        //}
    }

    public void ReturnToNodesMap()
    {
        int nNodesMaps = PlayerPrefs.GetInt("nNodesMaps");
        SceneManager.LoadScene("NodesMap" + nNodesMaps);
        if (fio != null)
        {
            fio.FadeToScene("NodesMap" + nNodesMaps);
        }
        else
        {
            SceneManager.LoadScene("NodesMap" + nNodesMaps);
        }
    }

    public void AddCharacter(double character)
    {
        Gear.Info gi = new Gear.Info(-1, 10, -1, -1, 0, 0, 0, false, -1);
        int idCharCount;
        switch (character)
        {
            case 0:
                //Velkra
                idCharCount = PlayerPrefs.GetInt("idCharCount");
                GameManager.allChar.Add(new Character.Info(idCharCount, 0, 0, 7, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                idCharCount++;
                PlayerPrefs.SetInt("idCharCount", idCharCount);
                break;
            case 1:
                //Freydam
                idCharCount = PlayerPrefs.GetInt("idCharCount");
                GameManager.allChar.Add(new Character.Info(idCharCount, 5, 0, 6, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                idCharCount++;
                PlayerPrefs.SetInt("idCharCount", idCharCount);
                break;
            case 2:
                //Karris
                idCharCount = PlayerPrefs.GetInt("idCharCount");
                GameManager.allChar.Add(new Character.Info(idCharCount, 6, 0, 6, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                idCharCount++;
                PlayerPrefs.SetInt("idCharCount", idCharCount); 
                break;
            case 3:
                //Belaran
                idCharCount = PlayerPrefs.GetInt("idCharCount");
                GameManager.allChar.Add(new Character.Info(idCharCount, 5, 0, 11, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                idCharCount++;
                PlayerPrefs.SetInt("idCharCount", idCharCount);
                break;
            case 4:
                //Oriel
                idCharCount = PlayerPrefs.GetInt("idCharCount");
                GameManager.allChar.Add(new Character.Info(idCharCount, 3, 0, 9, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                idCharCount++;
                PlayerPrefs.SetInt("idCharCount", idCharCount);
                break;
            case 5:
                //Glokku
                idCharCount = PlayerPrefs.GetInt("idCharCount");
                GameManager.allChar.Add(new Character.Info(idCharCount, 2, 0, 10, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                idCharCount++;
                PlayerPrefs.SetInt("idCharCount", idCharCount);
                break;
            case 6:
                //Yonlud
                idCharCount = PlayerPrefs.GetInt("idCharCount");
                GameManager.allChar.Add(new Character.Info(idCharCount, 3, 0, 6, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                idCharCount++;
                PlayerPrefs.SetInt("idCharCount", idCharCount);
                break;
            case 7:
                //Dramor
                idCharCount = PlayerPrefs.GetInt("idCharCount");
                GameManager.allChar.Add(new Character.Info(idCharCount, 1, 0, 10, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                idCharCount++;
                PlayerPrefs.SetInt("idCharCount", idCharCount);
                break;
            case 8:
                //Godrick
                idCharCount = PlayerPrefs.GetInt("idCharCount");
                GameManager.allChar.Add(new Character.Info(idCharCount, 6, 0, 6, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                idCharCount++;
                PlayerPrefs.SetInt("idCharCount", idCharCount);
                break;
            default:
                break;

        }
    }
}
