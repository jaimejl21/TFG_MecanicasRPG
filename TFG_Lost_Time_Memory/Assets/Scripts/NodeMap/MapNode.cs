using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapNode : MonoBehaviour
{
    public int id, type;
    bool selected = false;
    string txt;

    public List<MapNode> nextNodes, prevNodes;
    public GameObject parentColumn; 

    void Start()
    {
        txt = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        SetTypeVars();
    }

    public void SelectNode()
    {
        selected = true;
    }

    void SetTypeVars()
    {
        switch(type)
        {
            case 0:
                txt = "Fight";
                break;
            case 1:
                txt = "Merchant";
                break;
            case 2:
                txt = "Blacksmith";
                break;
            case 3:
                txt = "Team";
                break;
            case 4:
                txt = "Recruit";
                break;
            default:
                break;
        }
    }
}
