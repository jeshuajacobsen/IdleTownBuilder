using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using SharpUI.Source.Common.UI.Elements.Loading;

public class Building : MonoBehaviour, Unlockable
{
    public int level = 1;
    public ProductionInput inputResourceButton1;
    public ProductionInput inputResourceButton2;
    public ProductionInput inputResourceButton3;
    public ProductionOutput outputResourceButton;

    public string buildingName = "";
    public bool locked = true;
    private int unlockCost = 1;
    private int baseCost = 1;

    public TextMeshProUGUI levelText;

    public UnityEvent<string> onProductionClick;

    void Awake()
    {
        onProductionClick = new UnityEvent<string>();
    }

    void Start()
    {
        levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        Button button = transform.Find("UpgradeButton").GetComponent<Button>();
        button.onClick.AddListener(LevelUp);
        transform.Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "$" + CalculateCost();

        inputResourceButton1.onProductionClick.AddListener(ProductionClick);
        inputResourceButton2.onProductionClick.AddListener(ProductionClick);
        inputResourceButton3.onProductionClick.AddListener(ProductionClick);
        outputResourceButton.onProductionClick.AddListener(ProductionClick);

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

        switch(newName)
        {
            case "Farm":
                Unlock();
                transform.Find("LockedPanel").gameObject.SetActive(false);
                outputResource = "Wheat";
                baseCost = 1;
                unlockCost = 1;
                break;
            case "Forester":
                unlockCost = 100;
                outputResource = "Wood";
                baseCost = 30;
                break;
            case "Clay Pit":
                unlockCost = 1000;
                outputResource = "Clay";
                baseCost = 100;
                break;
            case "Lumber Mill":
                unlockCost = 5000;
                outputResource = "Lumber";
                inputResources = new string[] {"Wood"};
                baseCost = 500;
                break;
            case "Potter":
                unlockCost = 10000;
                outputResource = "Pottery";
                inputResources = new string[] {"Clay"};
                baseCost = 2000;
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

        outputResourceButton.InitValues(outputResource);
        TextMeshProUGUI nameText = transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = buildingName;
        if (!GameManager.instance.productionTimers.ContainsKey(outputResource))
        {
            GameManager.instance.productionTimers[outputResource] = 0;
        }
    }

    public void LevelUp()
    {
        int cost = CalculateCost();
        if (GameManager.instance.HasEnoughCoin(cost))
        {
            level++;
            levelText.text = "Level: " + level;
            GameManager.instance.SubtractCoins(cost);
            transform.Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "$" + CalculateCost();
        }
    }

    public int CalculateCost()
    {
        return (int)(baseCost * level * 1.2);
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
        float multiplier = 1;
        if (buildingName == "Farm")
        {
            multiplier = ResearchManager.instance.farmMultiplier;
        }
        return (int)(level * multiplier);
    }

    public void Unlock()
    {
        locked = false;
        Transform display = transform.Find("ProductionDisplay");
        display.Find("OutputProductionButton").GetComponent<ProductionOutput>().Unlock();
        display.Find("InputProductionButton1").GetComponent<ProductionInput>().Unlock();
        display.Find("InputProductionButton2").GetComponent<ProductionInput>().Unlock();
        display.Find("InputProductionButton3").GetComponent<ProductionInput>().Unlock();
    }

    public int GetUnlockCost()
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
