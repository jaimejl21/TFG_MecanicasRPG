using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InteractGUI : MonoBehaviour
{
    [SerializeField] 
    private Image customImage;
    [FormerlySerializedAs("_npcRecruited")] [SerializeField]
    private bool npcRecruited;
    
    //GETTERS & SETTERS npcRecruited
    public bool GetNpcRecruited() {
        return npcRecruited;
    }

    public void SetNpcRecruited(bool npcRecruited) {
        this.npcRecruited = npcRecruited;
    }
    
    private void Awake()
    {
        npcRecruited = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && !npcRecruited)
        {
            customImage.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (npcRecruited)
        {
            customImage.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            customImage.enabled = false;
        }
    }
}
