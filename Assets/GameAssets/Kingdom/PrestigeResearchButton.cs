using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
using UnityEngine.Events;

public class PrestigeResearchButton : MonoBehaviour
{

    int level = 0;
    [SerializeField] private int maxLevel;
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private BigInteger baseCost;

    [SerializeField] private bool isBuilding;

    public UnityEvent<string> onUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        if (isBuilding)
        {
            level = ResearchManager.instance.buildingResearchLevels.ContainsKey(title) ? ResearchManager.instance.buildingResearchLevels[title] : 0;
            baseCost = GameManager.instance.gameData.GetBuildingData(title).researchBaseCost;
        }
        else
        {
            baseCost = 100;
            level = ResearchManager.instance.prestigeResearchLevels.ContainsKey(title) ? ResearchManager.instance.prestigeResearchLevels[title] : 0;
        }
        transform.Find("levelText").GetComponent<TextMeshProUGUI>().text = "" + level + "/" + maxLevel;
        if (isBuilding)
        {
            transform.Find("Mask").Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetBuildingSprite(title);
        }

        ResearchManager.instance.setResearch.AddListener(SetLevel);
        
    }

    public void InitValues(string title, bool isBuilding)
    {
        this.title = title;
        this.isBuilding = isBuilding;
        if (isBuilding)
        {
            BuildingData buildingData = GameManager.instance.gameData.GetBuildingData(title);
            this.description = title;
            this.maxLevel = 50;
            this.baseCost = buildingData.researchBaseCost;
        }
        else
        {
            PrestigeResearchData researchData = GameManager.instance.gameData.GetPrestigeResearchData(title);
            this.description = researchData.description;
            this.maxLevel = researchData.maxLevel;
            this.baseCost = researchData.baseCost;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInSelectedResearch()
    {
        ResearchInfoPanel researchPanel = GameManager.instance.kingdomSelectedResearch;
        researchPanel.Setup(title, description, baseCost, level, maxLevel, isBuilding);
        researchPanel.onUpgrade.RemoveAllListeners();
        researchPanel.onUpgrade.AddListener(Upgrade);
    }

    public void Upgrade(string titleToUpgrade)
    {
        if (title == titleToUpgrade)
        {
            level++;
            transform.Find("levelText").GetComponent<TextMeshProUGUI>().text = "" + level + "/" + maxLevel;
            onUpgrade.Invoke(title);
        }
    }

    public void SetLevel(string titleToUpgrade, int level)
    {
        if (title == titleToUpgrade)
        {
            this.level = level;
            transform.Find("levelText").GetComponent<TextMeshProUGUI>().text = "" + level + "/" + maxLevel;
        }
    }
}
