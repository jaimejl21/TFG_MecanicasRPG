using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GearManager : MonoBehaviour
{
    public GameObject charGO, pool, gear;
    public GameObject[] gearSlots;
    public Transform charPos;

    public int idToEquip;

    public List<Gear.Info> gearList;
    public List<Character.Info> allCharList;

    private void Start()
    {
        gearList = GameManager.allGear.ToList();
        allCharList = GameManager.allChar.ToList();

        pool = GameObject.FindGameObjectWithTag("Pool");

        idToEquip = GameManager.inst.charToEquipGear;
        charGO.transform.GetComponent<Character>().info = GameManager.inst.GetCharInfoById(idToEquip);
        Instantiate(charGO, charPos);

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
                int pos = gearList[i].type;
                Instantiate(gear, gearSlots[pos].transform);
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
            if (gear.GetComponent<Gear>().info.equiped != true)
            {
                Instantiate(gear, pool.transform);
                Debug.Log("");
            }
        }
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
        for (int i = 0; i < 6; i++)
        {
            Debug.Log(charGO.transform.GetComponent<Character>().info.gear[i].id);
        }

        GameManager.allGear = gearList;
        GameManager.inst.SaveListsToJson();
    }
}
