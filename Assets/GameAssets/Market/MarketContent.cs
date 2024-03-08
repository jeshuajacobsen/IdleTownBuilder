using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Numerics;

public class MarketContent : MonoBehaviour
{

    public List<ResourceListItem> resources = new List<ResourceListItem>();

    public ResourceListItem ResourceListItemPrefab;
    public Transform contentTransform;
    private Transform selectedHighlight;
    public ResourceListItem selectedResource;

    public UnityEvent<string> onSelectedResourceChange;

    void Awake()
    {
        onSelectedResourceChange = new UnityEvent<string>();

        GameManager.instance.onResourcesAdded.AddListener(AddResourceListItem);       

        GameManager.instance.resetCity.AddListener(Reset);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset(string newCityName)
    {
        if (selectedHighlight == null)
        {
            selectedHighlight = transform.Find("SelectedHighlight");
        }
        selectedHighlight.SetParent(transform, false);
        
        foreach(ResourceListItem item in resources)
        {
            Destroy(item.gameObject);
        }
        resources = new List<ResourceListItem>();
        foreach (KeyValuePair<string, BigInteger> resourceKV in GameManager.instance.resources)
        {
            AddResourceListItem(resourceKV.Key, resourceKV.Value);
        }
        SetSelectedResource(resources[0].resourceName);
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
                onSelectedResourceChange.Invoke(selectedResource.resourceName);
            }
        }
    }

    public void UpdateTextForResource(string resourceName)
    {
        
    }

    public void AddResourceListItem(string resourceName, BigInteger quantity)
    {
        ResourceListItem resource = Instantiate(ResourceListItemPrefab, contentTransform);
        resources.Add(resource);
        resource.transform.SetParent(transform, false);
        resource.InitValues(resourceName, GameManager.instance.resources[resourceName], GameManager.instance.GetResourcePrice(resourceName));
    }

    public void PrepForSave(SaveData saveData)
    {
        saveData.autosellSettings = new Dictionary<string, bool>();
        foreach (ResourceListItem resource in resources)
        {
            saveData.autosellSettings[resource.resourceName] = resource.transform.Find("AutosellToggle").GetComponent<Toggle>().isOn;
        }
    }

    public void LoadSavedData(SaveData saveData) 
    {
        foreach (string key in saveData.autosellSettings.Keys)
        {
            ResourceListItem resource = resources.Find(resource => resource.resourceName == key);
            if (resource != null)
            {
                resource.transform.Find("AutosellToggle").GetComponent<Toggle>().isOn = saveData.autosellSettings[key];
            } else {
                Debug.Log("Couldn't find building to load. " + key);
            }
        }
    }
}
