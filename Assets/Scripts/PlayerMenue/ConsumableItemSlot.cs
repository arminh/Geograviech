using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Utils;
using Assets.Scripts.PlayerMenue;
using Assets.Scripts.Items.Consumables;

public class ConsumableItemSlot : MonoBehaviour, IDropHandler 
{
    public IConsumableInteraction manager;

    public void Awake()
    {
        manager = GetComponentInParent<IConsumableInteraction>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("ConsumableItemSlot - OnDrop");
        if (DragItemHandler.ItemToBeDragged.type == Enums.ItemType.Consumable)
        {
            var item = DragItemHandler.ItemToBeDragged.Item;
            manager.Consume(item as IConsumable);
        }
    }
}
