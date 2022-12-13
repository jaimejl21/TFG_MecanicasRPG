using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ComboTxt : MonoBehaviour
{
    AnimationsManager am;

    void Start()
    {
        am = GameObject.Find("AnimationsManager").GetComponent<AnimationsManager>();
    }

    public void AnimFinished()
    {
        am.animComboTxt.SetBool("Combo", false);
    }
}
