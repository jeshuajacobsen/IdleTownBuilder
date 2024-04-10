using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Numerics;

public class ResearchInfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    BigInteger baseCost;
    int level;
    int maxLevel;
    public UnityEvent<string> onUpgrade;

    bool isBuilding;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(string title, string description, BigInteger baseCost, int level, int maxLevel, bool isBuilding)
    {
        if (onUpgrade == null)
        {
            onUpgrade = new UnityEvent<string>();
        }
        titleText.text = title;
        descriptionText.text = description;
        transform.parent.gameObject.SetActive(true);
        this.baseCost = baseCost;
        this.level = level;
        this.maxLevel = maxLevel;
        this.isBuilding = isBuilding;
        costText.text = "" + GameManager.BigIntToExponentString(getCost());
        currentLevelText.text = level + "/" + maxLevel;
    }

    public void Upgrade()
    {
        if (GameManager.instance.HasEnoughPrestige(getCost()) && level < maxLevel)
        {
            if (this.isBuilding)
            {
                ResearchManager.instance.BuildingResearchUpgrade(titleText.text);
            } else {
                ResearchManager.instance.PrestigeResearchUpgrade(titleText.text);
            }
            
            GameManager.instance.SubtractCollectedPrestige(getCost());
            level++;
            currentLevelText.text = level + "/" + maxLevel;
            costText.text = "" + GameManager.BigIntToExponentString(getCost());
            onUpgrade.Invoke(titleText.text);
        }
        
    }

    private BigInteger getCost()
    {
        return GameManager.GrowthFunction(level, baseCost);
    }
}
