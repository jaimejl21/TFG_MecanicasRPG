using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernManager : MonoBehaviour
{
    bool restartSr;
    int actualCol;

    void Start()
    {
        restartSr = true;
        GameManager.inst.restartSr = restartSr;
        actualCol = 0;
        PlayerPrefs.SetInt("actualCol", actualCol);
    }
}
