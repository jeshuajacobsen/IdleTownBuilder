using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
using System.Transactions;
using System;
using UnityEngine.Events;

public class Demographic : MonoBehaviour, Unlockable
{
    [SerializeField] private TextMeshProUGUI nameText;
    public string Name
    {
        get { return nameText.text; }
        set 
        { 
            nameText.text = value;
        }
    }
    private BigInteger baseCost = 1;
    private int basePrestigeGenerated;
    public string race = "Human";
    public Canvas canvas;
    public int tier;

    private double happiness = 1;
    public double Happiness
    {
        get { return happiness; }
        set 
        { 
            happiness = value;
        }
    }
    private int capacityLevel = 1;
    public int CapacityLevel
    {
        get { return capacityLevel; }
        set 
        { 
            capacityLevel = value;
            transform.Find("CapacityPanel").Find("CapacityText").GetComponent<TextMeshProUGUI>().text = GameManager.BigIntToExponentString(CalculateCapacity(false)) + " -> " + GameManager.BigIntToExponentString(CalculateCapacity(true));
            transform.Find("CapacityPanel").Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "$" + GameManager.BigIntToExponentString(CalculateCapacityCost());
        }
    }
    private int growthLevel = 1;

    public int GrowthLevel
    {
        get { return growthLevel; }
        set 
        { 
            growthLevel = value;
            transform.Find("GrowthPanel").Find("GrowthText").GetComponent<TextMeshProUGUI>().text = GameManager.BigIntToExponentString(CalculateGrowth(false)) + " -> " + GameManager.BigIntToExponentString(CalculateGrowth(true));
            transform.Find("GrowthPanel").Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "$" + GameManager.BigIntToExponentString(CalculateGrowthCost());
        }
    }

    private BigInteger population = 0;
    public BigInteger Population
    {
        get { return population; }
        set 
        { 
            //TODO - this is a hack to get the requirements to update.  Need to refactor
            foreach (Requirement requirement in transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().requirements)
            {
                requirement.population = value;
            }
            population = value;
            transform.Find("PopulationPanel").Find("PopulationText").GetComponent<TextMeshProUGUI>().text = "Pop: " + population.ToString();
        }
    }

    private double popGrowthPercentComplete = 0;

    public double PopGrowthPercentComplete
    {
        get { return popGrowthPercentComplete; }
        set 
        { 
            popGrowthPercentComplete = value;
        }
    }

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
        GrowthLevel = 1;
        CapacityLevel = 1;
        Population = 0;
        Name = newName;
        ConsumptionPanel consumptionPanel = transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>();

        switch(newName)
        {
            case "Peasants":
                baseCost = 20;
                basePrestigeGenerated = 4;
                consumptionPanel.AddRequirement("Wheat", 5);
                consumptionPanel.AddRequirement("Pottery", 1);
                tier = 1;
                break;
            case "Commoners":
                baseCost = 2000;
                basePrestigeGenerated = 180;
                consumptionPanel.AddRequirement("Vegetables", 4);
                consumptionPanel.AddRequirement("Clothes", 2);
                consumptionPanel.AddRequirement("Fruit", 1);
                tier = 2;
                break;
            case "Tradesmen":
                baseCost = 20000;
                basePrestigeGenerated = 400;
                consumptionPanel.AddRequirement("Flour", 2);
                consumptionPanel.AddRequirement("Fruit", 1);
                consumptionPanel.AddRequirement("Fish", 5);
                consumptionPanel.AddRequirement("Honey", 2);
                tier = 3;
                break;
            case "Patricians":
                baseCost = 400000;
                basePrestigeGenerated = 2000;
                consumptionPanel.AddRequirement("Bread", 20);
                consumptionPanel.AddRequirement("Furniture", 10);
                consumptionPanel.AddRequirement("Basket", 5);
                consumptionPanel.AddRequirement("Mead", 5);
                tier = 4;
                break;
            case "Wizards":
                baseCost = 8000000;
                basePrestigeGenerated = 20;
                consumptionPanel.AddRequirement("Milk", 20);
                consumptionPanel.AddRequirement("Artifact", 10);
                consumptionPanel.AddRequirement("Fairy Dust", 5);
                consumptionPanel.AddRequirement("Wand", 5);
                tier = 5;
                break;
            case "Nobles":
                baseCost = 200;
                basePrestigeGenerated = 20;
                consumptionPanel.AddRequirement("Beef", 20);
                consumptionPanel.AddRequirement("Light Bulb", 10);
                consumptionPanel.AddRequirement("Cake", 5);
                consumptionPanel.AddRequirement("Tea", 5);
                tier = 6;
                break;
            case "Royalty":
                baseCost = 200;
                basePrestigeGenerated = 20;
                consumptionPanel.AddRequirement("Human Jewelry", 20);
                consumptionPanel.AddRequirement("Elvish Jewelry", 10);
                consumptionPanel.AddRequirement("Fairy Jewelry", 5);
                consumptionPanel.AddRequirement("Mer Jewelry", 5);
                tier = 7;
                break;

            //merfolk
            case "Surfs":
                baseCost = 200;
                basePrestigeGenerated = 200;
                race = "Merfolk";
                consumptionPanel.AddRequirement("Kelp", 20);
                consumptionPanel.AddRequirement("Fish", 5);
                tier = 1;
                break;
            case "Middle Mer":
                baseCost = 400;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                consumptionPanel.AddRequirement("Pottery", 5);
                consumptionPanel.AddRequirement("Pearl", 10);
                consumptionPanel.AddRequirement("Crab", 7);
                tier = 2;
                break;
            case "Sea Witches":
                baseCost = 1000;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                consumptionPanel.AddRequirement("Beauty Charm", 5);
                consumptionPanel.AddRequirement("Rune", 10);
                consumptionPanel.AddRequirement("Eye Of Newt", 7);
                tier = 3;
                break;
            case "Mer-chants":
                baseCost = 4000;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                consumptionPanel.AddRequirement("Mer Jewelry", 5);
                consumptionPanel.AddRequirement("Luck Charm", 5);
                consumptionPanel.AddRequirement("Beer", 10);
                consumptionPanel.AddRequirement("Rice", 7);
                tier = 4;
                break;
            case "High Mer":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                consumptionPanel.AddRequirement("Mer Jewelry", 5);
                consumptionPanel.AddRequirement("Artifact", 5);
                consumptionPanel.AddRequirement("Light Bulb", 10);
                consumptionPanel.AddRequirement("Liqour", 7);
                tier = 5;
                break;
            case "Tritons":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                consumptionPanel.AddRequirement("Mermail", 5);
                consumptionPanel.AddRequirement("Trident", 5);
                consumptionPanel.AddRequirement("Wine", 10);
                consumptionPanel.AddRequirement("Liqour", 7);
                tier = 6;
                break;


            //Dwarves
            case "Miners":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Dwarf";
                consumptionPanel.AddRequirement("Mushroom", 15);
                consumptionPanel.AddRequirement("Mead", 5);
                tier = 1;
                break;
            case "Workers":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Dwarf";
                consumptionPanel.AddRequirement("Tools", 5);
                consumptionPanel.AddRequirement("Milk", 5);
                consumptionPanel.AddRequirement("Beer", 1);
                tier = 2;
                break;
            case "Mages":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Dwarf";
                consumptionPanel.AddRequirement("Rune", 5);
                consumptionPanel.AddRequirement("Magic Mushrooms", 15);
                consumptionPanel.AddRequirement("Wine", 10);
                consumptionPanel.AddRequirement("Enchantment", 7);
                tier = 3;
                break;
            case "Artificers":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Dwarf";
                consumptionPanel.AddRequirement("Book", 5);
                consumptionPanel.AddRequirement("Golem", 15);
                consumptionPanel.AddRequirement("Cake", 10);
                consumptionPanel.AddRequirement("Leather Clothes", 7);
                tier = 4;
                break;
            case "Dwarf Lords":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Dwarf";
                consumptionPanel.AddRequirement("Mithril Armor", 5);
                consumptionPanel.AddRequirement("Invisibility Cloak", 15);
                consumptionPanel.AddRequirement("Liqour", 10);
                tier = 5;
                break;

            //Fairies
            case "Changelings":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Fairy";
                consumptionPanel.AddRequirement("Flowers", 5);
                consumptionPanel.AddRequirement("Honey", 15);
                consumptionPanel.AddRequirement("Bread", 10);
                tier = 1;
                break;
            case "Brownies":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Fairy";
                consumptionPanel.AddRequirement("Flowers", 5);
                consumptionPanel.AddRequirement("Shoes", 15);
                consumptionPanel.AddRequirement("Berries", 10);
                tier = 2;
                break;
            case "Leprechauns":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Fairy";
                consumptionPanel.AddRequirement("Luck Charm", 5);
                consumptionPanel.AddRequirement("Sugar", 15);
                consumptionPanel.AddRequirement("Milk", 10);
                tier = 3;
                break;
            case "Selkies":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Fairy";
                consumptionPanel.AddRequirement("Fish", 5);
                consumptionPanel.AddRequirement("Rice", 15);
                consumptionPanel.AddRequirement("Mana", 10);
                consumptionPanel.AddRequirement("Egg", 10);
                tier = 4;
                break;
            case "Clurichaun":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Fairy";
                consumptionPanel.AddRequirement("Light Bulb", 5);
                consumptionPanel.AddRequirement("Beer", 15);
                consumptionPanel.AddRequirement("Wine", 10);
                consumptionPanel.AddRequirement("Liqour", 10);
                tier = 5;
                break;
            case "Aos Si":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Fairy";
                consumptionPanel.AddRequirement("Fairy Jewelry", 5);
                consumptionPanel.AddRequirement("Coffee", 15);
                consumptionPanel.AddRequirement("Wine", 10);
                consumptionPanel.AddRequirement("Liqour", 10);
                tier = 6;
                break;


            //Elf
            case "Worker Elves":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Elf";
                consumptionPanel.AddRequirement("Anima Fruit", 15);
                consumptionPanel.AddRequirement("Egg", 5);
                tier = 1;
                break;
            case "House Elves":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Elf";
                consumptionPanel.AddRequirement("Cotton Clothes", 15);
                consumptionPanel.AddRequirement("Shoes", 5);
                consumptionPanel.AddRequirement("Fish", 5);
                tier = 2;
                break;
            case "Druids":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Elf";
                consumptionPanel.AddRequirement("Chicken", 15);
                consumptionPanel.AddRequirement("Enchantment", 5);
                consumptionPanel.AddRequirement("Rune", 5);
                consumptionPanel.AddRequirement("Curse", 5);
                tier = 3;
                break;
            case "High Elves":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Elf";
                consumptionPanel.AddRequirement("Beauty Charm", 15);
                consumptionPanel.AddRequirement("Coffee", 5);
                consumptionPanel.AddRequirement("Rice", 5);
                consumptionPanel.AddRequirement("Liqour", 5);
                tier = 4;
                break;
            case "Perfects":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Elf";
                consumptionPanel.AddRequirement("Elvish Jewelry", 15);
                consumptionPanel.AddRequirement("Fairy Jewelry", 5);
                consumptionPanel.AddRequirement("Book", 5);
                consumptionPanel.AddRequirement("High Arcana", 5);
                tier = 5;
                break;
        }
    }

    public void GrowPopulation()
    {
        if (Population < CalculateCapacity(false) || popGrowthPercentComplete <= 100)
        {   
            popGrowthPercentComplete += GrowthLevel * Happiness / 100;
            if (Population < CalculateCapacity(false) && popGrowthPercentComplete >= 100)
            {
                Population += 1;
                popGrowthPercentComplete -= 100;
                TasksManager.instance.CheckTasks("PopulationGoal", Name, Population);
            } else if (popGrowthPercentComplete > 100) {
                popGrowthPercentComplete = 100;
            }
            SetPopGrowthProgress();
        }
    }

    private void SetPopGrowthProgress()
    {
        const int width = 200;
        RectTransform rectTransform = transform.Find("PopulationPanel").Find("BarFill").GetComponent<RectTransform>();
        float desiredWidth = (float)popGrowthPercentComplete / 100 * width;
        rectTransform.sizeDelta = new UnityEngine.Vector2(desiredWidth, rectTransform.sizeDelta.y);

        rectTransform.anchoredPosition = new UnityEngine.Vector2(-width / 2 + desiredWidth / 2, rectTransform.anchoredPosition.y);
    }

    public BigInteger CalculateCapacity(bool nextLevel)
    {
        int capLevel = nextLevel ? CapacityLevel + 1 : CapacityLevel;
        double multiplier = 1;
        if (tier == 1 || tier == 2)
        { 
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Housing") ? ResearchManager.instance.scienceResearchLevels["Housing"] * .1f : 0;
        } 
        else if (tier == 3 || tier == 4)
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Neighborhood") ? ResearchManager.instance.scienceResearchLevels["Neighborhood"] * .1f : 0;
        }
        else if (tier == 5 || tier == 6)
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Estates") ? ResearchManager.instance.scienceResearchLevels["Estates"] * .1f : 0;
        }
        return capLevel * (int)(100 * multiplier) / 100;
    }

    public BigInteger CalculateGrowth(bool nextLevel)
    {
        int growLevel = nextLevel ? GrowthLevel + 1 : GrowthLevel;
        double multiplier = 1;
        if (tier == 1 || tier == 2)
        { 
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Immigration") ? ResearchManager.instance.scienceResearchLevels["Immigration"] * .1f : 0;
        } 
        else if (tier == 3 || tier == 4)
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Child Care") ? ResearchManager.instance.scienceResearchLevels["Child Care"] * .1f : 0;
        }
        else if (tier == 5 || tier == 6)
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Fertility Rites") ? ResearchManager.instance.scienceResearchLevels["Fertility Rites"] * .1f : 0;
        }
        return growLevel * (int)(100 * multiplier) / 100;
    }

    public BigInteger CalculateCapacityCost()
    {
         return baseCost * GameManager.Pow(capacityLevel, 2);
    }

    public BigInteger CalculateGrowthCost()
    {
        return baseCost * GameManager.Pow(growthLevel, 2);
    }

    public void GrowthLevelUp()
    {
        BigInteger cost = CalculateGrowthCost();
        if (GameManager.instance.HasEnoughCoin(cost))
        {
            GrowthLevel = GrowthLevel + 1;
            GameManager.instance.SubtractCoins(cost);
        }
    }

    public void CapacityLevelUp()
    {
        BigInteger cost = CalculateCapacityCost();
        if (GameManager.instance.HasEnoughCoin(cost))
        {
            CapacityLevel = CapacityLevel + 1;
            GameManager.instance.SubtractCoins(cost);
        }
    }

    public BigInteger GetPrestigeGenerated()
    {
        double multiplier = 1;
        int addToBase = 0;
        if (nameText.text == "Peasants")
        {
            addToBase += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Peasanting") ? ResearchManager.instance.prestigeResearchLevels["Peasanting"] * 1 : 0;
        }
        return population * (int)((basePrestigeGenerated + addToBase) * multiplier * 100) / 100;
    }

    public void Unlock()
    {
        Population = 1;
        SetPopGrowthProgress();
        GrowthLevel = 1;
        CapacityLevel = 1;
        transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().Unlock();
        transform.Find("LockedPanel").gameObject.SetActive(false);
    }

    public BigInteger GetUnlockCost()
    {
        return baseCost * 3;
    }

    public void Tick()
    {
        transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().Tick();
    }
}
