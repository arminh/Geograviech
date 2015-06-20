using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using Assets.Scripts.Consumables;
using UnityEngine.UI;
using Assets;

public abstract class ListItemDragHandler : DragItemHandler
{
    public GameObject DraggedPrefab;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        ItemToBeDragged = Instantiate(DraggedPrefab).GetComponent<DragItemHandler>();
        
        var root = GameObject.Find("DragDropSlot");
        var rectTrans = ItemToBeDragged.transform as RectTransform;
        if(rectTrans && root)
        {
            rectTrans.position = Input.mousePosition;
            rectTrans.SetParent(root.transform);
            ItemOriginalSlot = root.transform;
            rectTrans.localScale = new Vector3(1,1,1);        
        }

		var itemHandl = ItemToBeDragged.GetComponent<DragItemHandler>();
		itemHandl.Item = this.Item;
        var image = ItemToBeDragged.GetComponentInChildren<Image>();
        image.sprite = (this.Item as Item).Icon;

        ItemCanvasGroup = ItemToBeDragged.GetComponent<CanvasGroup>();
        if (ItemCanvasGroup != null)
            ItemCanvasGroup.blocksRaycasts = false;
    }
	
	public override void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("EndDrag");
		
		var slot = ItemToBeDragged.transform.parent.GetComponent<ItemSlot>();
		if (slot && slot.type == this.type)
		{
            OnPlaceInSlot(slot);
		}
		
		base.OnEndDrag(eventData);
	}

    public abstract void OnListItemCreated(object item);
}
