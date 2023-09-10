using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketContent : MonoBehaviour
{

    List<ResourceListItem> resources = new List<ResourceListItem>();

    public ResourceListItem ResourceListItemPrefab;
    public Transform contentTransform;
    private Transform selectedHighlight;
    public ResourceListItem selectedResource;

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
    }

    // Update is called once per frame
    void Update()
    {
        
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
        resource.initValues(resourceName, quantity, 1);
    }
}
