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
        ResetItemInfo();
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
        Instantiate(blacksmithItem, itemPos);
        itemInfoTxts[0].text = "Augment: " + blacksmithItem.GetComponent<Gear>().info.augment;
        itemInfoTxts[1].text = "Stars: " + blacksmithItem.GetComponent<Gear>().info.stars;
        itemInfoTxts[2].text = "Stats: " + blacksmithItem.GetComponent<Gear>().info.statAmount;
        if (goSelected.GetComponent<BlacksmithItem>().upgrade)
        {
            itemInfoTxts[3].text = "Materials: " + goSelected.GetComponent<BlacksmithItem>().upMat;
            itemInfoTxts[4].text = "Coins: " + goSelected.GetComponent<BlacksmithItem>().upPrice;
            btns[0].gameObject.SetActive(true);
            btns[1].gameObject.SetActive(false);
            btns[0].interactable = true;
            btns[1].interactable = false;
        }
        else
        {
            itemInfoTxts[3].text = "Materials: " + goSelected.GetComponent<BlacksmithItem>().awMat;
            itemInfoTxts[4].text = "Coins: " + goSelected.GetComponent<BlacksmithItem>().awPrice;
            btns[0].gameObject.SetActive(false);
            btns[1].gameObject.SetActive(true);
            btns[0].interactable = false;
            btns[1].interactable = true;
        }      
    }

    void ResetItemInfo()
    {
        if (itemPos.childCount != 0)
        {
            Destroy(itemPos.GetChild(0).gameObject);
        }

        itemInfoTxts[0].text = "Augment: ";
        itemInfoTxts[1].text = "Stars: ";
        itemInfoTxts[2].text = "Stats: ";
        itemInfoTxts[3].text = "Materials: ";
        itemInfoTxts[4].text = "Coins: ";

        btns[0].gameObject.SetActive(true);
        btns[1].gameObject.SetActive(false);
        btns[0].interactable = false;
        btns[1].interactable = false;

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
