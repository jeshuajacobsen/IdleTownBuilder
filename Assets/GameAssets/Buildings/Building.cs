using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using SharpUI.Source.Common.UI.Elements.Loading;
using System.Numerics;

public class Building : MonoBehaviour, Unlockable
{
    private int level = 0;
    public int Level
    {
        get { return level; }
        set 
        { 
            level = value; 
            levelText.text = "Level: " + level;
            transform.Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = 
                "$" + GameManager.BigIntToExponentString(CalculateCost());
        }
    }
    public ProductionInput inputResourceButton1;
    public ProductionInput inputResourceButton2;
    public ProductionInput inputResourceButton3;
    public ProductionOutput outputResourceButton;

    public string buildingName = "";
    public bool locked = true;
    private BigInteger unlockCost = 1;
    private BigInteger baseCost = 1;

    public string race = "Human";

    public TextMeshProUGUI levelText;

    public UnityEvent<string> onProductionClick;

    public Canvas canvas;

    void Awake()
    {
        onProductionClick = new UnityEvent<string>();
    }

    void Start()
    {
        levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        Button button = transform.Find("UpgradeButton").GetComponent<Button>();
        button.onClick.AddListener(LevelUp);
        transform.Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = 
            "$" + GameManager.BigIntToExponentString(CalculateCost());

        inputResourceButton1.onProductionClick.AddListener(ProductionClick);
        inputResourceButton2.onProductionClick.AddListener(ProductionClick);
        inputResourceButton3.onProductionClick.AddListener(ProductionClick);
        outputResourceButton.onProductionClick.AddListener(ProductionClick);

        transform.Find("Mask").Find("BuildingImage").GetComponent<Image>().sprite = SpriteManager.instance.GetBuildingSprite(buildingName);
    }

    void Update()
    {
        
    }

    public void InitValues(string newName)
    {
        buildingName = newName;
        GameObject arrow1 = transform.Find("ProductionDisplay").Find("Arrow1").gameObject;
        GameObject arrow2 = transform.Find("ProductionDisplay").Find("Arrow2").gameObject;
        GameObject arrow3 = transform.Find("ProductionDisplay").Find("Arrow3").gameObject;

        string[] inputResources = {};
        string outputResource = "";
        int productionTime = 10;

        switch(newName)
        {
            case "Farm":
                Unlock();
                outputResource = "Wheat";
                baseCost = 1;
                unlockCost = 1;
                productionTime = 10;
                break;
            case "Forester":
                unlockCost = 10;
                outputResource = "Wood";
                baseCost = 6;
                productionTime = 12;
                break;
            case "Clay Pit":
                unlockCost = 100;
                outputResource = "Clay";
                baseCost = 10;
                productionTime = 14;
                break;
            case "Lumber Mill":
                unlockCost = 1000;
                outputResource = "Lumber";
                inputResources = new string[] {"Wood"};
                baseCost = 100;
                productionTime = 20;
                break;
            case "Potter":
                unlockCost = 6000;
                outputResource = "Pottery";
                inputResources = new string[] {"Clay"};
                baseCost = 600;
                productionTime = 22;
                break;
            case "Stone Quarry":
                unlockCost = 6000;
                outputResource = "Stone";
                inputResources = new string[] {};
                baseCost = 600;
                productionTime = 22;
                break;
            case "Vegetable Farm":
                unlockCost = 10000;
                outputResource = "Vegetables";
                inputResources = new string[] {};
                baseCost = 1000;
                productionTime = 24;
                break;
            case "Copper Mine":
                unlockCost = 80000;
                outputResource = "Copper Ore";
                inputResources = new string[] {};
                baseCost = 6000;
                productionTime = 28;
                break;
            case "Tin Mine":
                unlockCost = 80000;
                outputResource = "Tin Ore";
                inputResources = new string[] {};
                baseCost = 6000;
                productionTime = 28;
                break;
            case "Smelter":
                unlockCost = 200000;
                outputResource = "Bronze Ingot";
                inputResources = new string[] {"Copper Ore", "Tin Ore", "Wood"};
                baseCost = 10000;
                productionTime = 30;
                break;
            case "Wind Mill":
                unlockCost = 80000;
                outputResource = "Flour";
                inputResources = new string[] {"Wheat"};
                baseCost = 6000;
                productionTime = 26;
                break;
            case "Bakery":
                unlockCost = 200000;
                outputResource = "Bread";
                inputResources = new string[] {"Flour"};
                baseCost = 10000;
                productionTime = 30;
                break;
            case "Orchard":
                unlockCost = 200000;
                outputResource = "Fruit";
                inputResources = new string[] {};
                baseCost = 10000;
                productionTime = 30;
                break;
            case "Vineyard":
                unlockCost = 800000;
                outputResource = "Grapes";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                break;
            case "Furniture Factory":
                unlockCost = 800000;
                outputResource = "Furniture";
                inputResources = new string[] {"Lumber", "Bronze Ingot"};
                baseCost = 50000;
                productionTime = 32;
                break;
            case "Barrel Maker":
                unlockCost = 800000;
                outputResource = "Barrel";
                inputResources = new string[] {"Lumber", "Iron Ingot"};
                baseCost = 50000;
                productionTime = 32;
                break;
            case "Winery":
                unlockCost = 800000;
                outputResource = "Wine";
                inputResources = new string[] {"Grapes", "Barrel"};
                baseCost = 50000;
                productionTime = 32;
                break;

                //merfolk
            case "Kelpery":
                unlockCost = 1000;
                outputResource = "Kelp";
                inputResources = new string[] {};
                baseCost = 300;
                productionTime = 10;
                race = "Merfolk";
                break;
            case "Reef":
                unlockCost = 5000;
                outputResource = "Coral";
                inputResources = new string[] {};
                baseCost = 2000;
                productionTime = 12;
                race = "Merfolk";
                break;
            case "Fishery":
                unlockCost = 20000;
                outputResource = "Fish";
                inputResources = new string[] {};
                baseCost = 15000;
                productionTime = 14;
                race = "Merfolk";
                break;
            case "Oystery":
                unlockCost = 100000;
                outputResource = "Pearl";
                inputResources = new string[] {};
                baseCost = 30000;
                productionTime = 18;
                race = "Merfolk";
                break;
            case "Sand Banks":
                unlockCost = 100000;
                outputResource = "Sand";
                inputResources = new string[] {};
                baseCost = 30000;
                productionTime = 18;
                race = "Merfolk";
                break;
            case "Merite Cave":
                unlockCost = 1000000;
                outputResource = "Merite Ore";
                inputResources = new string[] {};
                baseCost = 300000;
                productionTime = 18;
                race = "Merfolk";
                break;
            case "Crab Pots":
                unlockCost = 800000;
                outputResource = "Crab";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                break;
            case "Thermal Vents":
                unlockCost = 10000000;
                outputResource = "Magma Slug";
                inputResources = new string[] {};
                baseCost = 3000000;
                productionTime = 18;
                race = "Merfolk";
                break;
            case "Slime Milker":
                unlockCost = 100000000;
                outputResource = "Fire Slime";
                inputResources = new string[] {"Magma Slug"};
                baseCost = 30000000;
                productionTime = 18;
                race = "Merfolk";
                break;
            case "Aquaforge":
                unlockCost = 1000000000;
                outputResource = "Merite Ingot";
                inputResources = new string[] {"Fire Slime", "Merite Ore"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Merfolk";
                break;

            //Dwarves
            case "Mushroom Cave":
                unlockCost = 1000000000;
                outputResource = "Mushroom";
                inputResources = new string[] {};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;  
            case "Mana Well":
                unlockCost = 1000000000;
                outputResource = "Mana";
                inputResources = new string[] {};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Coal Mine":
                unlockCost = 1000000000;
                outputResource = "Coal";
                inputResources = new string[] {};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Iron Mine":
                unlockCost = 1000000000;
                outputResource = "Iron Ore";
                inputResources = new string[] {};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Apiary":
                unlockCost = 1000000000;
                outputResource = "Honey";
                inputResources = new string[] {};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Iron Smelter":
                unlockCost = 1000000000;
                outputResource = "Iron Ingot";
                inputResources = new string[] {"Coal", "Iron Ore"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Meadery":
                unlockCost = 1000000000;
                outputResource = "Golem";
                inputResources = new string[] {"Honey"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Gear Works":
                unlockCost = 1000000000;
                outputResource = "Mechanical Parts";
                inputResources = new string[] {"Wood", "Iron Ingot"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Blacksmith":
                unlockCost = 1000000000;
                outputResource = "Tools";
                inputResources = new string[] {"Coal", "Iron Ore", "Wood"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Hops Farm":
                unlockCost = 1000000000;
                outputResource = "Hops";
                inputResources = new string[] {};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Gold Mine":
                unlockCost = 1000000000;
                outputResource = "Gold Ore";
                inputResources = new string[] {"Tools"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Glass Blower":
                unlockCost = 1000000000;
                outputResource = "Glass";
                inputResources = new string[] {"Sand"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;                
            case "Brewery":
                unlockCost = 1000000000;
                outputResource = "Beer";
                inputResources = new string[] {"Wheat", "Hops", "Bottles"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Gold Smelter":
                unlockCost = 1000000000;
                outputResource = "Gold Ingot";
                inputResources = new string[] {"Coal", "Iron Ore"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Artificer":
                unlockCost = 1000000000;
                outputResource = "Artifact";
                inputResources = new string[] {"Mechanical Parts", "Mana", "Gold"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Manufactory":
                unlockCost = 1000000000;
                outputResource = "Golem";
                inputResources = new string[] {"Mechanical Parts", "Mana", "Clay"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;  
            case "Rune Workshop":
                unlockCost = 800000;
                outputResource = "Rune";
                inputResources = new string[] {"Mana", "Stone"};
                baseCost = 50000;
                productionTime = 32;
                race = "Dwarf";
                break;  
            case "Deep Mine":
                unlockCost = 1000000000;
                outputResource = "Mithril Ore";
                inputResources = new string[] {"Tools", "Golem"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Volcanic Forge":
                unlockCost = 1000000000;
                outputResource = "Mithril Ingots";
                inputResources = new string[] {};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Mithril Smith":
                unlockCost = 1000000000;
                outputResource = "Mithril Armor";
                inputResources = new string[] {"Mithril Ingots"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;  

            //Fairies
            case "Garden":
                unlockCost = 800000;
                outputResource = "Flowers";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                break;
            case "Fairy Circle":
                unlockCost = 800000;
                outputResource = "Magic Mushrooms";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                break;
            case "Dustery":
                unlockCost = 800000;
                outputResource = "Fairy Dust";
                inputResources = new string[] {"Magic Mushrooms"};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                break;       
            case "Mana Siphon":
                unlockCost = 800000;
                outputResource = "Mana";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                break;
            case "Rainbow":
                unlockCost = 800000;
                outputResource = "Luck Charm";
                inputResources = new string[] {"Mana", "Iron Ingot"};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                break;
            case "Illusionist":
                unlockCost = 800000;
                outputResource = "Beauty Charm";
                inputResources = new string[] {"Mana", "Living Wood", "Fairy Dust"};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                break;
            case "Luminous Garden":
                unlockCost = 800000;
                outputResource = "Light Bulb";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                break;

            
            //elf
            case "Anima Tree":
                unlockCost = 800000;
                outputResource = "Anima Fruit";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                break;
            case "Treant Grove":
                unlockCost = 800000;
                outputResource = "Living Wood";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                break;
            case "Unicorn Stable":
                unlockCost = 800000;
                outputResource = "Unicorn Hair";
                inputResources = new string[] {"Wheat"};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                break;
            case "Druid Circle":
                unlockCost = 800000;
                outputResource = "Enchantment";
                inputResources = new string[] {"Mana", "Living Wood"};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                break;
        }

        if (inputResources.Length == 0)
        {
            inputResourceButton1.gameObject.SetActive(false);
            inputResourceButton2.gameObject.SetActive(false);
            inputResourceButton3.gameObject.SetActive(false);

            arrow1.SetActive(false);
            arrow2.SetActive(false);
            arrow3.SetActive(false);
        } 
        else if (inputResources.Length == 1)
        {
            inputResourceButton1.gameObject.SetActive(false);
            inputResourceButton2.InitValues(inputResources[0]);
            inputResourceButton3.gameObject.SetActive(false);

            arrow1.SetActive(false);
            arrow2.SetActive(true);
            arrow3.SetActive(false);
        }
        else if (inputResources.Length == 2)
        {
            inputResourceButton1.InitValues(inputResources[0]);
            inputResourceButton2.InitValues(inputResources[1]);
            inputResourceButton3.gameObject.SetActive(false);

            arrow1.SetActive(true);
            arrow2.SetActive(true);
            arrow3.SetActive(false);
        } 
        else
        {
            inputResourceButton1.InitValues(inputResources[0]);
            inputResourceButton2.InitValues(inputResources[1]);
            inputResourceButton3.InitValues(inputResources[2]);

            arrow1.SetActive(true);
            arrow2.SetActive(true);
            arrow3.SetActive(true);
        }

        outputResourceButton.InitValues(outputResource, productionTime);
        TextMeshProUGUI nameText = transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = buildingName;
        if (!GameManager.instance.productionTimers.ContainsKey(outputResource))
        {
            GameManager.instance.productionTimers[outputResource] = 0;
        }
    }

    public void LevelUp()
    {
        BigInteger cost = CalculateCost();
        if (GameManager.instance.HasEnoughCoin(cost))
        {
            Level = Level + 1;
            
            GameManager.instance.SubtractCoins(cost);
        }
    }

    public BigInteger CalculateCost()
    {
        double multiplier = 1;
        if (buildingName == "Farm")
        {
            multiplier -= ResearchManager.instance.prestigeResearchLevels.ContainsKey("Fertilizer") ? ResearchManager.instance.prestigeResearchLevels["Fertilizer"] * .1f : 0;
        }
        return new BigInteger((int)(baseCost * level * (int)(1.6 * multiplier * 100) / 100));
    }

    public void ProductionClick(string resource)
    {
        onProductionClick.Invoke(resource);
    }

    public ProductionOutput GetOutputResource()
    {
        return outputResourceButton;
    }

    public ProductionInput GetInputResource(int index)
    {
        if (index == 1)
        {
            return inputResourceButton1;
        } else if (index == 2) {
            return inputResourceButton2;
        } else {
            return inputResourceButton3;
        }
    }

    public void HandleProductionClick()
    {
        outputResourceButton.HandleProductionClick();
    }

    public int GetProductionQuantity()
    {
        double multiplier = 1;

        if (buildingName == "Forester")
        {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Forestry") ? ResearchManager.instance.prestigeResearchLevels["Forestry"] * .1f : 0;
        }
        if (ResearchManager.instance.buildingResearchLevels.ContainsKey(buildingName))
        {
            multiplier += ResearchManager.instance.buildingResearchLevels[buildingName] * .1f; 
        }
        return (int)(level * multiplier);
    }

    public void Unlock()
    {
        Level = 1;
        locked = false;
        transform.Find("LockedPanel").gameObject.SetActive(false);
        Transform display = transform.Find("ProductionDisplay");
        display.Find("OutputProductionButton").GetComponent<ProductionOutput>().Unlock();
        display.Find("InputProductionButton1").GetComponent<ProductionInput>().Unlock();
        display.Find("InputProductionButton2").GetComponent<ProductionInput>().Unlock();
        display.Find("InputProductionButton3").GetComponent<ProductionInput>().Unlock();
    }

    public BigInteger GetUnlockCost()
    {
        return unlockCost;
    }

    public void Tick()
    {
        outputResourceButton.Tick();
        inputResourceButton1.Tick();
        inputResourceButton2.Tick();
        inputResourceButton3.Tick();
    }
}
