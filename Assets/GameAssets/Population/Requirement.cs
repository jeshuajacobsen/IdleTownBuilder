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
    private BigInteger cost;
    public BigInteger Cost
    {
        get 
        { 
            double multiplier = 1; 
            multiplier -= resource == "Wheat" && ResearchManager.instance.scienceResearchLevels.ContainsKey("Foraging")? ResearchManager.instance.scienceResearchLevels["Foraging"] * .1f : 0;
            multiplier -= resource == "Vegetables" && ResearchManager.instance.scienceResearchLevels.ContainsKey("Gleaning")? ResearchManager.instance.scienceResearchLevels["Gleaning"] * .1f : 0;
            multiplier = Math.Round(multiplier, 2);
            return population * cost * new BigInteger(multiplier * 100) / 100;
        }
        set 
        { 
            this.cost = value;
        }
    }
    public BigInteger population = 1;
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
                    "/" + GameManager.BigIntToExponentString(Cost);
            }
            else
            {
                costText.text = "0" + "/" + GameManager.BigIntToExponentString(Cost);
            }
        }
    }

    public void InitValues(string newResource, BigInteger newCost)
    {
        resource = newResource;
        Cost = newCost;
        BigInteger resourceInStock = GameManager.instance.resources.ContainsKey(resource) ? GameManager.instance.resources[resource] : 0;
        transform.Find("Mask").Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetResourceSprite(newResource);
        
        transform.Find("costText").GetComponent<TextMeshProUGUI>().text = "" + resourceInStock + "/" + Cost;
    }

    public int PercentMet()
    {
        if (GameManager.instance.resources.ContainsKey(resource))
        {
            return (int)((float)Min(GameManager.instance.resources[resource], Cost) / (float)(Cost) * 100);
        }
        return 0;
    }

    public int ConsumeResource()
    {
         
        if (GameManager.instance.resources.ContainsKey(resource))
        {
            int quantity = (int)Min(GameManager.instance.resources[resource], Cost);
            GameManager.instance.SubtractResources(resource, quantity);

            if (ResearchManager.instance.scienceResearchLevels.ContainsKey("Taxation") && 
                transform.parent.parent.parent.GetComponent<Demographic>().tier < 4 && 
                quantity > 0)
            {
                GameManager.instance.AddCoins(quantity * GameManager.instance.GetResourcePrice(resource) * 
                    ResearchManager.instance.scienceResearchLevels["Taxation"] * 10 / 100);
            } else if (ResearchManager.instance.scienceResearchLevels.ContainsKey("Luxury Tax") && 
                transform.parent.parent.parent.GetComponent<Demographic>().tier > 3 && 
                quantity > 0)
            {
                GameManager.instance.AddCoins(quantity * GameManager.instance.GetResourcePrice(resource) *
                    ResearchManager.instance.scienceResearchLevels["Luxury Tax"] * 10 / 100);
            }
            return quantity;
        }
        return 0;
    }

    private BigInteger Min(BigInteger a, BigInteger b)
    {
        return a < b ? a : b;
    }
}


