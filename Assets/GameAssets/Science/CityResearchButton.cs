using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CityResearchButton : MonoBehaviour
{
    int level = 0;
    [SerializeField] private int maxLevel;
    [SerializeField] private string title;
    [SerializeField] private string description;

    [SerializeField] private string resourceCostNames;
    [SerializeField] private string resourceCostPrices;

    [SerializeField] private TextMeshProUGUI researchedRatioText;

    void Awake()
    {
        researchedRatioText.text = "" + level + "/" + maxLevel;
        Button button = transform.GetComponent<Button>();
        button.onClick.AddListener(OpenInSelectedResearch);
    }

    void Start()
    {
        ResearchManager.instance.setScienceResearch.AddListener(SetLevel);
        ResearchManager.instance.resetScienceResearch.AddListener(Reset);
    }

    void Update()
    {
        
    }

    public void OpenInSelectedResearch()
    {
        CityResearchInfoPanel researchPanel = transform.parent.parent.Find("SelectedResearchBackground")
            .Find("CityResearchInfoPanel").GetComponent<CityResearchInfoPanel>();

        string[] stringArray = resourceCostPrices.Split(' ');

        // Create a list of integers to store the parsed values
        List<int> intCostList = new List<int>();

        // Parse each substring and add it to the list
        foreach (string substring in stringArray)
        {
            if (int.TryParse(substring, out int intValue))
            {
                intCostList.Add(intValue);
            }
            else
            {
                Debug.Log($"Failed to parse: {substring}");
            }
        }
        researchPanel.Setup(title, description, level, maxLevel, new List<string>(resourceCostNames.Split(',')), intCostList);
        researchPanel.onUpgrade.RemoveAllListeners();
        researchPanel.onUpgrade.AddListener(Upgrade);
    }

    private void Reset()
    {
        level = 0;
        researchedRatioText.text = "" + level + "/" + maxLevel;
    }

    public void Upgrade(string titleToUpgrade)
    {
        if (title == titleToUpgrade)
        {
            level++;
            researchedRatioText.text = "" + level + "/" + maxLevel;
        }
    }

    private void SetLevel(string titleToUpgrade, int level)
    {
        if (title == titleToUpgrade)
        {
            this.level = level;
            researchedRatioText.text = "" + level + "/" + maxLevel;
        }
    }
}
