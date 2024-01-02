using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;

public class Requirement : MonoBehaviour
{
    public string resource;
    public BigInteger cost;
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

    public void InitValues(string newResource, BigInteger newCost)
    {
        resource = newResource;
        cost = newCost;
        BigInteger resourceInStock = GameManager.instance.resources.ContainsKey(resource) ? GameManager.instance.resources[resource] : 0;
        transform.Find("Mask").Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetResourceSprite(newResource);
        
        transform.Find("costText").GetComponent<TextMeshProUGUI>().text = "" + resourceInStock + "/" + cost * level;
    }

    public BigInteger PercentMet()
    {
        if (GameManager.instance.resources.ContainsKey(resource))
        {
            return Min(GameManager.instance.resources[resource], cost * level) / (cost * level) * 100;
        }
        return 0;
    }

    public int ConsumeResource()
    {
        if (GameManager.instance.resources.ContainsKey(resource))
        {
            GameManager.instance.SubtractResources(resource, Min(GameManager.instance.resources[resource], cost * level));
            return (int)(Min(GameManager.instance.resources[resource], cost * level));
        }
        return 0;
    }

    private BigInteger Min(BigInteger a, BigInteger b)
    {
        return a < b ? a : b;
    }
}


