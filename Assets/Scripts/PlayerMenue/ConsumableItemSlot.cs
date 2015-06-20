using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using Assets.Scripts.PlayerMenue;
using Assets.Scripts.Consumables;

public class ConsumableItemSlot : MonoBehaviour, IDropHandler 
{
    public IConsumableInteraction manager;

    public void Awake()
    {
        manager = GetComponentInParent<IConsumableInteraction>();
        Debug.Log(manager);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (DragItemHandler.ItemToBeDragged.type == Enums.ItemType.Consumable)
        {
            var item = DragItemHandler.ItemToBeDragged.Item;
            manager.Consume(item as IConsumable);
        }
    }
}
