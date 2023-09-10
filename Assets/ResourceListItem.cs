using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ResourceListItem : MonoBehaviour
{

    public string resourceName = "";
    int quantity = 0;
    int price = 0;
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

        GameManager.instance.onResourcesChanged.AddListener(UpdateQuantity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateQuantity(string resourceChanged, int newQuantity)
    {
        if (resourceChanged == resourceName)
        {
            quantity = newQuantity;
            quantityText.text = "" + quantity;
        }
    }

    public void initValues(string newName, int newQuantity, int newPrice)
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
