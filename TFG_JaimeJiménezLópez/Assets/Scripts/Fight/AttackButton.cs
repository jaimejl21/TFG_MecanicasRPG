using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public int id, charPos;
    public bool specialActivated = false, isAlive = true;

    public Button attackButton, specialButton;
    FightController fc;

    private void Start()
    {
        fc = FindObjectOfType<FightController>();
    }

    public void Attack()
    {
        fc.Attack();
    }

    public void SpecialAbility()
    {
        fc.SpecialAbility();
    }

    public void SetPlayerSelect()
    {
        fc.playerSelect = charPos;
    }
}