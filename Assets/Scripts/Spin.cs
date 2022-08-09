using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

[System.Serializable]
public class Slot
{
    public Transform slotTransform;
    public Item item;
    [Range(1, 10)] 
    public int chance = 1;
}

public class Spin : MonoBehaviour
{
    [SerializeField] private Transform spinBase;
    [SerializeField] private List<Slot> prizes;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private Button spinButton;
    [SerializeField] private bool isManuel = false;

    public bool IsSpinning => spinning;
    public Item GetPrize => prizeItem;
    
    // private variables
    private List<int> totalChances;
    private bool spinning = false;
    private float anglePerItem;
    private int probabilitySpace = 0;
    private int randomTime;
    private int itemNumber;

    private Item prizeItem;

    private void Start()
    {
        if(!isManuel) FillSlots();
        
        spinning = false;
        anglePerItem = 360 / prizes.Count;

        spinButton.onClick.AddListener(SpinTheWheel);
        
        totalChances = new List<int>();

        for (int i = 0; i < prizes.Count; i++)
        {
            totalChances.Add(prizes[i].chance);
            probabilitySpace += prizes[i].chance;
        }
    }

    public void SpinTheWheel()
    {
        spinButton.gameObject.SetActive(false);
        
        randomTime = Random.Range(1, 3);

        int randomNumbInSpace = Random.Range(1, probabilitySpace + 1);

        int total = 0;

        
        for (int i = 0; i < totalChances.Count; i++)
        {
            total += totalChances[i];
            
            if (randomNumbInSpace <= total)
            {
                itemNumber = i;
                break;
            }
        }
        
        Debug.Log("Item number: " + itemNumber);
        
        float maxAngle = 360 * randomTime + (itemNumber * anglePerItem); 
        StartCoroutine(SpinTheWheel(5 * randomTime, maxAngle));
    }

    private IEnumerator SpinTheWheel(float time, float maxAngle)
    {
        spinning = true;
        float timer = 0.0f;
        float startAngle = spinBase.eulerAngles.z;
        maxAngle -= startAngle;

        while (timer < time)
        {
            float angle = maxAngle * animationCurve.Evaluate(timer / time);
            spinBase.eulerAngles = new Vector3(0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            yield return 0;
        }

        spinBase.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle + startAngle);
        spinning = false;

        prizeItem = prizes[itemNumber].item;
        if (prizeItem.GetItemType == ItemData.ItemTypes.BOMB)
        {
            Debug.Log("bomb");
            GameReferencesHolder.instance.Inventory.ResetMyInventory();
        }
        else
        {
            prizeItem.ActivedShine();    
        }
    }

    private void FillSlots()
    {
        if (gameObject.tag.Equals("BronzeSpin"))
        {
            int randomSlot = Random.Range(0, prizes.Count);

            foreach (Item item in GameReferencesHolder.instance.items)
            {
                if (item.GetItemType == ItemData.ItemTypes.BOMB)
                {
                    Debug.Log("bombayi uret");
                    
                    Item bombItem = Instantiate(item,
                        prizes[randomSlot].slotTransform);
                    
                    bombItem.transform.localPosition = Vector3.zero;

                    prizes[randomSlot].item = bombItem;
                    prizes[randomSlot].chance = Random.Range(1, 3);
                    break;
                }
            }

            for (int i = 0; i < prizes.Count; i++)
            {
                if (prizes[i].item == null)
                {
                    int randomItemNumb = Random.Range(0, GameReferencesHolder.instance.items.Length);
                 
                    while (GameReferencesHolder.instance.items[randomItemNumb].GetItemType == ItemData.ItemTypes.BOMB)
                    {
                        randomItemNumb = Random.Range(0, GameReferencesHolder.instance.items.Length);
                    }
                    
                    Item itemInstance = Instantiate(GameReferencesHolder.instance.items[randomItemNumb],
                        prizes[i].slotTransform);

                    itemInstance.transform.localPosition = Vector3.zero;
                    prizes[i].item = itemInstance;
                    prizes[i].chance = Random.Range(1, 8);
                }
            }
        }
        else
        {
            for (int i = 0; i < prizes.Count; i++)
            {
                int randomItemNumb = Random.Range(0, GameReferencesHolder.instance.items.Length);
                
                while (GameReferencesHolder.instance.items[randomItemNumb].GetItemType == ItemData.ItemTypes.BOMB)
                {
                    randomItemNumb = Random.Range(0, GameReferencesHolder.instance.items.Length);
                }
                
                Item itemInstance = Instantiate(GameReferencesHolder.instance.items[randomItemNumb],
                                                                            prizes[i].slotTransform);

                itemInstance.transform.localPosition = Vector3.zero;
                prizes[i].item = itemInstance;
                prizes[i].chance = Random.Range(1, 8);
            }
        }
    }
}
