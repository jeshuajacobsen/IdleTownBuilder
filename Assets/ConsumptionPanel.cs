using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpUI.Source.Common.UI.Elements.Loading;

public class ConsumptionPanel : MonoBehaviour
{
    private float productionTimer = 0;
    private const float requiredTime = 10;
    public List<Requirement> requirements;
    [SerializeField] private Requirement requirementPrefab;
    private Transform contentTransform;
    [SerializeField] private LoadingBar loadingBar;
    private int unlockCost = 1;
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

    public void InitValues(string demoName)
    {
        requirements = new List<Requirement>();
        if (demoName == "Peasants")
        {
            Requirement requirement = Instantiate(requirementPrefab, contentTransform);
            requirement.transform.SetParent(transform.Find("RequirementsPanel"), false);
            requirement.InitValues("Wheat", 20);
            requirements.Add(requirement);
            
            requirement = Instantiate(requirementPrefab, contentTransform);
            requirement.transform.SetParent(transform.Find("RequirementsPanel"), false);
            requirement.InitValues("Pottery", 5);
            requirements.Add(requirement);
            
            requirement = Instantiate(requirementPrefab, contentTransform);
            requirement.transform.SetParent(transform.Find("RequirementsPanel"), false);
            requirement.InitValues("Vegetables", 2);
            requirements.Add(requirement);
        }
        if (demoName == "Commoners")
        {
            Requirement requirement = Instantiate(requirementPrefab, contentTransform);
            requirement.transform.SetParent(transform.Find("RequirementsPanel"), false);
            requirement.InitValues("Bread", 10);
            requirements.Add(requirement);
            
            requirement = Instantiate(requirementPrefab, contentTransform);
            requirement.transform.SetParent(transform.Find("RequirementsPanel"), false);
            requirement.InitValues("Pottery", 10);
            requirements.Add(requirement);
            
            requirement = Instantiate(requirementPrefab, contentTransform);
            requirement.transform.SetParent(transform.Find("RequirementsPanel"), false);
            requirement.InitValues("Vegetables", 20);
            requirements.Add(requirement);

            requirement = Instantiate(requirementPrefab, contentTransform);
            requirement.transform.SetParent(transform.Find("RequirementsPanel"), false);
            requirement.InitValues("Furniture", 5);
            requirements.Add(requirement);
        }
    }

    public void Tick()
    {
        productionTimer++;
    
        if (productionTimer >= requiredTime)
        {
            productionTimer -= requiredTime;
            GameManager.instance.AddCityPrestige(GetPrestigeGenerated());

            foreach (Requirement requirement in requirements)
            {
                requirement.ConsumeResource();
            }
        }
 
        loadingBar.UpdatePercentage(productionTimer / requiredTime * 100);
    }

    public int GetPrestigeGenerated()
    {
        float totalPrestige = 0;
        foreach (Requirement req in requirements)
        {
            totalPrestige += req.PercentMet() * transform.parent.GetComponent<Demographic>().GetPrestigeGenerated() / requirements.Count;
        }
        return (int)totalPrestige;
    }

    public void Unlock()
    {
        InvokeRepeating("Tick", 1.0f, 1.0f);
        locked = false;
    }

    public int GetUnlockCost()
    {
        return unlockCost;
    }
}
