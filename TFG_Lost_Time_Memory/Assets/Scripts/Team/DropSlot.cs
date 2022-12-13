using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public TeamManager teamManager;
    public GameObject item;
    public int slotPos;

    void Start()
    {
        teamManager = FindObjectOfType<TeamManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(!item)
        {
            Debug.Log("Opcion A");
            //Debug.Log(teamManager.CanAddToTeam().ToString());
            if (teamManager.CanAddToTeam())
            {
                item = DragHandler.itemDragging;
                item.transform.SetParent(transform);
                item.transform.position = transform.position;
                item.GetComponent<DragHandler>().slotParent = transform;
                item.GetComponent<Character>().info.inTeam = true;
                //Debug.Log("inTeam: " + item.GetComponent<Character>().info.inTeam);
                for (int i = 0; i < teamManager.noTeamCharList.Count; i++)
                {
                    if (teamManager.noTeamCharList[i].id == item.GetComponent<Character>().info.id)
                    {
                        teamManager.noTeamCharList.RemoveAt(i);
                    }
                }

                //Debug.Log(teamManager.auxCharList.Contains(item.GetComponent<Character>().info) + "   " + teamManager.auxCharList.Count);
                //Debug.Log("Count: " + teamManager.auxCharList.Count);

                //foreach (Character.Info c in teamManager.auxCharList)
                //{
                //    Debug.Log(c.id);
                //}
            } 
        }
        else
        {
            if(DragHandler.itemDragging.GetComponent<Character>().info.inTeam)
            {
                Debug.Log("Opcion B");
                item.transform.SetParent(DragHandler.itemDragging.GetComponent<DragHandler>().slotParent);
                item.transform.position = DragHandler.itemDragging.GetComponent<DragHandler>().slotParent.position;
                DragHandler.itemDragging.GetComponent<DragHandler>().slotParent.GetComponent<DropSlot>().item = item;
                item.GetComponent<DragHandler>().slotParent = DragHandler.itemDragging.GetComponent<DragHandler>().slotParent;
                item = DragHandler.itemDragging;
                item.transform.SetParent(transform);
                item.transform.position = transform.position;
                item.GetComponent<DragHandler>().slotParent = transform;
            }
            else
            {
                Debug.Log("Opcion C");
                item.transform.SetParent(GameObject.FindGameObjectWithTag("Pool").transform);
                item.transform.position = GameObject.FindGameObjectWithTag("Pool").transform.position;
                item.GetComponent<DragHandler>().slotParent = GameObject.FindGameObjectWithTag("Pool").transform;
                item.GetComponent<Character>().info.inTeam = false;
                teamManager.noTeamCharList.Add(item.GetComponent<Character>().info);
                //Debug.Log("Add to pool " + item.GetComponent<Character>().info.id);
                //Debug.Log("Char list " + teamManager.auxCharList[(teamManager.auxCharList.Count - 1)].id + "   " + teamManager.auxCharList.Count);
                //foreach (Character.Info c in teamManager.auxCharList)
                //{
                //    Debug.Log(c.id);
                //}

                item = DragHandler.itemDragging;
                item.transform.SetParent(transform);
                item.transform.position = transform.position;
                item.GetComponent<DragHandler>().slotParent = transform;
                item.GetComponent<Character>().info.inTeam = true;
                for (int i = 0; i < teamManager.noTeamCharList.Count; i++)
                {
                    if (teamManager.noTeamCharList[i].id == item.GetComponent<Character>().info.id)
                    {
                        teamManager.noTeamCharList.RemoveAt(i);
                    }
                }
            } 
        }
    }

    void Update()
    {
        if (item != null && item.transform.parent != transform)
        {
            item = null;
        }
    }
}
