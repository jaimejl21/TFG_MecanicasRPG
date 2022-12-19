using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GearManager : MonoBehaviour
{
    public GameObject charGO, pool, gear;
    public Transform charPos;

    public int idToEquip;

    public List<Gear.Info> gearList;

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

    }
}
