using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using SharpUI.Source.Common.UI.Elements.Loading;
using System.Numerics;
using System;
public class ProductionOutput : MonoBehaviour
{

    private double productionTimer = 0;
    public int requiredTime = 10;
    public string resource = "";
    public UnityEvent<string> onProductionClick;
    private bool producing = false;
    public Building building;

    [SerializeField] private Toggle pauseToggle;
    [SerializeField] private Image loadingBarImage;

    void Awake()
    {
        onProductionClick = new UnityEvent<string>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitValues(string newResource, int newRequiredTime)
    {
        requiredTime = newRequiredTime;
        resource = newResource;
        GameManager.instance.productionTimers[newResource] = 0;
        transform.Find("Mask").Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetResourceSprite(newResource);
    }

    public void Tick(bool fromTap)
    {
        if (fromTap)
        {
            pauseToggle.isOn = false;
        }
        if (!pauseToggle.isOn)
        {
            if (producing)
            {
                productionTimer++;
                productionTimer += ResearchManager.instance.scienceResearchLevels.ContainsKey("Tapping") && fromTap ? ResearchManager.instance.scienceResearchLevels["Tapping"] * .2f: 0;
                productionTimer += ResearchManager.instance.scienceResearchLevels.ContainsKey("Advanced Tapping") && fromTap ? ResearchManager.instance.scienceResearchLevels["Advanced Tapping"] * .2f: 0;
                productionTimer += ResearchManager.instance.scienceResearchLevels.ContainsKey("Expert Tapping") && fromTap ? ResearchManager.instance.scienceResearchLevels["Expert Tapping"] * .2f: 0;
                productionTimer += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Tap Power") && fromTap ? ResearchManager.instance.prestigeResearchLevels["Tap Power"] * .1f: 0;

                if (resource == "Wheat" || resource == "Vegetables")
                {
                    productionTimer += ResearchManager.instance.scienceResearchLevels.ContainsKey("Fast Crops") ? ResearchManager.instance.scienceResearchLevels["Fast Crops"] * .1f: 0;
                }
    
                if (building.Manager != null && (building.Manager.effect1Type == "ProductionSpeed" || building.Manager.effect2Type == "ProductionSpeed"))
                {
                    productionTimer += building.Manager.GetEffectMagnitude("ProductionSpeed");
                }

                if (productionTimer >= requiredTime)
                {
                    productionTimer -= requiredTime;
                    GameManager.instance.AddResources(resource, building.GetProductionQuantity());

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

            //TODO move production timer to timemanager.
            GameManager.instance.productionTimers[resource] = productionTimer;
            loadingBarImage.fillAmount = (float)productionTimer / requiredTime;
        }
    }

    private void Tick()
    {
        Tick(false);
    }

    public void ProductionClick()
    {
        onProductionClick.Invoke(resource);
    }

    public void HandleProductionClick()
    {
        Tick(true);
    }

    public bool CanStartNextProduction()
    {
        bool canStart = true;
        BigInteger cost;
        double multiplier = 1;
        if (building.Manager != null && (building.Manager.effect1Type == "LessConsumption" || building.Manager.effect2Type == "LessConsumption"))
        {
            multiplier -= building.Manager.GetEffectMagnitude("LessConsumption");
        }

        if (building.Manager != null && (building.Manager.effect1Type == "NoConsumption" || building.Manager.effect2Type == "NoConsumption"))
        {
            multiplier = 0;
        }
        
        if (building.inputResourceButton1.gameObject.activeSelf)
        {

            cost = building.inputResourceButton1.requiredAmount * building.Level * (int)(multiplier * 100) / 100;
            if (!GameManager.instance.resources.ContainsKey(building.inputResourceButton1.resource) || 
                cost > GameManager.instance.resources[building.inputResourceButton1.resource])
            {
                canStart = false;
            }
        }
        if (building.inputResourceButton2.gameObject.activeSelf)
        {
            cost = building.inputResourceButton2.requiredAmount * building.Level * (int)(multiplier * 100) / 100;
            if (!GameManager.instance.resources.ContainsKey(building.inputResourceButton2.resource) ||
                cost > GameManager.instance.resources[building.inputResourceButton2.resource])
            {
                canStart = false;
            }
        }
        if (building.inputResourceButton3.gameObject.activeSelf)
        {
            cost = building.inputResourceButton3.requiredAmount * building.Level * (int)(multiplier * 100) / 100;
            if (!GameManager.instance.resources.ContainsKey(building.inputResourceButton3.resource) ||
                cost > GameManager.instance.resources[building.inputResourceButton3.resource])
            {
                canStart = false;
            }
        }
        return canStart;
    }

    private void StartNextProduction()
    {
        producing = true;
        if (building.inputResourceButton1.gameObject.activeSelf)
        {
            GameManager.instance.SubtractResources(building.inputResourceButton1.resource, building.inputResourceButton1.requiredAmount * building.Level);
        }
        if (building.inputResourceButton2.gameObject.activeSelf)
        {
            GameManager.instance.SubtractResources(building.inputResourceButton2.resource, building.inputResourceButton2.requiredAmount * building.Level);
        }
        if (building.inputResourceButton3.gameObject.activeSelf)
        {
            GameManager.instance.SubtractResources(building.inputResourceButton3.resource, building.inputResourceButton3.requiredAmount * building.Level);
        }
    }

    public void Unlock()
    {
        InvokeRepeating("Tick", 1.0f, 1.0f);
    }
}
