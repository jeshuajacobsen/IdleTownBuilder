using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostPurchaseButton : MonoBehaviour
{

    [SerializeField] private string boostName;

    void Start()
    {
        Button button = transform.GetComponent<Button>();
        button.onClick.AddListener(PurchaseBoost);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PurchaseBoost()
    {
        BoostManager.instance.PurchaseBoost(boostName);
    }
}
