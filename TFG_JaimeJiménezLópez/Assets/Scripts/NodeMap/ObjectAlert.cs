using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectAlert : MonoBehaviour
{
    public TextMeshProUGUI[] objTxts;

    void Start()
    {
        
    }
    
    public void ActivateTexts(int num)
    {
        for(int i = 0; i < num; i++)
        {
            objTxts[i].gameObject.SetActive(true);
        }
    }

    public void DesactivateTxts()
    {
        for (int i = 0; i < 5; i++)
        {
            objTxts[i].gameObject.SetActive(false);
        }
    }
}
