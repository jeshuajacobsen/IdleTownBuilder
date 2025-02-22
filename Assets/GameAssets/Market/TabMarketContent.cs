using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SharpUI.Source.Common.UI.Elements.Sliders;
using TMPro;
using UnityEngine.Events;
using System.Numerics;

public class TabMarketContent : MonoBehaviour
{
    private SimpleSlider quantitySlider;
    [SerializeField] private MarketContent marketContent;

    void Awake()
    {
        Button button = transform.Find("SellButton").GetComponent<Button>();
        button.onClick.AddListener(SellSelectedResources);
        quantitySlider = transform.Find("SellQuantitySlider").GetComponent<SimpleSlider>();
        quantitySlider.slider.value = 100;
        InvokeRepeating("Tick", 1.0f, 1.0f);
    }
    void Start()
    {
        marketContent.onSelectedResourceChange.AddListener(UpdateSellText);
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
            BigInteger amountSold = calculateSellAmount(resource.resourceName);

            GameManager.instance.SubtractResources(resource.resourceName, amountSold);

            resource.InitValues(resource.resourceName, GameManager.instance.resources[resource.resourceName],
                GameManager.instance.GetResourcePrice(resource.resourceName));

            GameManager.instance.AddCoins(amountSold);
            UpdateSellText(resource.resourceName);
        }
    }

    public void SellResources(string resource)
    {
        ResourceListItem selectedResource = marketContent.resources.Find(x => {
            return x.resourceName == resource;});
        SellResources(selectedResource);
    }

    public void UpdateSellText(string selectedResource)
    {
        transform.Find("SellQuantitySlider").Find("SellAmount").GetComponent<TextMeshProUGUI>().text = 
            "$" + calculateSellAmount(selectedResource);
    }

    private BigInteger calculateSellAmount(string resourceName)
    {
        float multiplier = 100;

        return (BigInteger)(
            GameManager.instance.resources[resourceName] *
            GameManager.instance.GetResourcePrice(resourceName) *
            (BigInteger)(quantitySlider.slider.value * multiplier / 100)
        );
    }

    private void Tick()
    {
        foreach (ResourceListItem item in marketContent.resources)
        {
            
            if(item.transform.Find("AutosellToggle").GetComponent<Toggle>().isOn)
            {
                SellResources(item);
            }
        }
        
    }
}
