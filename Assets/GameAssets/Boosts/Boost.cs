using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boost : MonoBehaviour
{
    
    [SerializeField] private string boostName;

    void Start()
    {
        transform.Find("UseBoostButton").GetComponent<Button>().onClick.AddListener(UseBoost);
        BoostManager.instance.onBoostQuantityChange.AddListener((string name, int quantity) => {
            if (name == boostName) {
                transform.Find("QuantityText").GetComponent<TextMeshProUGUI>().text = "X" + quantity;
            }
        });

        if (BoostManager.instance.boostsInventory.ContainsKey(boostName))
        {
            transform.Find("QuantityText").GetComponent<TextMeshProUGUI>().text = "X" + BoostManager.instance.boostsInventory[boostName];
        }
        else
        {
            transform.Find("QuantityText").GetComponent<TextMeshProUGUI>().text = "X0";
        }
    }

    void Update()
    {
        if (BoostManager.instance.boostTimes.ContainsKey(boostName))
        {
            transform.Find("ActiveTime").GetComponent<TextMeshProUGUI>().text = "" + BoostManager.instance.boostTimes[boostName];
        }
        else
        {
            transform.Find("ActiveTime").GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void UseBoost()
    {
        if (boostName == "TimeWarp")
        {
            TimeManager.instance.LoadTime("PauseTime", true);
            BoostManager.instance.UseTimeBoost(boostName);
        }
        else
        {
            BoostManager.instance.UseBoost(boostName, 360);
        }
    }
}
