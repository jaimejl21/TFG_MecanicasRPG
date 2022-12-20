using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public int charToEquipGear = 0;
    public bool restartPP;

    [System.Serializable]
    public class ListsToJson
    {
        public List<Character.Info> charList;
        public List<Character.Info> teamList;

        public ListsToJson(List<Character.Info> charList, List<Character.Info> teamList)
        {
            this.charList = charList;
            this.teamList = teamList;
        }
    }

    ListsToJson lists;

    [SerializeField]
    string filename;

    public static List<Character.Info> allChar;
    public static List<Character.Info> myTeam;
    public static List<Gear.Info> allGear;

    int started;

    void Awake()
    {
        if(GameManager.inst == null)
        {
            GameManager.inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        myTeam = new List<Character.Info>();
        allChar = new List<Character.Info>();
        allGear = new List<Gear.Info>();

        //Debug.Log("Started: " + started);

        if (!restartPP)
        {
            if (PlayerPrefs.HasKey("started"))
            {
                started = PlayerPrefs.GetInt("started");
            }
            else
            {
                started = 0;
            }
        }
        else
        {
            started = 0;
        }

        if (started == 0)
        {
            for (int i = 0; i < 12; i++)
            {
                if(i<6)
                {
                    myTeam.Add(new Character.Info(-1, -2, false, new List<Gear.Info>()));
                    allGear.Add(new Gear.Info(i, i, false));
                }
                else
                {
                    allGear.Add(new Gear.Info(i, (i-6), false));
                }
                allChar.Add(new Character.Info(i, -1, false, new List<Gear.Info>()));
            }

            SaveListsToJson();

            started = 1;

            PlayerPrefs.SetInt("started", started);
        }
        else
        {
            GetListsFromJson();
            myTeam = lists.teamList;
            allChar = lists.charList;
        }
    } 

    public void ShowMyTeam()
    {
        for (int i = 0; i < 6; i++)
        {
            if (myTeam[i] != null)
            {
                Debug.Log("" + myTeam[i].id + "//" + myTeam[i].pos + "//" + myTeam[i].inTeam);
            }
        }
    }

    public void GetListsFromJson ()
    {
        lists = FileHandler.ReadFromJSON<ListsToJson>(filename);
        //Debug.Log(lists.charList[0].id);
        //Debug.Log(lists.teamList[0].id);
    }

    public void SaveListsToJson()
    {
        FileHandler.SaveToJson2(new ListsToJson(allChar, myTeam), filename);
        //Debug.Log("SaveJson");
    }

    public int GetCharPosById(int id)
    {
        int pos = -1;
        for (int i = 0; i < allChar.Count; i++)
        {
            if (allChar[i].id == id)
            {
                pos = i;
            }
        }
        return pos;
    }

    public Character.Info GetCharInfoById(int id)
    {
        Character.Info chi = new Character.Info();
        for (int i = 0; i < allChar.Count; i++)
        {
            if (allChar[i].id == id)
            {
                chi = allChar[i];
            }
        }
        return chi;
    }
}
