using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelUpMananager : MonoBehaviour
{
    public GameObject charGO, pool, selectedPool, materialGO;
    public Transform charPos;
    public TextMeshProUGUI[] statsTxt, statsBonusTxt;

    public int idToEquip, amountC, amountR, amountSR;
    int selectedC = 0, selectedR, selectedSR;

    public List<Character.Info> allCharList;
    public List<GameObject> materialsList;
    public List<GameObject> selectedList;

    private void Start()
    {
        allCharList = GameManager.allChar;

        materialsList = new List<GameObject>();
        selectedList = new List<GameObject>();

        pool = GameObject.FindGameObjectWithTag("Pool");
        selectedPool = GameObject.Find("SelectedPool");

        idToEquip = GameManager.inst.charToEquipGear;
        charGO.transform.GetComponent<Character>().info = GameManager.inst.GetCharInfoById(idToEquip);
        Instantiate(charGO, charPos);

        SetAmounts();
        MaterialsInventory();
        InitBaseStatsTxt();
    }

    void SetAmounts()
    {
        PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("amountC"))
        {
            amountC = PlayerPrefs.GetInt("amountC");
        }
        else
        {
            amountC = 10;
        }

        if (PlayerPrefs.HasKey("amountR"))
        {
            amountR = PlayerPrefs.GetInt("amountR");
        }
        else
        {
            amountR = 10;
        }

        if (PlayerPrefs.HasKey("amountSR"))
        {
            amountSR = PlayerPrefs.GetInt("amountSR");
        }
        else
        {
            amountSR = 10;
        }
    }

    void MaterialsInventory()
    {
        SetMaterials(0, amountC);
        SetMaterials(1, amountR);
        SetMaterials(2, amountSR);
    }

    void SetMaterials(int type, int amount)
    {
        if (amount != 0)
        {
            materialGO.GetComponent<LevelUpItem>().type = type;
            materialGO.GetComponent<LevelUpItem>().amount = amount;
            materialGO.GetComponent<LevelUpItem>().position = materialsList.Count;
            materialGO.GetComponent<LevelUpItem>().selected = false;
            GameObject aux = Instantiate(materialGO, pool.transform);
            materialsList.Add(aux);
        }  
    }

    public void SelectMaterial(bool selecType, int type, int position)
    {
        switch(type)
        {
            case 0:
                SelectionManager(selecType, type, position, selectedC, amountC);
                break;
            case 1:
                SelectionManager(selecType, type, position, selectedR, amountR);
                break;
            case 2:
                SelectionManager(selecType, type, position, selectedSR, amountSR);
                break;
            default:
                break;
        }
        
    }

    void SelectionManager(bool selecType, int type, int position, int selected, int amount)
    {
        if (selected == 0)
        {
            selected++;
            materialGO.GetComponent<LevelUpItem>().type = type;
            materialGO.GetComponent<LevelUpItem>().amount = selected;
            materialGO.GetComponent<LevelUpItem>().position = selectedList.Count;
            materialGO.GetComponent<LevelUpItem>().selected = true;
            GameObject aux = Instantiate(materialGO, selectedPool.transform);
            selectedList.Add(aux);
            amount--;
            materialsList[position].transform.GetComponent<LevelUpItem>().SetAmount(amount);
        }
        else
        {
            selected++;
            for (int i = 0; i < selectedList.Count; i++)
            {
                if (selectedList[i].transform.GetComponent<LevelUpItem>().type == type)
                {
                    selectedList[i].transform.GetComponent<LevelUpItem>().SetAmount(selected);
                    Debug.Log("--------------");
                }
            }
            amount--;
            materialsList[position].transform.GetComponent<LevelUpItem>().SetAmount(amount);
        }
    }

    public void UpdateCharStats(bool add, Gear.Info ginfo)
    {
        if(add)
        {
            if (ginfo.objType == 0 || ginfo.objType == 3)
            {
                charGO.transform.GetComponent<Character>().info.stats.extraAtk += ginfo.statAmount;
            }
            else if (ginfo.objType == 1 || ginfo.objType == 4)
            {
                charGO.transform.GetComponent<Character>().info.stats.extraDef += ginfo.statAmount;
            }
            else if (ginfo.objType == 2 || ginfo.objType == 5)
            {
                charGO.transform.GetComponent<Character>().info.stats.extraHp += ginfo.statAmount;
            }
        }
        else
        {
            if (ginfo.objType == 0 || ginfo.objType == 3)
            {
                charGO.transform.GetComponent<Character>().info.stats.extraAtk -= ginfo.statAmount;
            }
            else if (ginfo.objType == 1 || ginfo.objType == 4)
            {
                charGO.transform.GetComponent<Character>().info.stats.extraDef -= ginfo.statAmount;
            }
            else if (ginfo.objType == 2 || ginfo.objType == 5)
            {
                charGO.transform.GetComponent<Character>().info.stats.extraHp -= ginfo.statAmount;
            }
        }
        charGO.transform.GetComponent<Character>().info.stats.UpdateStats();
        UpdateStatsTxt();
    }

    void InitBaseStatsTxt()
    {
        statsTxt[0].text = "ATK: " + charGO.transform.GetComponent<Character>().info.stats.baseAtk;
        statsTxt[1].text = "DEF: " + charGO.transform.GetComponent<Character>().info.stats.baseDef;
        statsTxt[2].text = "HP: " + charGO.transform.GetComponent<Character>().info.stats.baseHp;
    }

    void UpdateStatsTxt()
    {
        statsBonusTxt[0].text = "+" + charGO.transform.GetComponent<Character>().info.stats.extraAtk;
        statsBonusTxt[1].text = "+" + charGO.transform.GetComponent<Character>().info.stats.extraDef;
        statsBonusTxt[2].text = "+" + charGO.transform.GetComponent<Character>().info.stats.extraHp;
    }

    void AddSubtractStats(int statType, int amount)
    {
        if (statType == 0)
        {
            charGO.transform.GetComponent<Character>().info.stats.extraAtk += amount;
        }
        else if (statType == 1)
        {
            charGO.transform.GetComponent<Character>().info.stats.extraDef += amount;
        }
        else if (statType == 2)
        {
            charGO.transform.GetComponent<Character>().info.stats.extraHp += amount;
        }
    }

    public void SaveAmounts()
    {
        PlayerPrefs.SetInt("amountC", amountC);
        PlayerPrefs.SetInt("amountR", amountR);
        PlayerPrefs.SetInt("amountSR", amountSR);

        for (int i = 0; i < allCharList.Count; i++)
        {
            if (allCharList[i].id == idToEquip)
            {
                allCharList[i].level = charGO.transform.GetComponent<Character>().info.level;
            }
        }

        GameManager.inst.SaveListsToJson();
    }
}
