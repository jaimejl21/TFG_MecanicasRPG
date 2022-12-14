using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearManager : MonoBehaviour
{
    public GameObject charGO;
    public Transform charPos;

    public int idToEquip;

    private void Start()
    {
        idToEquip = GameManager.inst.charToEquipGear;
    }
}
