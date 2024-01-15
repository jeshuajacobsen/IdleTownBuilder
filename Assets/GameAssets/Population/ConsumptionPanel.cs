using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpUI.Source.Common.UI.Elements.Loading;
using System.Numerics;

public class ConsumptionPanel : MonoBehaviour
{
    private float productionTimer = 0;
    private const float requiredTime = 10;
    public List<Requirement> requirements;
    [SerializeField] private Requirement requirementPrefab;
    private Transform contentTransform;
    [SerializeField] private LoadingBar loadingBar;
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

    public BigInteger GetPrestigeGenerated()
    {
        BigInteger totalPrestige = 0;
        Demographic currentDemo = transform.parent.GetComponent<Demographic>();
        foreach (Requirement req in requirements)
        {
            totalPrestige += new BigInteger((int)req.PercentMet()) * 
                currentDemo.GetPrestigeGenerated() / 
                new BigInteger(requirements.Count) / 
                new BigInteger(100);
        }
        return totalPrestige;
    }

    public void Unlock()
    {
        InvokeRepeating("Tick", 1.0f, 1.0f);
        locked = false;
    }
}
