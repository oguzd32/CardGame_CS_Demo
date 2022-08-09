using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "itemData", menuName = "Item Data", order = 51)]
public class ItemData : ScriptableObject
{
    public enum ItemTypes
    {
        RIFLE, BOMB, ARMOR, KNIFE, PISTOL, SHOTGUN, SMG, SNIPER, SUBMACHINE, VEST
    }

    [SerializeField] private string itemName;
    [SerializeField] private ItemTypes itemType = ItemTypes.RIFLE;

    public string Name { get { return itemName; } }
    public ItemTypes ItemType { get { return itemType; } }
}
