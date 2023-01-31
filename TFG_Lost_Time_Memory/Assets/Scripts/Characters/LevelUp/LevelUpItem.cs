using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelUpItem : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI typeTxt, amountTxt;

    public int statsAm, amount, type;
    public bool selected;

    LevelUpMananager lum;

    void Start()
    {
        lum = FindObjectOfType<LevelUpMananager>();
        
        SetItemInfo();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        lum.SelectMaterial(selected, type);
        //Debug.Log("Click type " + type + " position " + position);
    }

    public void OnPointerDown(PointerEventData eventData) { }

    public void OnPointerUp(PointerEventData eventData) { }

    void SetItemInfo()
    {
        switch(type)
        {
            case 0:
                typeTxt.text = "C";
                break;
            case 1:
                typeTxt.text = "R";
                break;
            case 2:
                typeTxt.text = "SR";
                break;
            default:
                break;
        }

        switch (typeTxt.text)
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

        amountTxt.text = "" + amount;
    }

    public void SetAmount(int am)
    {
        amount = am;
        amountTxt.text = "" + amount;
        Debug.Log("Amount changed to " + amount);
    }
}
