using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameReferencesHolder _gameReferencesHolder;
    [SerializeField] private Transform mainMenuWindow;
    private void Start()
    {
        GenerateSpinByZone();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(_gameReferencesHolder.currentSpin.IsSpinning) return;
            
            if(!_gameReferencesHolder.Inventory.prizeCollected) return;

            _gameReferencesHolder.zone++;
            UIManager.instance.SetZoneText();
            _gameReferencesHolder.Inventory.prizeCollected = false;
            GenerateSpinByZone();
        }
    }

    private void GenerateSpin(string spinTag)
    {
        Spin spinInstance = Instantiate(_gameReferencesHolder.GetSpin(spinTag), mainMenuWindow);

        if (_gameReferencesHolder.currentSpin != null)
        {
            Destroy(_gameReferencesHolder.currentSpin.gameObject);   
        }

        _gameReferencesHolder.currentSpin = spinInstance;
    }

    private void GenerateSpinByZone()
    {
        if (_gameReferencesHolder.zone % 30 == 0)
        {
            GenerateSpin("GoldSpin");
        }
        else if (_gameReferencesHolder.zone % 5 == 0)
        {
            GenerateSpin("SilverSpin");
        }
        else
        {
            GenerateSpin("BronzeSpin");
        }
    }
}
