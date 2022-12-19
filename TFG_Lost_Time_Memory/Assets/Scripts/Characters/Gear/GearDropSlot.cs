using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearDropSlot : MonoBehaviour, IDropHandler
{
    public GearManager gearManager;
    public GameObject item;
    public int slotPos;
    public int type;

    void Start()
    {
        gearManager = FindObjectOfType<GearManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (GearDragHandler.itemDragging.GetComponent<Gear>().info.type == type)
        {
            if (!item)
            {
                Debug.Log("Opcion A");
                item = GearDragHandler.itemDragging;
                item.transform.SetParent(transform);
                item.transform.position = transform.position;
                item.GetComponent<GearDragHandler>().slotParent = transform;
                item.GetComponent<Gear>().info.equiped = true;

                for (int i = 0; i < gearManager.gearList.Count; i++)
                {
                    if (gearManager.gearList[i].id == item.GetComponent<Gear>().info.id)
                    {
                        gearManager.gearList[i].equiped = true;
                    }
                }
            }
            else
            {

                Debug.Log("Opcion B");
                item.transform.SetParent(GameObject.FindGameObjectWithTag("Pool").transform);
                item.transform.position = GameObject.FindGameObjectWithTag("Pool").transform.position;
                item.GetComponent<GearDragHandler>().slotParent = GameObject.FindGameObjectWithTag("Pool").transform;
                item.GetComponent<Gear>().info.equiped = false;
                for (int i = 0; i < gearManager.gearList.Count; i++)
                {
                    if (gearManager.gearList[i].id == item.GetComponent<Gear>().info.id)
                    {
                        gearManager.gearList[i].equiped = false;
                    }
                }

                item = GearDragHandler.itemDragging;
                item.transform.SetParent(transform);
                item.transform.position = transform.position;
                item.GetComponent<GearDragHandler>().slotParent = transform;
                item.GetComponent<Gear>().info.equiped = true;
                for (int i = 0; i < gearManager.gearList.Count; i++)
                {
                    if (gearManager.gearList[i].id == item.GetComponent<Gear>().info.id)
                    {
                        gearManager.gearList[i].equiped = true;
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
