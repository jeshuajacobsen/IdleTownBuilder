using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ResearchInfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    int baseCost;
    int level;
    int maxLevel;
    public UnityEvent<string> onUpgrade;

    bool isBuilding;

    // Start is called before the first frame update
    void Start()
    {
        onUpgrade = new UnityEvent<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(string title, string description, int baseCost, int level, int maxLevel, bool isBuilding)
    {
        titleText.text = title;
        descriptionText.text = description;
        transform.parent.gameObject.SetActive(true);
        this.baseCost = baseCost;
        this.level = level;
        this.maxLevel = maxLevel;
        this.isBuilding = isBuilding;
        costText.text = "" + getCost();
        currentLevelText.text = level + "/" + maxLevel;
    }

    public void Upgrade()
    {
        if (GameManager.instance.HasEnoughPrestige(getCost()) && level < maxLevel)
        {
            if (this.isBuilding)
            {
                ResearchManager.instance.BuildingUpgrade(titleText.text);
            } else {
                ResearchManager.instance.Upgrade(titleText.text);
            }
            
            GameManager.instance.SubtractCollectedPrestige(getCost());
            level++;
            currentLevelText.text = level + "/" + maxLevel;
            costText.text = "" + getCost();
            onUpgrade.Invoke(titleText.text);
        }
        
    }

    private int getCost()
    {
        return (int)(baseCost * (level + 1) * 2.5);
    }
}
