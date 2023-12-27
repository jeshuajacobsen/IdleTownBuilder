using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GainedResourceListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resourceName;
    [SerializeField] private TextMeshProUGUI quantityGained;
    List<ResourceListItem> resources = new List<ResourceListItem>();

    public GainedResourceListItem gainedResourceListItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        resourceName = transform.Find("ResourceName").GetComponent<TextMeshProUGUI>();
        quantityGained = transform.Find("QuantityGained").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitValues(string newName, string newQuantity)
    {
        resourceName.text = newName;
        quantityGained.text = newQuantity;
    }
}
