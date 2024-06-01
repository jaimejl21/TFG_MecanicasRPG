using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AllyNpcsController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _npcAllies;
    private int nCompañerosMAX = 5;
    private Boolean triggered;
    private Collider2D auxCollider;
    

    private void Awake()
    {
        _npcAllies = new List<GameObject>(nCompañerosMAX);
        triggered = false;
        auxCollider = null;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_npcAllies.Capacity > 0 && _npcAllies.Count!=0)
        {
            for (int i = 0; i < _npcAllies.Count ; i++)
            {
                _npcAllies[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        triggered = true;
        auxCollider = other;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (triggered && auxCollider.CompareTag("NPC"))
        {
            if (_npcAllies.Count < 5)
            {
                if (!_npcAllies.Contains(auxCollider.gameObject))
                {
                    _npcAllies.Add(auxCollider.gameObject);
                    auxCollider.gameObject.GetComponent<InteractGUI>().SetNpcRecruited(true);
                }
            }
        }
    }
}
