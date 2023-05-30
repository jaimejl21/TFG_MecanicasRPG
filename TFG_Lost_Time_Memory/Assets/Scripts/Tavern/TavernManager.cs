using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernManager : MonoBehaviour
{
    public bool canAdvance;
    //int nNodesMaps;
    int actualCol;

    void Start()
    {
        //nNodesMaps = PlayerPrefs.GetInt("nNodesMaps");
        //actualCol = PlayerPrefs.GetInt("actualCol");

        //if (GameManager.inst.death == true)
        //{
        //    canAdvance = false;
        //}
        //else
        //{
        //    canAdvance = true;
        //}

        //if(canAdvance)
        //{
        //    //PlayerPrefs.SetInt("nNodesMaps", nNodesMaps);
        //}
        actualCol = 0;
        PlayerPrefs.SetInt("actualCol", actualCol);
    }
}
