using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public int charToEquipGear = 0;
    public bool restartPP, initialized = false, objectAlert = false;
    public int coins, idGearCount, idCharCount, awMats, upMats, enemyTeam, lvlUpMatC, lvlUpMatR, lvlUpMatSR;
    public string converName = "";

    [SerializeField]
    string filename;

    public static List<Character.Info> allChar;
    public static List<Gear.Info> allGear;

    int started;

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
            PlayerPrefs.DeleteAll();
            started = 0;
        }

        if (started == 0)
        {
            idGearCount = 0;
            idCharCount = 0;

            Gear.Info gi = new Gear.Info(-1, 10, -1, -1, 0, 0, 0, false, -1);
            for (int i = 0; i < 39; i++)
            {
                if(i < 6)
                {
                    allGear.Add(new Gear.Info(i, AuxSetStatAmount(i, 0), i, 0, 0, 0, 0, false, -1));
                    if(i == 0)
                    {
                        allChar.Add(new Character.Info(i, 0, 0, 6, 1, true, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                        idCharCount++;
                    }
                    else if(i == 1)
                    {
                        allChar.Add(new Character.Info(i, 1, 0, 7, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                        idCharCount++;
                    }
                    else if (i == 2)
                    {
                        allChar.Add(new Character.Info(i, 2, 0, 8, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                        idCharCount++;
                    }
                    else if ((i > 2) && (i < 5))
                    {
                        allChar.Add(new Character.Info(i, 3, 0, 9, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                        idCharCount++;
                    }
                    else
                    {
                        allChar.Add(new Character.Info(i, 4, 0, 10, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                        idCharCount++;
                    }
                }
                else if((i > 5) && (i < 12))
                {
                    allGear.Add(new Gear.Info(i, AuxSetStatAmount((i-6), 1), (i-6), 1, 1, 0, 0, false, -1));
                    if(i == 6)
                    {
                        allChar.Add(new Character.Info(i, 4, 0, 11, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                        idCharCount++;
                    }
                    if ((i > 6) && (i < 9))
                    {
                        allChar.Add(new Character.Info(i, 5, 0, 12, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                        idCharCount++;
                    }
                    else if ((i > 8) && (i < 11))
                    {
                        allChar.Add(new Character.Info(i, 6, 0, 6, -1, false, new List<Gear.Info>() { gi, gi, gi, gi, gi, gi, gi }, 1, 0, 320, new Character.Stats()));
                        idCharCount++;
                    }
                }
                else if ((i > 11) && (i < 18))
                {
                    allGear.Add(new Gear.Info(i, AuxSetStatAmount((i-12), 2), (i - 12), 2, 2, 0, 0, false, -1));
                }
                else if ((i > 17) && (i < 25))
                {
                    allGear.Add(new Gear.Info(i, AuxSetStatAmount((i-12), 0), (i - 12), -1, 0, 0, 0, false, -1));
                }
                else if ((i > 24) && (i < 32))
                {
                    allGear.Add(new Gear.Info(i, AuxSetStatAmount((i-19), 1), (i - 19), -1, 1, 0, 0, false, -1));
                }
                else
                {
                    allGear.Add(new Gear.Info(i, AuxSetStatAmount((i-26), 2), (i - 26), -1, 2, 0, 0, false, -1));
                }
                idGearCount++;
            }

            SaveListsToJson();

            started = 1;
            coins = 50000;
            awMats = 90;
            upMats = 90;
            lvlUpMatC = 100;
            lvlUpMatR = 100;
            lvlUpMatSR = 100;

            PlayerPrefs.SetInt("started", started);
            PlayerPrefs.SetInt("idGearCount", idGearCount);
            PlayerPrefs.SetInt("idCharCount", idCharCount);
            PlayerPrefs.SetInt("coins", coins);
            PlayerPrefs.SetInt("awMats", awMats); 
            PlayerPrefs.SetInt("upMats", upMats);
            PlayerPrefs.SetInt("amountC", lvlUpMatC);
            PlayerPrefs.SetInt("amountR", lvlUpMatR);
            PlayerPrefs.SetInt("amountSR", lvlUpMatSR);
        }
        else
        {
            GetListsFromJson();
            allChar = lists.charList;
            allGear = lists.gearList;

            GetPlayerPrefs("idGearCount", ref idGearCount, 0);
            GetPlayerPrefs("idCharCount", ref idCharCount, 0);
            GetPlayerPrefs("coins", ref coins, 50000);
            GetPlayerPrefs("upMats", ref upMats, 90);
            GetPlayerPrefs("awMats", ref awMats, 90);
            GetPlayerPrefs("amountC", ref lvlUpMatC, 100);
            GetPlayerPrefs("amountR", ref lvlUpMatR, 100);
            GetPlayerPrefs("amountSR", ref lvlUpMatSR, 100);
        }
        initialized = true;
    }

    public int AuxSetStatAmount(int objType, int rarity)
    {
        if (objType == 0 || objType == 3)
        {
            switch (rarity)
            {
                case 0:
                    return 5;
                case 1:
                    return 10;
                case 2:
                    return 20;
                default:
                    return 0;
            }
        }
        else if (objType == 1 || objType == 4)
        {
            switch (rarity)
            {
                case 0:
                    return 3;
                case 1:
                    return 6;
                case 2:
                    return 12;
                default:
                    return 0;
            }
        }
        else if (objType == 2 || objType == 5)
        {
            switch (rarity)
            {
                case 0:
                    return 50;
                case 1:
                    return 100;
                case 2:
                    return 200;
                default:
                    return 0;
            }
        }
        else
        {
            switch (rarity)
            {
                case 0:
                    return 10;
                case 1:
                    return 20;
                case 2:
                    return 30;
                default:
                    return 0;
            }
        }
    }

    public void GetPlayerPrefs(string name, ref int toGet, int num)
    {
        if (PlayerPrefs.HasKey(name))
        {
            toGet = PlayerPrefs.GetInt(name);
        }
        else
        {
            toGet = num;
        }
    }

    public void GetListsFromJson ()
    {
        lists = FileHandler.ReadFromJSON<ListsToJson>(filename);
    }

    public void SaveListsToJson()
    {
        FileHandler.SaveToJson2(new ListsToJson(allChar, allGear), filename);
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
