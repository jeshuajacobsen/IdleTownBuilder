using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using SharpUI.Source.Common.UI.Elements.Loading;

public class Building : MonoBehaviour
{
    public int level = 1;
    public ProductionInput inputResourceButton1;
    public ProductionInput inputResourceButton2;
    public ProductionInput inputResourceButton3;
    public ProductionOutput outputResourceButton;

    public string buildingName = "";
    public int costForUpgrade = 1;

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
        transform.Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "$" + costForUpgrade;

        inputResourceButton1.onProductionClick.AddListener(ProductionClick);
        inputResourceButton2.onProductionClick.AddListener(ProductionClick);
        inputResourceButton3.onProductionClick.AddListener(ProductionClick);
        outputResourceButton.onProductionClick.AddListener(ProductionClick);
    }

    void Update()
    {
        
    }

    public void InitValues(string newName, string outputResource, string[] inputResources)
    {
        buildingName = newName;
        GameObject arrow1 = transform.Find("ProductionDisplay").Find("Arrow1").gameObject;
        GameObject arrow2 = transform.Find("ProductionDisplay").Find("Arrow2").gameObject;
        GameObject arrow3 = transform.Find("ProductionDisplay").Find("Arrow3").gameObject;

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


        for (int i = 0; i < inputResources.Length; i++)
        {
            inputResourceButton1.InitValues(inputResources[i]);
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
        if (GameManager.instance.HasEnoughCoin(costForUpgrade))
        {
            level++;
            levelText.text = "Level: " + level;
            GameManager.instance.SubtractCoins(costForUpgrade);
            costForUpgrade++;
            transform.Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "$" + costForUpgrade;
        }
    }

    public void ProductionClick(string resource)
    {
        onProductionClick.Invoke(resource);
    }

    public string getOutputResource()
    {
        return outputResourceButton.resource;
    }

    public void HandleProductionClick()
    {
        outputResourceButton.HandleProductionClick();
    }

    public int GetProductionQuantity()
    {
        return level;
    }
}
