using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GearManager : MonoBehaviour
{
    public GameObject charGO, pool, gear;
    public GameObject[] gearSlots;
    public Transform charPos;
    public TextMeshProUGUI[] statsTxt;

    public int idToEquip;
    public int atkGears = 0, defGears = 0, hpGears = 0;

    public List<Gear.Info> gearList;
    public List<Character.Info> allCharList;

    private void Start()
    {
        gearList = GameManager.allGear.ToList();
        allCharList = GameManager.allChar;

        pool = GameObject.FindGameObjectWithTag("Pool");

        idToEquip = GameManager.inst.charToEquipGear;
        charGO.transform.GetComponent<Character>().info = GameManager.inst.GetCharInfoById(idToEquip);
        Instantiate(charGO, charPos);

        UpdateStatsTxt();
        TeamSlots();
        GearInventory();
    }

    void TeamSlots()
    {
        for (int i = 0; i < gearList.Count; i++)
        {
            if (gearList[i].equiped && gearList[i].characterId == charGO.transform.GetComponent<Character>().info.id)
            {
                gear.transform.GetComponent<Gear>().info = gearList[i];
                int pos = gearList[i].objType;
                GameObject aux = Instantiate(gear, gearSlots[pos].transform);
                //aux.transform.GetComponent<GearItem>().SetGearColor();
                gearSlots[pos].GetComponent<GearDropSlot>().item = gearSlots[pos].transform.GetChild(0).gameObject;
                gearSlots[pos].transform.GetChild(0).GetComponent<GearDragHandler>().slotParent = gearSlots[pos].transform;
                gearSlots[pos].transform.GetChild(0).GetComponent<GearDragHandler>().startParent = pool.transform;
            }
        }
    }

    void GearInventory()
    {
        foreach (Gear.Info g in gearList)
        {
            gear.GetComponent<Gear>().info = g;
            gear.GetComponent<GearDragHandler>().slotParent = pool.transform;
            if (gear.GetComponent<Gear>().info.equiped != true)
            {
                GameObject aux = Instantiate(gear, pool.transform);
                //aux.transform.GetComponent<GearItem>().SetGearColor();
            }
        }
    }

    public void UpdateStatGear(bool add, Gear.Info ginfo)
    {
        if (add)
        {
            switch (ginfo.statType)
            {
                case 0:
                    atkGears++;
                    break;
                case 1:
                    defGears++;
                    break;
                case 2:
                    hpGears++;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (ginfo.statType)
            {
                case 0:
                    atkGears--;
                    break;
                case 1:
                    defGears--;
                    break;
                case 2:
                    hpGears--;
                    break;
                default:
                    break;
            }
        }
        UpdateCharStats(add, ginfo);
    }

    public void UpdateCharStats(bool add, Gear.Info ginfo)
    {
        if(add)
        {
            if (ginfo.objType == 0 || ginfo.objType == 3)
            {
                charGO.transform.GetComponent<Character>().info.stats.atk += ginfo.statAmount;
            }
            else if (ginfo.objType == 1 || ginfo.objType == 4)
            {
                charGO.transform.GetComponent<Character>().info.stats.def += ginfo.statAmount;
            }
            else if (ginfo.objType == 2 || ginfo.objType == 5)
            {
                charGO.transform.GetComponent<Character>().info.stats.hp += ginfo.statAmount;
            }
        }
        else
        {
            if (ginfo.objType == 0 || ginfo.objType == 3)
            {
                charGO.transform.GetComponent<Character>().info.stats.atk -= ginfo.statAmount;
            }
            else if (ginfo.objType == 1 || ginfo.objType == 4)
            {
                charGO.transform.GetComponent<Character>().info.stats.def -= ginfo.statAmount;
            }
            else if (ginfo.objType == 2 || ginfo.objType == 5)
            {
                charGO.transform.GetComponent<Character>().info.stats.hp -= ginfo.statAmount;
            }
        }
        UpdateStatsTxt();
    }

    public bool CanDropGear()
    {
        if (atkGears <= 0 && defGears <= 0 && hpGears <= 0)
        {
            return false;
        }
        else return true;
    }

    void UpdateStatsTxt()
    {
        statsTxt[0].text = "ATK: " + charGO.transform.GetComponent<Character>().info.stats.atk;
        statsTxt[1].text = "DEF: " + charGO.transform.GetComponent<Character>().info.stats.def;
        statsTxt[2].text = "HP: " + charGO.transform.GetComponent<Character>().info.stats.hp;
    }

    public void SaveGear()
    {
        for (int i = 0; i < 6; i++)
        {
            if (gearSlots[i].GetComponent<GearDropSlot>().item != null)
            {
                int pos_type = gearSlots[i].GetComponent<GearDropSlot>().slotPos;
                charGO.transform.GetComponent<Character>().info.gear.RemoveAt(pos_type);
                charGO.transform.GetComponent<Character>().info.gear.Insert(pos_type, gearSlots[i].GetComponent<GearDropSlot>().item.transform.GetComponent<Gear>().info);
            }
        }
        for(int i = 0; i < allCharList.Count; i++)
        {
            if(allCharList[i].id == idToEquip)
            {
                allCharList[i].stats = charGO.transform.GetComponent<Character>().info.stats;
            }
        }

        GameManager.allGear = gearList;
        GameManager.inst.SaveListsToJson();
    }
}
