using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentAmountText;
    [SerializeField] private ItemData data;

    // private variables
    private int currentAmount = 0;

    public ItemData GetData { get { return data; } }
    
    public void DealAmount(int amount)
    {
        currentAmount += amount;
        currentAmountText.text = currentAmount.ToString();
    }

    public void Reset()
    {
        currentAmount = 0;
        currentAmountText.text = currentAmount.ToString();
    }
}
