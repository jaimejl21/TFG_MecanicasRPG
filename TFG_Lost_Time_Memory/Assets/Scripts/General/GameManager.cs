using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

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

        //Debug.Log("Started: " + started);

        if(!restartPP)
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
            for (int i = 0; i < 8; i++)
            {
                allChar.Add(new Character.Info(i, -1, false));
            }

            for (int i = 0; i < 6; i++)
            {
                myTeam.Add(new Character.Info(-1, -2, false));
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
}
