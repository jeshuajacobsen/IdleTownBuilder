using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SharpUI.Source.Common.UI.Elements.Sliders;
using TMPro;
using UnityEngine.Events;

public class TabMarketContent : MonoBehaviour
{
    private SimpleSlider quantitySlider;
    MarketContent marketContent;

    void Start()
    {
        Button button = transform.Find("SellButton").GetComponent<Button>();
        quantitySlider = transform.Find("SellQuantitySlider").GetComponent<SimpleSlider>();
        quantitySlider.slider.value = 100;

        button.onClick.AddListener(SellSelectedResources);

        marketContent = GameObject.FindWithTag("MarketContent").GetComponent<MarketContent>();
        marketContent.onSelectedResourceChange.AddListener(UpdateSellText);

        InvokeRepeating("Tick", 1.0f, 1.0f);
    }

    void Update()
    {
        
    }

    private void SellSelectedResources()
    {
        ResourceListItem selectedResource = marketContent.selectedResource;
        SellResources(selectedResource);
    }

    private void SellResources(ResourceListItem resource) 
    {
        
        if (resource != null && GameManager.instance.resources.ContainsKey(resource.resourceName))
        {
            int amountSold = calculateSellAmount(resource.resourceName);

            GameManager.instance.SubtractResources(resource.resourceName, amountSold);

            resource.InitValues(resource.resourceName, GameManager.instance.resources[resource.resourceName],
                GameManager.instance.resourcePrices[resource.resourceName]);

            GameManager.instance.AddCoins(amountSold);
            UpdateSellText(resource.resourceName);
        }
    }

    public void UpdateSellText(string selectedResource)
    {
        transform.Find("SellQuantitySlider").Find("SellAmount").GetComponent<TextMeshProUGUI>().text = 
            "$" + calculateSellAmount(selectedResource);
    }

    private int calculateSellAmount(string resourceName)
    {
        float multiplier = 1;
        multiplier += ResearchManager.instance.incomeMultiplier;
        return (int)(quantitySlider.slider.value * 
                GameManager.instance.resources[resourceName] * 
                GameManager.instance.resourcePrices[resourceName] *
                multiplier);
    }

    private void Tick()
    {
        foreach (ResourceListItem item in marketContent.resources)
        {
            if(item.transform.Find("Toggle").GetComponent<Toggle>().isOn)
            {
                SellResources(item);
            }
        }
        
    }
}
