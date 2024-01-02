using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public class TimeAwayContent : MonoBehaviour
{
    public GainedResourceListItem gainedResourceListItemPrefab;
    public Transform contentTransform;

    List<GainedResourceListItem> resources = new List<GainedResourceListItem>();

    // Start is called before the first frame update
    void Start()
    {
        TimeManager.instance.timeAwayShowing.AddListener(UpdateListItems);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateListItems(int secondsAway, Dictionary<string, BigInteger> gainedResources, BigInteger gainedPrestige)
    {
        foreach (GainedResourceListItem resource in resources)
        {
            Destroy(resource.gameObject);
        }
        foreach (KeyValuePair<string, BigInteger> resourceKV in TimeManager.instance.timeAwayResources)
        {
            AddResourceListItem(resourceKV.Key, resourceKV.Value);
        }
    }

    public void AddResourceListItem(string resourceName, BigInteger quantity)
    {
        GainedResourceListItem resource = Instantiate(gainedResourceListItemPrefab, contentTransform);
        resource.transform.SetParent(transform, false);
        resource.InitValues(resourceName, "" + quantity);
        resources.Add(resource);
    }
}
