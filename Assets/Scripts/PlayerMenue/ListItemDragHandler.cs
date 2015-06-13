using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;

public class ListItemDragHandler : DragItemHandler
{
    public GameObject DraggedPrefab;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        ItemToBeDragged = Instantiate(DraggedPrefab).GetComponent<DragItemHandler>();
        ItemOriginalSlot = transform.parent;

        var root = GameObject.Find("DragDropSlot");
        var rectTrans = ItemToBeDragged.transform as RectTransform;
        if(rectTrans && root)
        {
            rectTrans.position = Input.mousePosition;
            rectTrans.SetParent(root.transform);
            rectTrans.localScale = new Vector3(1,1,1);
            Debug.Log(rectTrans.sizeDelta);
            Debug.Log(rectTrans.rect);
            rectTrans.sizeDelta = new Vector2(rectTrans.sizeDelta.x, rectTrans.sizeDelta.x);
        
        }
        ItemCanvasGroup = ItemToBeDragged.GetComponent<CanvasGroup>();
        if (ItemCanvasGroup != null)
            ItemCanvasGroup.blocksRaycasts = false;
    }
}
