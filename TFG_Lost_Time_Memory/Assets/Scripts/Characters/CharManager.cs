using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharManager : MonoBehaviour
{
    public GameObject pool, charSlotGO;
    public List<Character.Info> auxCharList;
    public ScrollRect sr;

    private void Start()
    {
        auxCharList = GameManager.allChar;

        CharInventory();
        sr.verticalNormalizedPosition = 1f;
    }

    private void CharInventory()
    {
        foreach (Character.Info c in auxCharList)
        {
            charSlotGO.GetComponent<Character>().info = c;
            Instantiate(charSlotGO, pool.transform);
        }
    }
}
