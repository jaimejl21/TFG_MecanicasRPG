using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpItem : MonoBehaviour
{
    public TextMeshProUGUI[] texts;

    public int statsAm, type, amount;
    string typeTxt;

    void Start()
    {
        SetItemInfo();
    }

    void SetItemInfo()
    {
        switch(type)
        {
            case 0:
                typeTxt = "C";
                break;
            case 1:
                typeTxt = "R";
                break;
            case 2:
                typeTxt = "SR";
                break;
            default:
                break;
        }
        texts[1].text = typeTxt;

        switch (typeTxt)
        {
            case "C":
                statsAm =  10;
                break;
            case "R":
                statsAm = 20;
                break;
            case "SR":
                statsAm = 30;
                break;
            default:
                break;
        }

        texts[0].text = amount.ToString();
    }
}
