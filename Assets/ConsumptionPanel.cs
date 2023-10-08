using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpUI.Source.Common.UI.Elements.Loading;

public class ConsumptionPanel : MonoBehaviour
{
    private float productionTimer = 0;
    private const float requiredTime = 10;
    public List<Requirement> requirements;
    private bool producing = false;
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
            requirement.InitValues("Wheat", 1);
            requirements.Add(requirement);
        }
    }

    public void Tick()
    {
        if (producing)
        {
            productionTimer++;
        
            if (productionTimer >= requiredTime)
            {
                productionTimer -= requiredTime;
                GameManager.instance.AddCityPrestige(transform.parent.GetComponent<Demographic>().GetPrestigeGenerated());

                if (!CanStartNextProduction())
                {
                    productionTimer = 0;
                    producing = false;
                }
                else
                {
                    StartNextProduction();
                }
            }
        }
        else
        {
            if (CanStartNextProduction())
            {
                StartNextProduction();
            }
        }
 
        loadingBar.UpdatePercentage(productionTimer / requiredTime * 100);
    }

    private bool CanStartNextProduction()
    {
        bool canStart = true;
        foreach (Requirement requirement in requirements)
        {
            if (!requirement.IsMet())
            {
                canStart = false;
            }
        }
        return canStart;
    }

    private void StartNextProduction()
    {
        producing = true;
        foreach (Requirement requirement in requirements)
        {
            requirement.ConsumeResource();
        }
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
