using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MerchantManager : MonoBehaviour
{
    public GameObject pool, merchantItem;

    public Transform itemPos;

    public List<Gear.Info> sellGearList;

    public TextMeshProUGUI[] itemInfoTxts;

    private void Start()
    {
        sellGearList = GameManager.allGear.ToList();

        //for (int i = 0; i < auxCharList.Count; i++)
        //{
        //    Debug.Log(" Pos " + i + ": " +  auxCharList[i].id);
        //}

        SellGearInventory();
    }

    void SellGearInventory()
    {
        foreach (Gear.Info g in sellGearList)
        {
            merchantItem.GetComponent<Gear>().info = g;
            if (merchantItem.GetComponent<Gear>().info.equiped != true)
            {
                Instantiate(merchantItem, pool.transform);
            }
        }
    }
}
