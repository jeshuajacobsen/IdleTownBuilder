using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MarketContent : MonoBehaviour
{

    List<ResourceListItem> resources = new List<ResourceListItem>();

    public ResourceListItem ResourceListItemPrefab;
    public Transform contentTransform;
    private Transform selectedHighlight;
    public ResourceListItem selectedResource;

    public UnityEvent onSelectedResourceChange;

    void Awake()
    {
        onSelectedResourceChange = new UnityEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        foreach (KeyValuePair<string, int> resourceKV in GameManager.instance.resources)
        {
            AddResourceListItem(resourceKV.Key, resourceKV.Value);
        }
        
        GameManager.instance.onResourcesAdded.AddListener(AddResourceListItem);

        selectedHighlight = transform.Find("selectedHighlight");

        SetSelectedResource(resources[0].resourceName);

        GameManager.instance.resetCity.AddListener(Reset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Reset(string newCityName)
    {
        foreach(ResourceListItem item in resources)
        {
            Destroy(item.gameObject);
        }
        resources = new List<ResourceListItem>();
    }

    public void SetSelectedResource(string resourceName)
    {
        foreach (ResourceListItem item in resources)
        {
            if (item.resourceName == resourceName)
            {
                selectedResource = item;
                selectedHighlight.SetParent(item.transform, false);
                selectedHighlight.gameObject.SetActive(true);
                selectedHighlight.SetSiblingIndex(0);
                onSelectedResourceChange.Invoke();
            }
        }
    }

    public void UpdateTextForResource(string resourceName)
    {
        
    }

    public void AddResourceListItem(string resourceName, int quantity)
    {
        ResourceListItem resource = Instantiate(ResourceListItemPrefab, contentTransform);
        resources.Add(resource);
        resource.transform.SetParent(transform, false);
        resource.InitValues(resourceName, GameManager.instance.resources[resourceName], GameManager.instance.resourcePrices[resourceName]);
    }
}
