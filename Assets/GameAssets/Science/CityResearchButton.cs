using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class CityResearchButton : MonoBehaviour
{
    public int level = 0;
    [SerializeField] private int maxLevel;
    public string title;
    [SerializeField] private string description;

    [SerializeField] private string resourceCostNames;
    [SerializeField] private string resourceCostPrices;

    [SerializeField] private TextMeshProUGUI researchedRatioText;

    [SerializeField] private CityResearchButton dependency;
    public UnityEvent onUpgrade;
    List<int> intCostList = new List<int>();

    void Awake()
    {
        researchedRatioText.text = "" + level + "/" + maxLevel;
        Button button = transform.GetComponent<Button>();
        button.onClick.AddListener(OpenInSelectedResearch);
        onUpgrade = new UnityEvent();
    }

    void Start()
    {
        ResearchManager.instance.setScienceResearch.AddListener(SetLevel);
        ResearchManager.instance.resetScienceResearch.AddListener(Reset);
        if (dependency != null)
        {
            dependency.onUpgrade.AddListener(Unlock);
        } 
        else
        {
            Unlock();
        }

        string[] stringArray = resourceCostPrices.Split(' ');
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
    }

    void Update()
    {
        bool preReqMet = dependency == null || dependency.level > 0;
        if (!preReqMet)
        {
            SetTextColor(Color.white);
            transform.Find("BackgroundImage").GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("ResearchBackgroundGrey");
            return;
        }
        if (level >= maxLevel)
        {
            SetTextColor(Color.green);
            transform.Find("BackgroundImage").GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("ResearchBackgroundGreen");
            return;
        }
        List<string> resources = new List<string>(resourceCostNames.Split(','));
        bool canAffordUpgrade = true;
        for (int i = 0; i < resources.Count; i++)
        {
            
            if (GameManager.instance.resources.ContainsKey(resources[i]))
            {
                if (GameManager.instance.resources[resources[i]] < ResourceCost.GetRequiredAmount(level, intCostList[i]))
                {
                    canAffordUpgrade = false;
                }
            }
            else 
            {
                canAffordUpgrade = false;
            }
        }
        if (canAffordUpgrade)
        {
            SetTextColor(Color.yellow);
            transform.Find("BackgroundImage").GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("ResearchBackgroundYellow");
        }
        else 
        {
            SetTextColor(Color.red);
            transform.Find("BackgroundImage").GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("ResearchBackgroundRed");
        }
    }

    private void SetTextColor(Color color)
    {
        //transform.Find("ResearchName").GetComponent<TextMeshProUGUI>().color = color;
        //transform.Find("ResearchedRatioText").GetComponent<TextMeshProUGUI>().color = color;
    }

    public void OpenInSelectedResearch()
    {
        CityResearchInfoPanel researchPanel = transform.parent.parent.Find("SelectedResearchBackground")
            .Find("CityResearchInfoPanel").GetComponent<CityResearchInfoPanel>();

        List<string> resources = new List<string>(resourceCostNames.Split(','));
        researchPanel.Setup(title, description, level, maxLevel, resources, intCostList, dependency);
        researchPanel.onUpgrade.RemoveAllListeners();
        researchPanel.onUpgrade.AddListener(Upgrade);
    }

    private void Reset()
    {
        level = 0;
        researchedRatioText.text = "" + level + "/" + maxLevel;
        
        SetTextColor(Color.white);
        transform.Find("BackgroundImage").GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("ResearchBackgroundGrey");
    }

    public void Upgrade(string titleToUpgrade)
    {
        if (title == titleToUpgrade)
        {
            level++;
            researchedRatioText.text = "" + level + "/" + maxLevel;
            onUpgrade.Invoke();
        }
    }

    private void SetLevel(string titleToUpgrade, int level)
    {
        if (title == titleToUpgrade)
        {
            this.level = level;
            researchedRatioText.text = "" + level + "/" + maxLevel;
            if (level > 0)
            {
                onUpgrade.Invoke();
            }
        }
    }

    private void Unlock()
    {
        //transform.Find("Mask").Find("LockedPanel").gameObject.SetActive(false);
    }
}
