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

    void Start()
    {
        Button button = transform.Find("SellButton").GetComponent<Button>();
        quantitySlider = transform.Find("SellQuantitySlider").GetComponent<SimpleSlider>();

        button.onClick.AddListener(SellResources);

        MarketContent marketContent = GameObject.FindWithTag("MarketContent").GetComponent<MarketContent>();
        marketContent.onSelectedResourceChange.AddListener(UpdateSellText);
    }

    void Update()
    {
        
    }

    private void SellResources() 
    {
        GameObject marketContent = GameObject.FindWithTag("MarketContent");
        ResourceListItem selectedResource = marketContent.transform.GetComponent<MarketContent>().selectedResource;
        if (selectedResource != null)
        {
            int amountSold = calculateSellAmount(selectedResource.resourceName);

            GameManager.instance.SubtractResources(selectedResource.resourceName, amountSold);

            selectedResource.InitValues(selectedResource.resourceName, GameManager.instance.resources[selectedResource.resourceName],
                GameManager.instance.resourcePrices[selectedResource.resourceName]);

            GameManager.instance.AddCoins(amountSold);
            UpdateSellText();
        }
    }

    public void UpdateSellText()
    {
        GameObject marketContent = GameObject.FindWithTag("MarketContent");
        ResourceListItem selectedResource = marketContent.transform.GetComponent<MarketContent>().selectedResource;
        transform.Find("SellQuantitySlider").Find("SellAmount").GetComponent<TextMeshProUGUI>().text = 
            "$" + calculateSellAmount(selectedResource.resourceName);
    }

    private int calculateSellAmount(string resourceName)
    {
        return (int)(quantitySlider.slider.value * 
                GameManager.instance.resources[resourceName] * 
                GameManager.instance.resourcePrices[resourceName]);
    }
}
