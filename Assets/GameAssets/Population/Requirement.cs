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
    public BigInteger Cost
    {
        get 
        { 
            double multiplier = 1;
            multiplier -= resource == "Wheat" && ResearchManager.instance.scienceResearchLevels.ContainsKey("Foraging")? ResearchManager.instance.scienceResearchLevels["Foraging"] * .1f : 0;
            multiplier -= resource == "Vegetables" && ResearchManager.instance.scienceResearchLevels.ContainsKey("Gleaning")? ResearchManager.instance.scienceResearchLevels["Gleaning"] * .1f : 0; 
            multiplier = Math.Round(multiplier, 2);
            return cost * new BigInteger(multiplier * 100) / 100;
        }
        set 
        { 
            this.cost = value;
        }
    }
    public int population = 1;
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
                costText.text = GameManager.BigIntToExponentString(GameManager.instance.resources[resource]) +
                    "/" + GameManager.BigIntToExponentString(Cost * population);
            }
            else
            {
                costText.text = "0" + "/" + GameManager.BigIntToExponentString(Cost * population);
            }
        }
    }

    public void InitValues(string newResource, BigInteger newCost)
    {
        resource = newResource;
        Cost = newCost;
        BigInteger resourceInStock = GameManager.instance.resources.ContainsKey(resource) ? GameManager.instance.resources[resource] : 0;
        transform.Find("Mask").Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetResourceSprite(newResource);
        
        transform.Find("costText").GetComponent<TextMeshProUGUI>().text = "" + resourceInStock + "/" + Cost * population;
    }

    public int PercentMet()
    {
        if (GameManager.instance.resources.ContainsKey(resource))
        {
            return (int)((float)Min(GameManager.instance.resources[resource], Cost * population) / (float)(Cost * population) * 100);
        }
        return 0;
    }

    public int ConsumeResource()
    {
        if (GameManager.instance.resources.ContainsKey(resource))
        {
            GameManager.instance.SubtractResources(resource, Min(GameManager.instance.resources[resource], Cost * population));
            return (int)(Min(GameManager.instance.resources[resource], Cost * population));
        }
        return 0;
    }

    private BigInteger Min(BigInteger a, BigInteger b)
    {
        return a < b ? a : b;
    }
}


