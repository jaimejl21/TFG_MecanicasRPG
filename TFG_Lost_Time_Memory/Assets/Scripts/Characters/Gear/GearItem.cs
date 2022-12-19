using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GearItem : MonoBehaviour
{
    public TextMeshProUGUI text;
    string typeName;

    void Start()
    {
        SetName();
        gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "" + transform.GetComponent<Gear>().info.id + "\n" + typeName;
    }

    public void SetName()
    {
        int type = transform.GetComponent<Gear>().info.type;
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
}
