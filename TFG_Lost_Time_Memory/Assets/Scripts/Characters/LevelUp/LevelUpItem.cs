using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelUpItem : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI[] texts;

    public int statsAm, type, amount, position;
    string typeTxt;
    bool selected = false;

    LevelUpMananager lum;

    void Start()
    {
        lum = FindObjectOfType<LevelUpMananager>();
        
        SetItemInfo();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        lum.SelectMaterial(type);
    }

    public void OnPointerDown(PointerEventData eventData) { }

    public void OnPointerUp(PointerEventData eventData) { }

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
