using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Numerics;

public class ResourceListItem : MonoBehaviour
{

    public string resourceName = "";
    BigInteger quantity = 0;
    BigInteger price = 0;
    [SerializeField] private TextMeshProUGUI resourceText;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private TextMeshProUGUI priceText;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        resourceText = transform.Find("ResourceText").GetComponent<TextMeshProUGUI>();
        quantityText = transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        priceText = transform.Find("PriceText").GetComponent<TextMeshProUGUI>();
        transform.Find("ResourceIcon").GetComponent<Image>().sprite = SpriteManager.instance.GetResourceSprite(resourceName);

        GameManager.instance.onResourcesChanged.AddListener(UpdateQuantity);
        quantity = GameManager.instance.resources[resourceName];
        UpdateQuantity(resourceName, quantity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateQuantity(string resourceChanged, BigInteger newQuantity)
    {
        if (resourceChanged == resourceName)
        {
            quantity = newQuantity;
            quantityText.text = "" + quantity;
        }
    }

    public void InitValues(string newName, BigInteger newQuantity, BigInteger newPrice)
    {
        resourceName = newName;
        quantity = newQuantity;
        price = newPrice;

        resourceText.text = resourceName;
        quantityText.text = "" + quantity;
        priceText.text = "$" + price;
    }

    public void SelectResource()
    {
        transform.parent.GetComponent<MarketContent>().SetSelectedResource(resourceName);
    }
}
