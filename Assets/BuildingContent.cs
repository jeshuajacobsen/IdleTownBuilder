using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingContent : MonoBehaviour
{
    public Building BuildingPrefab;
    public Transform contentTransform;
    private List<Building> buildings;

    // Start is called before the first frame update
    void Start()
    {
        buildings = new List<Building>();
        Building building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        string[] input = {};
        building.InitValues("Farm", "Wheat", input);
        building.onProductionClick.AddListener(HandleProductionClick);
        buildings.Add(building);

        building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        building.InitValues("Forester", "Wood", input);
        building.onProductionClick.AddListener(HandleProductionClick);
        buildings.Add(building);

        building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        input = new string[] {"Wood"};
        building.InitValues("Lumber Mill", "Lumber", input);
        building.onProductionClick.AddListener(HandleProductionClick);
        buildings.Add(building);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleProductionClick(string resource)
    {
        foreach (Building currentBuilding in buildings)
        {
            if (currentBuilding.getOutputResource() == resource)
            {
                currentBuilding.HandleProductionClick();
            }
        }
    }
}
