using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReferencesHolder : MonoBehaviour
{
    public static GameReferencesHolder instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Spin[] spinPrefabs; 
    
    public Inventory Inventory;
    public PlayerController player;
    public Spin currentSpin;
    public int zone;
    public Item[] items;
    
    public Spin GetSpin(string tag)
    {
        foreach (Spin spin in spinPrefabs)
        {
            if (spin.tag.Equals(tag)) return spin;
        }

        Debug.Log("Spin tag is wrong.");
        return null;
    }
}
