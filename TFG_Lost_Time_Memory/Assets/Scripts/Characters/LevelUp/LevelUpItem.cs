using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpItem : MonoBehaviour
{
    public TextMeshProUGUI[] texts;

    public int statsAm;
    int type;
    string typeTxt;

    void Start()
    {
        SetType();
    }

    void SetType()
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
        }
        texts[1].text = typeTxt;
    }
}
