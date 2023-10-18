using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingContent : MonoBehaviour
{
    public Building BuildingPrefab;
    public Transform contentTransform;
    private List<Building> buildings;

    private bool starting = false;

    void Awake()
    {
        Reset("Peasantville");
    }

    // Start is called before the first frame update
    void Start()
    {
        
        GameManager.instance.resetCity.AddListener(Reset);
    }

    void Reset(string newCityName)
    {
        if (buildings != null)
        {
            foreach(Building item in buildings)
            {
                item.onProductionClick.RemoveListener(HandleProductionClick);
                Destroy(item.gameObject);
            }
        }
        
        buildings = new List<Building>();
        Building building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        building.InitValues("Farm");
        building.onProductionClick.AddListener(HandleProductionClick);
        buildings.Add(building);

        building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        building.InitValues("Forester");
        building.onProductionClick.AddListener(HandleProductionClick);
        buildings.Add(building);

        building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        building.InitValues("Clay Pit");
        building.onProductionClick.AddListener(HandleProductionClick);
        buildings.Add(building);

        building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        building.InitValues("Lumber Mill");
        building.onProductionClick.AddListener(HandleProductionClick);
        buildings.Add(building);

        building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        building.InitValues("Potter");
        building.onProductionClick.AddListener(HandleProductionClick);
        buildings.Add(building);

        building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        building.InitValues("Vegetable Farm");
        building.onProductionClick.AddListener(HandleProductionClick);
        buildings.Add(building);

        building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        building.InitValues("Wind Mill");
        building.onProductionClick.AddListener(HandleProductionClick);
        buildings.Add(building);

        building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        building.InitValues("Copper Mine");
        building.onProductionClick.AddListener(HandleProductionClick);
        buildings.Add(building);

        building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        building.InitValues("Tin Mine");
        building.onProductionClick.AddListener(HandleProductionClick);
        buildings.Add(building);

        building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        building.InitValues("Bakery");
        building.onProductionClick.AddListener(HandleProductionClick);
        buildings.Add(building);

        building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        building.InitValues("Smelter");
        building.onProductionClick.AddListener(HandleProductionClick);
        buildings.Add(building);

        building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        building.InitValues("Furniture Factory");
        building.onProductionClick.AddListener(HandleProductionClick);
        buildings.Add(building);
        

        starting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (starting)
        {
            foreach (Building building in buildings)
            {
                building.onProductionClick.RemoveListener(HandleProductionClick);
                building.onProductionClick.AddListener(HandleProductionClick);
            }
            starting = false;
        }
    }

    void HandleProductionClick(string resource)
    {
        foreach (Building currentBuilding in buildings)
        {
            if (currentBuilding.GetOutputResource().resource == resource)
            {
                currentBuilding.HandleProductionClick();
            }
        }
    }

    public void TickAll()
    {
        if (buildings != null)
        {
            foreach (Building currentBuilding in buildings)
            {
                if (!currentBuilding.locked)
                {
                    currentBuilding.Tick();
                }
            }
        }
    }

    void HandleTimeAway(int secondsAway)
    {
        // Dictionary<string, List<ProductionInput>> resourceDependencyCounts = new Dictionary<string, List<ProductionInput>>();
        // foreach (Building currentBuilding in buildings)
        // {
            
        //     for (int i = 0; i < 3; i++)
        //     {
        //         if (!currentBuilding.getInputResource(i).locked)
        //         {
        //             string resourceName = currentBuilding.GetInputResource(i).resource;
        //             if (resourceDependencyCounts.ContainsKey(resourceName))
        //             {
        //                 resourceDependencyCounts[resourceName].Add(currentBuilding.GetInputResource(i));
        //                 //onResourcesChanged.Invoke(resourceName, resources[resourceName]);
        //             } else {
        //                 List<ProductionInput> list = new List<ProductionInput>();
        //                 list.Add(currentBuilding.GetInputResource(i));
        //                 resourceDependencyCounts.Add(resourceName, list);
        //                 //onResourcesAdded.Invoke(resourceName, currentBuilding.getInputResource(1).requiredAmount);
        //             }
        //         }
        //     }
        // }

        // foreach (Building currentBuilding in buildings)
        // {
        //     if (!currentBuilding.locked)
        //     {
        //         int amountToAdd = currentBuilding.GetProductionQuantity() * (secondsAway / currentBuilding.GetOutputResource().requiredTime);
        //         for (int i = 0; i < 3; i++)
        //         {
        //             if (!currentBuilding.getInputResource(i).locked)
        //             {
        //                 string resourceName = currentBuilding.GetInputResource(i).resource;
        //                 List<ProductionInput> inputs = resourceDependencyCounts[resourceName];
        //                 int requiredInput = 0
        //                 foreach (ProductionInput input in inputs)
        //                 {
        //                     requiredInput += input.GetResourcesConsumed();
        //                 }
        //                 amountToAdd = Math.Min(amountToAdd, currentBuilding.GetInputResource(i).GetResourcesConsumed() / requiredInput)
        //             }
        //         }
        //         GameManager.instance.SubtractResources(currentBuilding.GetInputResource().resource, currentBuilding.GetInputResource(i).get)
        //         GameManager.instance.AddResources(currentBuilding.GetOutputResource().resource, amountToAdd);
        //     }
        // }
    }
}
