using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class BoostManager : MonoBehaviour
{

    public static BoostManager instance;

    public Dictionary<string, int> boostsInventory = new Dictionary<string, int>();
    
    public Dictionary<string, int> boostTimes = new Dictionary<string, int>();

    public UnityEvent<string, int> onBoostQuantityChange = new UnityEvent<string, int>();

    [SerializeField] private BoostsPanel boostsPanel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        InvokeRepeating("TickAll", 1.0f, 1.0f);
        
    }

    void Update()
    {
        
    }

    public void ToggleBoostsPanel()
    {
        boostsPanel.Toggle();
    }

    public void TickAll()
    {
        Dictionary<string, int> updatedTimes = new Dictionary<string, int>();

        foreach (string key in boostTimes.Keys)
        {
            int newTime = boostTimes[key] - 1;
            if (newTime > 0)
            {
                updatedTimes[key] = newTime;
            }
        }
        boostTimes = updatedTimes;
    }

    public void PurchaseBoost(string boostType)
    {
        if (boostsInventory.ContainsKey(boostType))
        {
            boostsInventory[boostType]++;
        }
        else
        {
            boostsInventory.Add(boostType, 1);
        }
        onBoostQuantityChange.Invoke(boostType, boostsInventory[boostType]);
    }

    public void UseBoost(string boostType, int duration)
    {
        if (boostsInventory.ContainsKey(boostType) && boostsInventory[boostType] > 0)
        {
            if (boostTimes.ContainsKey(boostType))
            {
                boostTimes[boostType] += duration;
            }
            else
            {
                boostTimes.Add(boostType, duration);
            }
            boostsInventory[boostType] -= 1;
            onBoostQuantityChange.Invoke(boostType, boostsInventory[boostType]);
        }
    }

    public void UseTimeBoost(string boostType)
    {
        if (boostsInventory.ContainsKey(boostType) && boostsInventory[boostType] > 0)
        {
            boostsInventory[boostType] -= 1;
            onBoostQuantityChange.Invoke(boostType, boostsInventory[boostType]);
        }
    }
}
