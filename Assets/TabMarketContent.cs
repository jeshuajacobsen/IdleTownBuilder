using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SharpUI.Source.Common.UI.Elements.Sliders;
using TMPro;

public class TabMarketContent : MonoBehaviour
{
    private SimpleSlider quantitySlider;

    void Start()
    {
        Button button = transform.Find("SellButton").GetComponent<Button>();
        quantitySlider = transform.Find("SellQuantitySlider").GetComponent<SimpleSlider>();

        button.onClick.AddListener(SellResources);
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
            int amountSold = (int)(quantitySlider.slider.value * GameManager.instance.resources[selectedResource.resourceName]);

            GameManager.instance.SubtractResources(selectedResource.resourceName, amountSold);

//this should probably be something like change text but init values works for now.
            selectedResource.initValues(selectedResource.resourceName, GameManager.instance.resources[selectedResource.resourceName], 1);

            GameManager.instance.AddCoins(amountSold);
            UpdateSellText(quantitySlider.slider.value);
        }
    }

    public void UpdateSellText(float percentToSell)
    {
        GameObject marketContent = GameObject.FindWithTag("MarketContent");
        ResourceListItem selectedResource = marketContent.transform.GetComponent<MarketContent>().selectedResource;
        transform.Find("SellQuantitySlider").Find("SellAmount").GetComponent<TextMeshProUGUI>().text = 
            "$" + (int)(quantitySlider.slider.value * GameManager.instance.resources[selectedResource.resourceName]);
    }
}
