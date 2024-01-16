using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class CityResearchInfoPanel : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI currentLevelText;

    [SerializeField] private ResourceCostPanel resourceCostPanel;

    [SerializeField] private ResourceCost resourceCostPrefab;

    int level;
    int maxLevel;
    List<ResourceCost> resourceCosts = new List<ResourceCost>();

    public UnityEvent<string> onUpgrade;

    public Transform contentTransform;

    // Start is called before the first frame update
    void Start()
    {
        
        Button button = transform.Find("UpgradeButton").GetComponent<Button>();
        button.onClick.AddListener(Upgrade);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(string title, string description, int level, int maxLevel, List<string> resources, List<int> costs)
    {
        onUpgrade = new UnityEvent<string>();
        foreach (ResourceCost resourceCost in resourceCosts)
        {
            Destroy(resourceCost.gameObject);
        }
        resourceCosts = new List<ResourceCost>();
        titleText.text = title;
        descriptionText.text = description;
        transform.parent.gameObject.SetActive(true);
        this.level = level;
        this.maxLevel = maxLevel;
        currentLevelText.text = level + "/" + maxLevel;
        for (int i = 0; i < resources.Count; i++)
        {
            ResourceCost resourceCost = Instantiate(resourceCostPrefab, contentTransform);
            resourceCost.transform.SetParent(resourceCostPanel.transform, false);
            resourceCost.InitValues(resources[i], costs[i], level);
            resourceCosts.Add(resourceCost);
        }
    }

    public void Upgrade()
    {
        bool canAffordUpgrade = true;
        foreach (ResourceCost cost in resourceCosts)
        {
            if (GameManager.instance.resources.ContainsKey(cost.resourceName.text))
            {
                if (GameManager.instance.resources[cost.resourceName.text] < cost.GetRequiredAmount(level))
                {
                    canAffordUpgrade = false;
                }
            }
            else 
            {
                canAffordUpgrade = false;
            }
        }
        if (canAffordUpgrade && level < maxLevel)
        {
            level++;
            ResearchManager.instance.CityResearchUpgrade(titleText.text);
            foreach (ResourceCost cost in resourceCosts)
            {
                GameManager.instance.SubtractResources(cost.resourceName.text, cost.GetRequiredAmount(level));
                cost.level++;
            }
            
            currentLevelText.text = level + "/" + maxLevel;

            onUpgrade.Invoke(titleText.text);
        }
    }
}
