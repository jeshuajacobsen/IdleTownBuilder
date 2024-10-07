using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
using System;

public class ResourceCost : MonoBehaviour
{
    public TextMeshProUGUI resourceName;
    public TextMeshProUGUI resourceCost;

    private BigInteger requiredAmount = 1;
    public int level;
    public BigInteger GetRequiredAmount(int level)
    {
        return GameManager.ResearchGrowthFunction(level, requiredAmount);
    }

    public static BigInteger GetRequiredAmount(int level, BigInteger baseAmount)
    {
        return GameManager.ResearchGrowthFunction(level, baseAmount);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.resources.ContainsKey(resourceName.text))
        {
            resourceCost.text =  "0/" + GameManager.BigIntToExponentString(GetRequiredAmount(level));
            resourceCost.color = Color.red;
        } 
        else 
        {
            resourceCost.text = "" + GameManager.BigIntToExponentString(GameManager.instance.resources[resourceName.text]) +
              "/" + GameManager.BigIntToExponentString(GetRequiredAmount(level));
            if (GetRequiredAmount(level) > GameManager.instance.resources[resourceName.text])
            {
                resourceCost.color = Color.red;
            } else {
                resourceCost.color = Color.green;
            }
        }
    }

    public void InitValues(string newResourceName, int newRequiredAmount, int level)
    {
        requiredAmount = newRequiredAmount;
        resourceName.text = newResourceName;
        this.level = level;
        if (GameManager.instance.resources.ContainsKey(newResourceName))
        {
            resourceCost.text = "" + GameManager.BigIntToExponentString(GameManager.instance.resources[newResourceName]) + "/"
                 + GameManager.BigIntToExponentString(GetRequiredAmount(level));

            if (GetRequiredAmount(level) > GameManager.instance.resources[newResourceName])
            {
                resourceCost.color = Color.red;
            } else {
                resourceCost.color = Color.green;
            }
        }
        else
        {
            resourceCost.text = "0/" + GameManager.BigIntToExponentString(GetRequiredAmount(level));
            resourceCost.color = Color.red;
        }
        transform.Find("Mask").Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetResourceSprite(newResourceName);
    }
}
