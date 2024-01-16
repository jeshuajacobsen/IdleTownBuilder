using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private BigInteger baseCost = 1;
    private int basePrestigeGenerated;
    public string race = "Human";
    public Canvas canvas;


    // Start is called before the first frame update
    void Start()
    {
        transform.Find("DemoImage").GetComponent<Image>().sprite = SpriteManager.instance.GetDemoSprite(nameText.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitValues(string newName)
    {
        Level = 0;
        Name = newName;
        ConsumptionPanel consumptionPanel = transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>();

        switch(newName)
        {
            case "Peasants":
                baseCost = 20;
                basePrestigeGenerated = 4;
                consumptionPanel.AddRequirement("Wheat", 20);
                consumptionPanel.AddRequirement("Pottery", 5);
                break;
            case "Commoners":
                baseCost = 2000;
                basePrestigeGenerated = 180;
                consumptionPanel.AddRequirement("Pottery", 10);
                consumptionPanel.AddRequirement("Vegetables", 7);
                consumptionPanel.AddRequirement("Clothes", 5);
                break;
            case "Tradesmen":
                baseCost = 20000;
                basePrestigeGenerated = 400;
                consumptionPanel.AddRequirement("Clothes", 20);
                consumptionPanel.AddRequirement("Fruit", 10);
                consumptionPanel.AddRequirement("Fish", 5);
                consumptionPanel.AddRequirement("Honey", 2);
                break;
            case "Patricians":
                baseCost = 400000;
                basePrestigeGenerated = 2000;
                consumptionPanel.AddRequirement("Bread", 20);
                consumptionPanel.AddRequirement("Furniture", 10);
                consumptionPanel.AddRequirement("Luck Charm", 5);
                consumptionPanel.AddRequirement("Enchantment", 2);
                break;
            case "Wizards":
                baseCost = 200;
                basePrestigeGenerated = 20;
                break;
            case "Nobles":
                baseCost = 200;
                basePrestigeGenerated = 20;
                break;
            case "Royalty":
                baseCost = 200;
                basePrestigeGenerated = 20;
                break;

            //merfolk
            case "Surfs":
                baseCost = 4000;
                basePrestigeGenerated = 200;
                race = "Merfolk";
                consumptionPanel.AddRequirement("Kelp", 20);
                consumptionPanel.AddRequirement("Fish", 5);
                break;
            case "Middle Mer":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                consumptionPanel.AddRequirement("Pottery", 20);
                consumptionPanel.AddRequirement("Pearl", 10);
                consumptionPanel.AddRequirement("Crab", 7);
                break;
            case "Sea Witches":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                break;
            case "Mer-chants":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                break;
            case "High Mer":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                break;
            case "Tritons":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                break;


            //Dwarves
            case "Miners":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Dwarf";
                break;
            case "Workers":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Dwarf";
                break;
            case "Mages":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Dwarf";
                break;
            case "Artificers":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Dwarf";
                break;
            case "Dwarf Lords":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Dwarf";
                break;

            //Fairies
            case "Changelings":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Fairy";
                break;
            case "Brownies":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Fairy";
                break;
            case "Leprechauns":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Fairy";
                break;
            case "Selkies":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Fairy";
                break;
            case "Clurichaun":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Fairy";
                break;
            case "Aos Si":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Fairy";
                break;


            //Elf
            case "Worker Elves":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Elf";
                break;
            case "House Elves":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Elf";
                break;
            case "Druids":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Elf";
                break;
            case "High Elves":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Elf";
                break;
            case "Perfects":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Elf";
                break;
        }
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
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Peasanting") ? ResearchManager.instance.prestigeResearchLevels["Peasanting"] * .1f : 0;
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
        return baseCost * 3;
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
