using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrestigeResearchButton : MonoBehaviour
{

    int level = 0;
    [SerializeField] private int maxLevel;
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private int baseCost;

    [SerializeField] private bool isBuilding;

    // Start is called before the first frame update
    void Start()
    {
        if (isBuilding)
        {
            level = ResearchManager.instance.buildingLevels.ContainsKey(title) ? ResearchManager.instance.buildingLevels[title] : 0;
        }
        transform.Find("levelText").GetComponent<TextMeshProUGUI>().text = "" + level + "/" + maxLevel;
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
}
