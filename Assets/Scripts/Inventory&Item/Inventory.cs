using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryItem[] myItems;

    public bool prizeCollected = false;
    
    public InventoryItem GetInventoryItem(ItemData.ItemTypes itemType)
    {
        foreach (InventoryItem item in myItems)
        {
            if (item.GetData.ItemType == itemType)
            {
                return item;
                break;
            }
        }

        Debug.Log("There is no match item.");
        return null;
    }

    public void ResetMyInventory()
    {
        prizeCollected = true;
        
        foreach (InventoryItem item in myItems)
        {
            item.Reset();
        }
    }
}
