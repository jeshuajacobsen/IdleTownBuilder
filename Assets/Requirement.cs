using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Requirement : MonoBehaviour
{
    public string resource;
    private int cost;
    public int level = 1;
    [SerializeField] private Image resourceImage;
    [SerializeField] private TextMeshProUGUI costText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (costText != null)
        {
            if (resource != null && GameManager.instance.resources.ContainsKey(resource))
            {
                costText.text = "" + GameManager.instance.resources[resource] + "/" + cost * level;
            }
            else
            {
                costText.text = "0" + "/" + cost;
            }
        }
    }

    public void InitValues(string newResource, int newCost)
    {
        resource = newResource;
        cost = newCost;
        int resourceInStock = GameManager.instance.resources.ContainsKey(resource) ? GameManager.instance.resources[resource] : 0;
        
        transform.Find("costText").GetComponent<TextMeshProUGUI>().text = "" + resourceInStock + "/" + cost * level;
    }

    public float PercentMet()
    {
        if (GameManager.instance.resources.ContainsKey(resource))
        {
            return Math.Min(GameManager.instance.resources[resource], cost * level) / (cost * level);
        }
        return 0;
    }

    public int ConsumeResource()
    {
        if (GameManager.instance.resources.ContainsKey(resource))
        {
            GameManager.instance.SubtractResources(resource, Math.Min(GameManager.instance.resources[resource], cost * level));
            return (int)(Math.Min(GameManager.instance.resources[resource], cost * level));
        }
        return 0;
    }
}
