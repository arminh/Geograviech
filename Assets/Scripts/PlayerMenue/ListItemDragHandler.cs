using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Assets;
using Assets.Scripts.Utils;
using Assets.Scripts.Items.Consumables;

public abstract class ListItemDragHandler : DragItemHandler
{
    public GameObject DraggedPrefab;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        ItemToBeDragged = Instantiate(DraggedPrefab).GetComponent<DragItemHandler>();

        Debug.Log("ListItemDragHandler - OnBeginDrag");

        var root = GameObject.Find("DragDropSlot");
        var rectTrans = ItemToBeDragged.transform as RectTransform;
        if (rectTrans && root)
        {
            rectTrans.SetParent(root.transform);
            ItemOriginalSlot = root.transform;
            rectTrans.localScale = new Vector3(1, 1, 1);
            startPositionSlot = rectTrans.localPosition;
            startPositionMouse = Vector3.zero;
        }

        var itemHandl = ItemToBeDragged.GetComponent<DragItemHandler>();
        itemHandl.OnItemCreated(this.Item);

        ItemCanvasGroup = ItemToBeDragged.GetComponent<CanvasGroup>();
        if (ItemCanvasGroup != null)
            ItemCanvasGroup.blocksRaycasts = false;
    }
	
	public override void OnEndDrag(PointerEventData eventData)
	{
        Debug.Log("ListItemDragHandler - OnEndDrag");

		var slot = ItemToBeDragged.transform.parent.GetComponent<ItemSlot>();
        base.OnEndDrag(eventData);

		if (slot && slot.type == this.type)
        {
            Debug.Log("ListItemDragHandler - Destroy");
            Destroy(this.gameObject);
		}
	}

    public override void OnPlaceInSlot(ItemSlot slot) 
    {
        Debug.Log("ListItemDragHandler - OnPlaceInSlot"); 
    }

    public override void OnChangeSlot(ItemSlot fromSlot, ItemSlot toSlot) 
    {
        Debug.Log("ListItemDragHandler - OnChangeSlot"); 
    }

    public override void OnRemoveFromSlot(ItemSlot slot) 
    {
        Debug.Log("ListItemDragHandler - OnRemoveFromSlot"); 
    }
}
