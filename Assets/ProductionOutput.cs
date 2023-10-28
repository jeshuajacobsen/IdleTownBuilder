using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using SharpUI.Source.Common.UI.Elements.Loading;

public class ProductionOutput : MonoBehaviour
{

    private float productionTimer = 0;
    public int requiredTime = 10;
    public string resource = "";
    public UnityEvent<string> onProductionClick;
    private bool producing = false;

    public LoadingBar loadingBar;
    public Building building;

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

    public void InitValues(string newResource)
    {
        resource = newResource;
        GameManager.instance.productionTimers[newResource] = 0;
        transform.GetComponent<Image>().sprite = SpriteManager.instance.GetResourceSprite(newResource);
    }

    public void Tick()
    {
        if (producing)
        {
            productionTimer++;
            if (resource == "Wheat" || resource == "Vegetables")
            {
                productionTimer += ResearchManager.instance.farmSpeedUp;
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

        GameManager.instance.productionTimers[resource] = productionTimer; 
        loadingBar.UpdatePercentage(productionTimer / requiredTime * 100);
    }

    public void ProductionClick()
    {
        onProductionClick.Invoke(resource);
    }

    public void HandleProductionClick()
    {
        Tick();
    }

    private bool CanStartNextProduction()
    {
        bool canStart = true;
        if (building.inputResourceButton1.gameObject.activeSelf)
        {
            if (!GameManager.instance.resources.ContainsKey(building.inputResourceButton1.resource) || 
                building.inputResourceButton1.requiredAmount * building.level > GameManager.instance.resources[building.inputResourceButton1.resource])
            {
                canStart = false;
            }
        }
        if (building.inputResourceButton2.gameObject.activeSelf)
        {
            if (!GameManager.instance.resources.ContainsKey(building.inputResourceButton2.resource) ||
                building.inputResourceButton2.requiredAmount * building.level > GameManager.instance.resources[building.inputResourceButton2.resource])
            {
                canStart = false;
            }
        }
        if (building.inputResourceButton3.gameObject.activeSelf)
        {
            if (!GameManager.instance.resources.ContainsKey(building.inputResourceButton3.resource) ||
                building.inputResourceButton3.requiredAmount * building.level > GameManager.instance.resources[building.inputResourceButton3.resource])
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
            GameManager.instance.SubtractResources(building.inputResourceButton1.resource, building.inputResourceButton1.requiredAmount * building.level);
        }
        if (building.inputResourceButton2.gameObject.activeSelf)
        {
            GameManager.instance.SubtractResources(building.inputResourceButton2.resource, building.inputResourceButton2.requiredAmount * building.level);
        }
        if (building.inputResourceButton3.gameObject.activeSelf)
        {
            GameManager.instance.SubtractResources(building.inputResourceButton3.resource, building.inputResourceButton3.requiredAmount * building.level);
        }
    }

    public void Unlock()
    {
        InvokeRepeating("Tick", 1.0f, 1.0f);
    }
}
