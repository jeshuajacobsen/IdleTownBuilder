using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Demographic : MonoBehaviour, Unlockable
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI nameText;
    public string Name
    {
        get { return nameText.text; }
        set 
        { 
            nameText.text = value;
        }
    }
    private int level;
    public int Level
    {
        get { return level; }
        set 
        { 
            level = value;
            levelText.text = "Level: " + level;
            transform.Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "$" + CalculateCost();
        }
    }
    private int unlockCost = 1;
    private int baseCost = 1;
    private int basePrestigeGenerated;
    public string race = "Human";
    public Canvas canvas;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitValues(string newName)
    {
        Level = 0;
        Name = newName;

        switch(newName)
        {
            case "Peasants":
                unlockCost = 40;
                baseCost = 20;
                basePrestigeGenerated = 3;
                break;
            case "Commoners":
                unlockCost = 4000;
                baseCost = 2000;
                basePrestigeGenerated = 180;
                break;

            //merfolk
            case "Surfs":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                break;
        }

        transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().InitValues(newName);
        transform.Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "$" + CalculateCost();
    }

    public void LevelUp()
    {
        int cost = CalculateCost();
        if (GameManager.instance.HasEnoughCoin(cost))
        {
            Level = Level + 1;
            GameManager.instance.SubtractCoins(cost);
            foreach (Requirement requirement in transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().requirements)
            {
                requirement.level = Level;
            }
        }
    }

    public float GetPrestigeGenerated()
    {
        float multiplier = 1;
        if (nameText.text == "Peasant")
        {
            multiplier += ResearchManager.instance.multipliers.ContainsKey("Peasentry") ? ResearchManager.instance.multipliers["Peasentry"] : 0;
        }
        return (Level * basePrestigeGenerated * multiplier);
    }

    public void Unlock()
    {
        transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().Unlock();
        transform.Find("LockedPanel").gameObject.SetActive(false);
    }

    public int GetUnlockCost()
    {
        return unlockCost;
    }

    public int CalculateCost()
    {
        return (int)(baseCost * Level * 1.6);
    }

    public void Tick()
    {
        transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().Tick();
    }
}
