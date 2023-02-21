using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlacksmithManager : MonoBehaviour
{
    public GameObject pool, blacksmithItem, goSelected;
    public Transform itemPos;
    public List<Gear.Info> gearList;
    public TextMeshProUGUI[] itemInfoTxts;
    public TextMeshProUGUI coinsTxt;
    public Button[] btns;

    public int coins;

    private void Start()
    {
        gearList = GameManager.allGear.ToList();

        coins = GameManager.inst.coins;
        coinsTxt.text = "Coins: " + coins;

        SetInventory(gearList);
    }

    void UpdateCoinsTxt()
    {
        coinsTxt.text = "Coins: " + coins;
        GameManager.inst.coins = coins;
    }

    public void ChangeItemInfo(GameObject go)
    {
        goSelected = go;
        if(itemPos.childCount != 0)
        {
            Destroy(itemPos.GetChild(0).gameObject);
        }
        blacksmithItem.GetComponent<Gear>().info = go.GetComponent<Gear>().info;
        //blacksmithItem.GetComponent<BlacksmithItem>().price = go.GetComponent<MerchantItem>().price;
        Instantiate(blacksmithItem, itemPos);
        //itemInfoTxts[0].text = "Price: " + blacksmithItem.GetComponent<MerchantItem>().price;
        itemInfoTxts[1].text = "Level: " + 1;
        itemInfoTxts[2].text = "Stats: " + blacksmithItem.GetComponent<Gear>().info.statAmount;    
        if(btns[0].interactable == false)
        {
            if (goSelected.GetComponent<MerchantItem>().price > coins)
            {
                btns[2].interactable = false;
            }
            else
            {
                btns[2].interactable = true;
            }
        }else if(btns[1].interactable == false)
        {
            btns[3].interactable = true;
        }

    }

    void ResetItemInfo()
    {
        if (itemPos.childCount != 0)
        {
            Destroy(itemPos.GetChild(0).gameObject);
        }

        itemInfoTxts[0].text = "Price: ";
        itemInfoTxts[1].text = "Level: ";
        itemInfoTxts[2].text = "Stats: ";

        btns[2].interactable = false;
        btns[3].interactable = false;

        goSelected = null;
    }

    public void SetInventory(List<Gear.Info> list)
    {
        foreach (Gear.Info g in list)
        {
            blacksmithItem.GetComponent<Gear>().info = g;
            Instantiate(blacksmithItem, pool.transform);
        }
    }
}
