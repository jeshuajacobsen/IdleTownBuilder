using System;
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
    private BigInteger baseCost = 1;

    public string race = "Human";

    public TextMeshProUGUI levelText;

    public UnityEvent<string> onProductionClick;

    public Canvas canvas;

    private Manager manager;
    public Manager Manager
    {
        get { return manager; }
        set 
        { 
            this.manager = value;
            if (this.manager != null)
            {
                transform.Find("ManagerButton").Find("Text").GetComponent<TextMeshProUGUI>().text = manager.nameText.text;
            } else {
                transform.Find("ManagerButton").Find("Text").GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

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


        transform.Find("ManagerButton").GetComponent<Button>().onClick.AddListener(OpenManagerPanel);

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
                productionTime = 10;
                break;
            case "Forester":
                outputResource = "Wood";
                baseCost = 6;
                productionTime = 12;
                break;
            case "Clay Pit":
                outputResource = "Clay";
                baseCost = 10;
                productionTime = 14;
                break;
            case "Lumber Mill":
                outputResource = "Lumber";
                inputResources = new string[] {"Wood"};
                baseCost = 100;
                productionTime = 16;
                break;
            case "Potter":
                outputResource = "Pottery";
                inputResources = new string[] {"Clay"};
                baseCost = 600;
                productionTime = 18;
                break;
            case "Stone Quarry":
                outputResource = "Stone";
                inputResources = new string[] {};
                baseCost = 600;
                productionTime = 22;
                break;
            case "Vegetable Farm":
                outputResource = "Vegetables";
                inputResources = new string[] {};
                baseCost = 1000;
                productionTime = 24;
                break;
            case "Hemp Farm":
                outputResource = "Hemp";
                inputResources = new string[] {};
                baseCost = 1000;
                productionTime = 24;
                break;
            case "Weaver":
                outputResource = "Clothes";
                inputResources = new string[] {"Hemp"};
                baseCost = 4000;
                productionTime = 24;
                break;
            case "Copper Mine":
                outputResource = "Copper Ore";
                inputResources = new string[] {};
                baseCost = 6000;
                productionTime = 28;
                break;
            case "Orchard":
                outputResource = "Fruit";
                inputResources = new string[] {};
                baseCost = 10000;
                productionTime = 30;
                break;
            case "Tin Mine":
                outputResource = "Tin Ore";
                inputResources = new string[] {};
                baseCost = 20000;
                productionTime = 28;
                break;
            case "Smelter":
                outputResource = "Bronze Ingot";
                inputResources = new string[] {"Copper Ore", "Tin Ore", "Wood"};
                baseCost = 60000;
                productionTime = 30;
                break;
            case "Wind Mill":
                outputResource = "Flour";
                inputResources = new string[] {"Wheat"};
                baseCost = 400000;
                productionTime = 26;
                break;
            case "Bakery":
                outputResource = "Bread";
                inputResources = new string[] {"Flour"};
                baseCost = 1000000;
                productionTime = 30;
                break;
            case "Vineyard":
                outputResource = "Grapes";
                inputResources = new string[] {};
                baseCost = new BigInteger(Math.Pow(10, 6));
                productionTime = 32;
                break;
            case "Furniture Factory":
                outputResource = "Furniture";
                inputResources = new string[] {"Lumber", "Bronze Ingot"};
                baseCost = new BigInteger(Math.Pow(10, 7));
                productionTime = 32;
                break;
            case "Barrel Maker":
                outputResource = "Barrel";
                inputResources = new string[] {"Lumber", "Iron Ingot"};
                baseCost = new BigInteger(Math.Pow(10, 8));
                productionTime = 32;
                break;
            case "Winery":
                outputResource = "Wine";
                inputResources = new string[] {"Grapes", "Barrel"};
                baseCost = new BigInteger(Math.Pow(10, 11));
                productionTime = 32;
                break;

                //merfolk
            case "Kelpery":
                outputResource = "Kelp";
                inputResources = new string[] {};
                baseCost = 200;
                productionTime = 10;
                race = "Merfolk";
                break;
            case "Reef":
                outputResource = "Coral";
                inputResources = new string[] {};
                baseCost = 2000;
                productionTime = 12;
                race = "Merfolk";
                break;
            case "Fishery":
                outputResource = "Fish";
                inputResources = new string[] {};
                baseCost = 15000;
                productionTime = 14;
                race = "Merfolk";
                break;
            case "Oystery":
                outputResource = "Pearl";
                inputResources = new string[] {};
                baseCost = 60000;
                productionTime = 18;
                race = "Merfolk";
                break;
            case "Sand Banks":
                outputResource = "Sand";
                inputResources = new string[] {};
                baseCost = 6 * new BigInteger(Math.Pow(10, 5));
                productionTime = 18;
                race = "Merfolk";
                break;
            case "Merite Cave":
                outputResource = "Merite Ore";
                inputResources = new string[] {};
                baseCost = new BigInteger(Math.Pow(10, 9));
                productionTime = 18;
                race = "Merfolk";
                break;
            case "Crab Pots":
                outputResource = "Crab";
                inputResources = new string[] {};
                baseCost = new BigInteger(Math.Pow(10, 12));
                productionTime = 32;
                break;
            case "Thermal Vents":
                outputResource = "Magma Slug";
                inputResources = new string[] {};
                baseCost = 3000000;
                productionTime = 34;
                race = "Merfolk";
                break;
            case "Slime Milker":
                outputResource = "Fire Slime";
                inputResources = new string[] {"Magma Slug"};
                baseCost = 30000000;
                productionTime = 36;
                race = "Merfolk";
                break;
            case "Aquaforge":
                outputResource = "Merite Ingot";
                inputResources = new string[] {"Fire Slime", "Merite Ore"};
                baseCost = 3000000000;
                productionTime = 38;
                race = "Merfolk";
                break;

            //Dwarves
            case "Mushroom Cave":
                outputResource = "Mushroom";
                inputResources = new string[] {};
                baseCost = 200;
                productionTime = 10;
                race = "Dwarf";
                break;  
            case "Mana Well":
                outputResource = "Mana";
                inputResources = new string[] {};
                baseCost = 2000;
                productionTime = 12;
                race = "Dwarf";
                break;
            case "Coal Mine":
                outputResource = "Coal";
                inputResources = new string[] {};
                baseCost = 20000;
                productionTime = 14;
                race = "Dwarf";
                break;
            case "Iron Mine":
                outputResource = "Iron Ore";
                inputResources = new string[] {};
                baseCost = new BigInteger(Math.Pow(10, 6));
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Apiary":
                outputResource = "Honey";
                inputResources = new string[] {};
                baseCost = new BigInteger(Math.Pow(10, 7));
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Iron Smelter":
                outputResource = "Iron Ingot";
                inputResources = new string[] {"Coal", "Iron Ore"};
                baseCost = new BigInteger(Math.Pow(10, 10));
                productionTime = 18;
                race = "Dwarf"; 
                break;
            case "Meadery":
                outputResource = "Mead";
                inputResources = new string[] {"Honey"};
                baseCost = new BigInteger(Math.Pow(10, 13));
                productionTime = 20;
                race = "Dwarf";
                break;
            case "Gear Works":
                outputResource = "Mechanical Parts";
                inputResources = new string[] {"Wood", "Iron Ingot"};
                baseCost = 3000000000;
                productionTime = 24;
                race = "Dwarf";
                break;
            case "Blacksmith":
                outputResource = "Tools";
                inputResources = new string[] {"Coal", "Iron Ore", "Wood"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Hops Farm":
                outputResource = "Hops";
                inputResources = new string[] {};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Gold Mine":
                outputResource = "Gold Ore";
                inputResources = new string[] {"Tools"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Glass Blower":
                outputResource = "Glass";
                inputResources = new string[] {"Sand"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;                
            case "Brewery":
                outputResource = "Beer";
                inputResources = new string[] {"Wheat", "Hops", "Bottles"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Gold Smelter":
                outputResource = "Gold Ingot";
                inputResources = new string[] {"Coal", "Iron Ore"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Artificer":
                outputResource = "Artifact";
                inputResources = new string[] {"Mechanical Parts", "Mana", "Gold"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Manufactory":
                outputResource = "Golem";
                inputResources = new string[] {"Mechanical Parts", "Mana", "Clay"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;  
            case "Rune Workshop":
                outputResource = "Rune";
                inputResources = new string[] {"Mana", "Stone"};
                baseCost = 50000;
                productionTime = 32;
                race = "Dwarf";
                break;  
            case "Deep Mine":
                outputResource = "Mithril Ore";
                inputResources = new string[] {"Tools", "Golem"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Volcanic Forge":
                outputResource = "Mithril Ingots";
                inputResources = new string[] {};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;
            case "Mithril Smith":
                outputResource = "Mithril Armor";
                inputResources = new string[] {"Mithril Ingots"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                break;  

            //Fairies
            case "Garden":
                outputResource = "Flowers";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                break;
            case "Fairy Circle":
                outputResource = "Magic Mushrooms";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                break;
            case "Dustery":
                outputResource = "Fairy Dust";
                inputResources = new string[] {"Magic Mushrooms"};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                break;       
            case "Mana Siphon":
                outputResource = "Mana";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                break;
            case "Rainbow":
                outputResource = "Luck Charm";
                inputResources = new string[] {"Mana", "Iron Ingot"};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                break;
            case "Illusionist":
                outputResource = "Beauty Charm";
                inputResources = new string[] {"Mana", "Living Wood", "Fairy Dust"};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                break;
            case "Luminous Garden":
                outputResource = "Light Bulb";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                break;

            
            //elf
            case "Anima Tree":
                outputResource = "Anima Fruit";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                break;
            case "Treant Grove":
                outputResource = "Living Wood";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                break;
            case "Unicorn Stable":
                outputResource = "Unicorn Hair";
                inputResources = new string[] {"Wheat"};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                break;
            case "Druid Circle":
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

    public void OpenManagerPanel()
    {
        GameManager.instance.managersPanel.Open(this);
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
        if (buildingName == "Farm" || buildingName == "Vegetable Farm" || buildingName == "Orchard" || buildingName == "Vineyard")
        {
            multiplier -= ResearchManager.instance.prestigeResearchLevels.ContainsKey("Fertilizer") ? ResearchManager.instance.prestigeResearchLevels["Fertilizer"] * .1f : 0;
        }
        if (Manager != null)
        {
            multiplier -= Manager.effect1Type == "LessConsumption" || Manager.effect2Type == "LessConsumption" ? Manager.GetEffectMagnitude("LessConsumption") : 0;
        }
        return baseCost * GameManager.Pow(level + 1, 2) * (int)(multiplier * 100) / 100;
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
        if (race == "Human")
        {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Human Tech") ? ResearchManager.instance.prestigeResearchLevels["Human Tech"] * .1f : 0;
        } else if (race == "Merfolk") {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Merfolk Tech") ? ResearchManager.instance.prestigeResearchLevels["Human Tech"] * .1f : 0;
        } else if (race == "Dwarf") {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Dwarf Tech") ? ResearchManager.instance.prestigeResearchLevels["Human Tech"] * .1f : 0;
        } else if (race == "Fairy") {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Fairy Tech") ? ResearchManager.instance.prestigeResearchLevels["Human Tech"] * .1f : 0;
        } else if (race == "Elf") {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Elf Tech") ? ResearchManager.instance.prestigeResearchLevels["Human Tech"] * .1f : 0;
        }
        multiplier += Manager != null && (Manager.effect1Type == "ProductionQuantity" || Manager.effect2Type == "ProductionQuantity") ? Manager.GetEffectMagnitude("ProductionQuantity") : 0;
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
        BigInteger baseUnlock = baseCost * 3;
        if (ResearchManager.instance.scienceResearchLevels.ContainsKey("Architecture"))
        {
            baseUnlock -= baseUnlock * ResearchManager.instance.scienceResearchLevels["Architecture"] / 10;
        }
        return baseUnlock;
    }

    public void Tick()
    {
        if (transform.Find("ProductionDisplay").Find("PauseToggle").GetComponent<Toggle>().isOn == false)
        {
            outputResourceButton.Tick(false);
        }
    }
}
