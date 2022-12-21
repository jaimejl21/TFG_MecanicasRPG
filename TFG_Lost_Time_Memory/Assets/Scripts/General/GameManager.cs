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
        public List<Gear.Info> gearList;

        public ListsToJson(List<Character.Info> charList, List<Gear.Info> gearList)
        {
            this.charList = charList;
            this.gearList = gearList;
        }
    }

    ListsToJson lists;

    [SerializeField]
    string filename;

    public static List<Character.Info> allChar;
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
            Gear.Info gi = new Gear.Info(-1, -1, false, -1);
            for (int i = 0; i < 12; i++)
            {
                if(i<6)
                {
                    allGear.Add(new Gear.Info(i, i, false, -1));
                }
                else
                {
                    allGear.Add(new Gear.Info(i, (i-6), false, -1));
                }
                allChar.Add(new Character.Info(i, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi }));
            }

            SaveListsToJson();

            started = 1;

            PlayerPrefs.SetInt("started", started);
        }
        else
        {
            GetListsFromJson();
            allChar = lists.charList;
            allGear = lists.gearList;
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
        FileHandler.SaveToJson2(new ListsToJson(allChar, allGear), filename);
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
