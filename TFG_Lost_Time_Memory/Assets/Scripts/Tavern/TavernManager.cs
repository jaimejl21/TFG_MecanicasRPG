using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernManager : MonoBehaviour
{
    public bool canAdvance;
    int nNodesMaps;

    void Start()
    {
        nNodesMaps = PlayerPrefs.GetInt("nNodesMaps");

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
    }
}
