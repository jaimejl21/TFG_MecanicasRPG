using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    public Animator animComboTxt;

    void Start()
    {
        animComboTxt = GameObject.Find("ComboTxt").GetComponent<Animator>();
    }

    public void PlayAnimState(Animator anim, string state)
    {
        anim.Play(state, -1, 0f);
    }

    public void NextAnim()
    {

    }
}
