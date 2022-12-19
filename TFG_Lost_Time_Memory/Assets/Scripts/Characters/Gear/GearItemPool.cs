using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearItemPool : MonoBehaviour, IDropHandler
{
    public GearManager gearManager;
    public GameObject item;

    void Start()
    {
        gearManager = FindObjectOfType<GearManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        item = GearDragHandler.itemDragging;

        if (item.GetComponent<Gear>().info.equiped)
        {
            item.GetComponent<Gear>().info.equiped = false;
            for (int i = 0; i < gearManager.gearList.Count; i++)
            {
                if (gearManager.gearList[i].id == item.GetComponent<Gear>().info.id)
                {
                    gearManager.gearList[i].equiped = false;
                }
            }
        }

        GearDragHandler.itemDragging.transform.SetParent(transform);
        item.GetComponent<GearDragHandler>().slotParent = transform;
    }
}
