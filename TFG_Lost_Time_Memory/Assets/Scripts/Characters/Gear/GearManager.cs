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
    public List<Gear.Info> equipedGearList;

    private void Start()
    {
        gearList = GameManager.allGear.ToList();

        pool = GameObject.FindGameObjectWithTag("Pool");

        idToEquip = GameManager.inst.charToEquipGear;
        charGO.transform.GetComponent<Character>().info = GameManager.inst.GetCharInfoById(idToEquip);
        Instantiate(charGO, charPos);

        GearInventory();
    }

    void GearInventory()
    {
        foreach (Gear.Info g in gearList)
        {
            gear.GetComponent<Gear>().info = g;
            if (gear.GetComponent<Gear>().info.equiped != true)
            {
                Instantiate(gear, pool.transform);
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
                Gear.Info gear = new Gear.Info(gearSlots[i].GetComponent<GearDropSlot>().item.GetComponent<Gear>().info.id, pos_type, true);
                //.RemoveAt(i);
                //inTeamCharList.Insert(i, inSlotChar);
                //.Add();
            }
        }
    }
}
