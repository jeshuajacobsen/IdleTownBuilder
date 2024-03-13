using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;

public class PrestigeResearchButton : MonoBehaviour
{

    int level = 0;
    [SerializeField] private int maxLevel;
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private BigInteger baseCost;

    [SerializeField] private bool isBuilding;

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
        }
        transform.Find("levelText").GetComponent<TextMeshProUGUI>().text = "" + level + "/" + maxLevel;
        if (isBuilding)
        {
            transform.Find("Mask").Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetBuildingSprite(title);
        }
        ResearchManager.instance.setResearch.AddListener(SetLevel);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInSelectedResearch()
    {
        ResearchInfoPanel researchPanel = transform.parent.parent.Find("SelectedResearchBackground")
            .Find("ResearchInfoPanel").GetComponent<ResearchInfoPanel>();
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
