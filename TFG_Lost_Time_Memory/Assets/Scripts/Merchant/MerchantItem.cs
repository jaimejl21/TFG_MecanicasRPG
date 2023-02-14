using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MerchantItem : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    string typeName;
    public int price;

    MerchantManager mm;

    public TextMeshProUGUI text;

    [SerializeField]
    Color statTypeColor;

    void Start()
    {
        mm = FindObjectOfType<MerchantManager>();
        
        SetName();
        gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "" + transform.GetComponent<Gear>().info.id + "\n" + typeName;
        SelectGearColor();
        SetPrice();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        mm.ChangeItemInfo(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData) { }

    public void OnPointerUp(PointerEventData eventData) { }

    public void SetName()
    {
        int type = transform.GetComponent<Gear>().info.objType;
        switch(type)
        {
            case 0:
                typeName = "Bracer";
                break;
            case 1:
                typeName = "Neck";
                break;
            case 2:
                typeName = "Belt";
                break;
            case 3:
                typeName = "Ring";
                break;
            case 4:
                typeName = "Earring";
                break;
            case 5:
                typeName = "Orb";
                break;
        }
    }

    void SelectGearColor()
    {
        int statType = gameObject.transform.GetComponent<Gear>().info.statType;
        switch(statType)
        {
            case 0:
                //statTypeColor = new Color(246f, 153f, 133f, 255f);
                statTypeColor = Color.red;
                break;
            case 1:
                //statTypeColor = new Color(189f, 133f, 246f, 255f);
                statTypeColor = Color.blue;
                break;
            case 2:
                //statTypeColor = new Color(133f, 225f, 246f, 255f);
                statTypeColor = Color.green;
                break;
            default:
                break;
        }
        gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = statTypeColor;
    }

    public void SetGearColor()
    {
        gameObject.transform.GetComponent<Image>().color = statTypeColor;
    }

    public void SetPrice()
    {
        int rarity = gameObject.GetComponent<Gear>().info.rarity;
        switch(rarity)
        {
            case 0:
                price = 60;
                break;
            case 1:
                price = 120;
                break;
            case 2:
                price = 180;
                break;
            default:
                break;
        }
    }
}
