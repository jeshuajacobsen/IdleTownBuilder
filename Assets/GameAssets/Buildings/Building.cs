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
    private string category = "Gathering";

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
                transform.Find("ManagerButton").GetComponent<Image>().sprite = SpriteManager.instance.GetManagerSprite(manager.nameText.text);
            } else {
                transform.Find("ManagerButton").GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("ManagerButton");
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

        if (buildingName == "Farm")
        {
            Unlock();
        }
        BuildingData buildingData = GameManager.instance.gameData.GetBuildingData(newName);
        string[] inputResources;
        string outputResource;
        int productionTime;

        outputResource = buildingData.outputResource;
        baseCost = buildingData.baseCost;
        productionTime = buildingData.productionTime;
        category = buildingData.category;
        race = buildingData.race;
        inputResources = buildingData.inputResources;

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
        if (category == "Farm")
        {
            multiplier -= ResearchManager.instance.prestigeResearchLevels.ContainsKey("Fertilizer") ? ResearchManager.instance.prestigeResearchLevels["Fertilizer"] * .1 : 0;
        }

        multiplier -= ResearchManager.instance.scienceResearchLevels.ContainsKey("Architecture") ? ResearchManager.instance.scienceResearchLevels["Architecture"] * .1 : 0;

        if (Manager != null)
        {
            multiplier -= Manager.effect1Type == "LessConsumption" || Manager.effect2Type == "LessConsumption" ? Manager.GetEffectMagnitude("LessConsumption") : 0;
        }
        BigInteger cost = baseCost * GameManager.Pow(level, 2) * (int)(multiplier * 100) / 100;
        return cost > 0 ? cost : 1;
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

        if (category == "Gathering")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Gathering") ? ResearchManager.instance.scienceResearchLevels["Gathering"] * .1f : 0;
        }
        if (category == "Mine")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Mining") ? ResearchManager.instance.scienceResearchLevels["Mining"] * .1f : 0;
        }
        if (category == "Farm")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Farming") ? ResearchManager.instance.scienceResearchLevels["Farming"] * .1f : 0;
        }
        if (category == "Crafting")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Crafting") ? ResearchManager.instance.scienceResearchLevels["Crafting"] * .1f : 0;
        }
        if (category == "Processing")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Processing") ? ResearchManager.instance.scienceResearchLevels["Processing"] * .1f : 0;
        }
        if (category == "Manufacturing")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Manufacturing") ? ResearchManager.instance.scienceResearchLevels["Manufacturing"] * .1f : 0;
        }
        if (category == "Jeweler")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Jeweler") ? ResearchManager.instance.scienceResearchLevels["Jeweler"] * .1f : 0;
        }
        if (category == "Magic")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Magic") ? ResearchManager.instance.scienceResearchLevels["Magic"] * .1f : 0;
        }

        if (ResearchManager.instance.buildingResearchLevels.ContainsKey(buildingName))
        {
            multiplier += (ResearchManager.instance.buildingResearchLevels[buildingName] - 1) * .1f; 
        }
        if (race == "Human")
        {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Human Tech") ? ResearchManager.instance.prestigeResearchLevels["Human Tech"] * .1f : 0;
        } else if (race == "Merfolk") {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Merfolk Tech") ? ResearchManager.instance.prestigeResearchLevels["Merfolk Tech"] * .1f : 0;
        } else if (race == "Dwarf") {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Dwarf Tech") ? ResearchManager.instance.prestigeResearchLevels["Dwarf Tech"] * .1f : 0;
        } else if (race == "Fairy") {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Fairy Tech") ? ResearchManager.instance.prestigeResearchLevels["Fairy Tech"] * .1f : 0;
        } else if (race == "Elf") {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Elf Tech") ? ResearchManager.instance.prestigeResearchLevels["Elf Tech"] * .1f : 0;
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
        BigInteger baseUnlock = baseCost * 4;
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
