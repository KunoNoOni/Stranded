using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    Vector3 dieSourcePosition;
    Transform dieSourceParent;
    Transform canvasTransform;
    private SoundManager sm;

    private void Awake()
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Start()
    {
        canvasTransform = FindObjectsOfType<Canvas>()[1].transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        sm.PlaySound(sm.sounds[0]);
        itemBeingDragged = gameObject;
        dieSourcePosition = transform.position;
        dieSourceParent = transform.parent;
        itemBeingDragged.transform.SetParent(canvasTransform);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (eventData.pointerEnter != null && eventData.pointerEnter.gameObject.tag == "SlotDiceTray")
        {
            PlaceDice(eventData);
            return;
        }

        if (eventData.pointerEnter != null && eventData.pointerEnter.gameObject.tag == "Slot" + itemBeingDragged.tag)
        {
            PlaceDice(eventData);
            return;
        }

        if (eventData.pointerEnter != null && itemBeingDragged.tag == "Dice6")
        {
            switch (eventData.pointerEnter.gameObject.tag)
            {
                case "SlotDice1":
                case "SlotDice2":
                case "SlotDice3":
                case "SlotDice4":
                case "SlotDice5":
                    PlaceDice(eventData);
                    break;
                default:
                    NoPlaceForDice();
                    break;
            }
            return;
        }

        NoPlaceForDice();
    }

    private void PlaceDice(PointerEventData eventData)
    {
        sm.PlaySound(sm.sounds[0]);
        transform.SetParent(eventData.pointerEnter.transform);
        transform.position = eventData.pointerEnter.transform.position;
    }

    private void NoPlaceForDice()
    {
        transform.position = dieSourcePosition;
        transform.SetParent(dieSourceParent);
        itemBeingDragged = null;
    }
}
