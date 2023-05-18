using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernManager : MonoBehaviour
{
    public bool canAdvance;
    int nNodesMaps, actualCol;

    void Start()
    {
        nNodesMaps = PlayerPrefs.GetInt("nNodesMaps");
        actualCol = PlayerPrefs.GetInt("actualCol");

        if (GameManager.inst.death == true)
        {
            canAdvance = false;
            GameManager.inst.death = false;
        }
        else
        {
            canAdvance = true;
        }

        if(canAdvance)
        {
            nNodesMaps++;
            PlayerPrefs.SetInt("nNodesMaps", nNodesMaps);
        }

        GameManager.nodesLinesList.Clear();
        actualCol = 0;
        PlayerPrefs.SetInt("actualCol", actualCol);
    }
}
