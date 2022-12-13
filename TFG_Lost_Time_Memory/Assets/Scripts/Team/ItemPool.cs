using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPool : MonoBehaviour, IDropHandler
{
    public TeamManager teamManager;
    public GameObject item;

    void Start()
    {
        teamManager = FindObjectOfType<TeamManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        item = DragHandler.itemDragging;

        //foreach (Character.Info c in teamManager.auxCharList)
        //{
        //    Debug.Log(c.id);
        //}

        if (item.GetComponent<Character>().info.inTeam)
        {
            item.GetComponent<Character>().info.inTeam = false;
            teamManager.noTeamCharList.Add(item.GetComponent<Character>().info);
            //Debug.Log("Add to pool " + item.GetComponent<Character>().info.id);
            //Debug.Log("Char list " + teamManager.auxCharList[(teamManager.auxCharList.Count - 1)].id + "   " + teamManager.auxCharList.Count);
        }

        DragHandler.itemDragging.transform.SetParent(transform);
        item.GetComponent<DragHandler>().slotParent = transform;

        //Debug.Log("Char list " + teamManager.auxCharList[(teamManager.auxCharList.Count - 1)].id + "   " + teamManager.auxCharList.Count);

        //foreach (Character.Info c in teamManager.auxCharList)
        //{
        //    Debug.Log(c.id);
        //}
    }
}
