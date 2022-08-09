using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData info;
    [SerializeField] private GameObject shineObj;
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(ToTheInventory);
    }

    private void Update()
    {
        transform.rotation = quaternion.identity;
        if (shineObj.activeInHierarchy)
        {
            shineObj.transform.Rotate(Vector3.forward * Time.deltaTime * 50);
        }
    }

    public void ToTheInventory()
    {
        if (GameReferencesHolder.instance.currentSpin.GetPrize == this)
        {
            Transform targetInventory = GameReferencesHolder.instance.Inventory.GetInventoryItem(GetItemType).transform;

            transform.DOMove(targetInventory.position, 1f).OnComplete(() =>
            {
                GameReferencesHolder.instance.Inventory.GetInventoryItem(GetItemType).DealAmount(1);
                GameReferencesHolder.instance.Inventory.prizeCollected = true;
                Destroy(gameObject);
            });
        }
    }

    public string GetName => info.name;
    public ItemData.ItemTypes GetItemType => info.ItemType;
    public void ActivedShine() => shineObj.SetActive(true);
}
