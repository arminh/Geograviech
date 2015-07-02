using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScollableContent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler
{
    private ScrollRect srcollview;

    public void Start()
    {
        srcollview = GetComponentInParent<ScrollRect>();
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        srcollview.OnInitializePotentialDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        srcollview.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        srcollview.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        srcollview.OnEndDrag(eventData);
    }
}
