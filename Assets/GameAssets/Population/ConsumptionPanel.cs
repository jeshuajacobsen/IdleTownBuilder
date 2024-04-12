using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SharpUI.Source.Common.UI.Elements.Loading;
using System.Numerics;
using UnityEditor.Rendering;
using Codice.Client.Commands.TransformerRule;
using TMPro;

public class ConsumptionPanel : MonoBehaviour
{
    private float productionTimer = 0;
    private const float requiredTime = 10;
    public List<Requirement> requirements;
    [SerializeField] private Requirement requirementPrefab;
    private Transform contentTransform;
    [SerializeField] private LoadingBar loadingBar;
    [SerializeField] private Toggle pauseToggle;
    public bool locked = true;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddRequirement(string resourceName, int resourceAmount)
    {
        Requirement requirement = Instantiate(requirementPrefab, contentTransform);
        requirement.transform.SetParent(transform.Find("RequirementsPanel"), false);
        requirement.InitValues(resourceName, resourceAmount);
        requirements.Add(requirement);
    }

    public void Tick()
    {
        if (!pauseToggle.isOn)
        {
            productionTimer++;
        
            if (productionTimer >= requiredTime)
            {
                productionTimer -= requiredTime;
                GameManager.instance.AddCityPrestige(GetPrestigeGenerated());
                Demographic currentDemo = transform.parent.GetComponent<Demographic>();
                transform.parent.Find("PrestigeText").GetComponent<TextMeshProUGUI>().text = GameManager.BigIntToExponentString(GetPrestigeGenerated());
                currentDemo.Happiness = CalculateHappiness();
                
                foreach (Requirement requirement in requirements)
                {
                    requirement.ConsumeResource();
                }
                currentDemo.GrowPopulation();
            }
    
            loadingBar.UpdatePercentage(productionTimer / requiredTime * 100);
        }
    }

    public BigInteger GetPrestigeGenerated()
    {
        BigInteger totalPrestige = 0;
        Demographic currentDemo = transform.parent.GetComponent<Demographic>();
        foreach (Requirement req in requirements)
        {
            totalPrestige += new BigInteger(req.PercentMet()) * 
                currentDemo.GetPrestigeGenerated() / 
                new BigInteger(requirements.Count) / 
                new BigInteger(100);
        }
        return totalPrestige;
    }

    public int CalculateHappiness()
    {
        int totalHappiness = 0;
        foreach (Requirement req in requirements)
        {
            totalHappiness += req.PercentMet() / requirements.Count;
        }
        int tier = transform.parent.GetComponent<Demographic>().tier;
        double multiplier = 1;
        if (tier == 1 || tier == 2)
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Wells") ? ResearchManager.instance.scienceResearchLevels["Wells"] * .1f : 0;
        }
        else if (tier == 3 || tier == 4)
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Fountains") ? ResearchManager.instance.scienceResearchLevels["Fountains"] * .1f : 0;
        }
        else if (tier == 5 || tier == 6)
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Baths") ? ResearchManager.instance.scienceResearchLevels["Baths"] * .1f : 0;
        }
        return totalHappiness * (int)(multiplier * 100) / 100;
    }    

    public void Unlock()
    {
        InvokeRepeating("Tick", 1.0f, 1.0f);
        locked = false;
    }
}
