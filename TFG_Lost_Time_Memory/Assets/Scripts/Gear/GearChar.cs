using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearChar : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "" + transform.GetComponent<Character>().info.id;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("Selected character: " + transform.GetComponent<Character>().info.id);
    }

    public void OnPointerDown(PointerEventData eventData) { }

    public void OnPointerUp(PointerEventData eventData) { }
}
