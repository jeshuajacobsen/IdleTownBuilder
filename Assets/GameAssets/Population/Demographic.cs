using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;

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
    private BigInteger unlockCost = 1;
    private BigInteger baseCost = 1;
    private int basePrestigeGenerated;
    public string race = "Human";
    public Canvas canvas;


    // Start is called before the first frame update
    void Start()
    {
        //transform.Find("Mask").Find("BuildingImage").GetComponent<Image>().sprite = SpriteManager.instance.GetBuildingSprite(buildingName);
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
            case "Tradesmen":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                break;
            case "Patricians":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                break;
            case "Wizards":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                break;
            case "Nobles":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                break;
            case "Royalty":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                break;

            //merfolk
            case "Surfs":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                break;
            case "Middle Mer":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                break;
            case "Sea Witches":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                break;
            case "Mer-chants":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                break;
            case "High Mer":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                break;
            case "Tritons":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                break;


            //Dwarves
            case "Miners":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Dwarf";
                break;
            case "Workers":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Dwarf";
                break;
            case "Mages":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Dwarf";
                break;
            case "Artificers":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Dwarf";
                break;
            case "Dwarf Lords":
                unlockCost = 400;
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Dwarf";
                break;
        }

        transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().InitValues(newName);
        transform.Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "$" + CalculateCost();
    }

    public void LevelUp()
    {
        BigInteger cost = CalculateCost();
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

    public BigInteger GetPrestigeGenerated()
    {
        double multiplier = 1;
        if (nameText.text == "Peasant")
        {
            multiplier += ResearchManager.instance.multipliers.ContainsKey("Peasentry") ? ResearchManager.instance.multipliers["Peasentry"] : 0;
        }
        return new BigInteger(Level * basePrestigeGenerated * multiplier);
    }

    public void Unlock()
    {
        Level = 1;
        transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().Unlock();
        transform.Find("LockedPanel").gameObject.SetActive(false);
    }

    public BigInteger GetUnlockCost()
    {
        return unlockCost;
    }

    public BigInteger CalculateCost()
    {
        return (baseCost * new BigInteger(Level * 1.6));
    }

    public void Tick()
    {
        transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().Tick();
    }
}
