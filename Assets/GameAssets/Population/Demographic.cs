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
    public int tier;


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
                consumptionPanel.AddRequirement("Wheat", 30);
                consumptionPanel.AddRequirement("Pottery", 3);
                tier = 1;
                break;
            case "Commoners":
                baseCost = 2000;
                basePrestigeGenerated = 180;
                consumptionPanel.AddRequirement("Pottery", 8);
                consumptionPanel.AddRequirement("Vegetables", 5);
                consumptionPanel.AddRequirement("Clothes", 3);
                tier = 2;
                break;
            case "Tradesmen":
                baseCost = 20000;
                basePrestigeGenerated = 400;
                consumptionPanel.AddRequirement("Clothes", 20);
                consumptionPanel.AddRequirement("Fruit", 10);
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
                baseCost = 4000;
                basePrestigeGenerated = 200;
                race = "Merfolk";
                consumptionPanel.AddRequirement("Kelp", 20);
                consumptionPanel.AddRequirement("Fish", 5);
                tier = 1;
                break;
            case "Middle Mer":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                consumptionPanel.AddRequirement("Pottery", 5);
                consumptionPanel.AddRequirement("Pearl", 10);
                consumptionPanel.AddRequirement("Crab", 7);
                tier = 2;
                break;
            case "Sea Witches":
                baseCost = 200;
                basePrestigeGenerated = 20;
                race = "Merfolk";
                consumptionPanel.AddRequirement("Beauty Charm", 5);
                consumptionPanel.AddRequirement("Rune", 10);
                consumptionPanel.AddRequirement("Eye Of Newt", 7);
                tier = 3;
                break;
            case "Mer-chants":
                baseCost = 200;
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
