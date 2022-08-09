using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI zoneText;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetZoneText();
    }

    public void SetZoneText()
    {
        zoneText.text = $"ZONE {GameReferencesHolder.instance.zone}";
    }
}
