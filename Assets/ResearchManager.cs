using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResearchManager : MonoBehaviour
{

    public static ResearchManager instance;

    public float farmMultiplier = 0;
    public float woodMultiplier = 0;
    public float incomeMultiplier = 0;
    public float farmCostMultiplier = 0;
    public float peasentPrestigeMultiplier = 0;
    public float farmSpeedUp = 0;
    public float peasantWheatDecrease = 0;
    public float humanTechMultiplier = 0;

    public Dictionary<string, int> buildingLevels = new Dictionary<string, int>();

    public UnityEvent<string> addBuilding;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            BuildingUpgrade("Farm");
            BuildingUpgrade("Forester");
            BuildingUpgrade("Clay Pit");
            BuildingUpgrade("Lumber Mill");
            BuildingUpgrade("Potter");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade(string upgradeTitle)
    {
        switch (upgradeTitle)
        {
            case "Farming":
                farmMultiplier += .1f;
                break;
            case "Forestry":
                woodMultiplier += .1f;
                break;
            case "Market":
                incomeMultiplier += .1f;
                break;
            case "Fertilizer":
                farmCostMultiplier -= .1f;
                break;
            case "Peasentry":
                peasentPrestigeMultiplier += .1f;
                break;
            case "Fast Crops":
                farmSpeedUp += .2f;
                break;
            case "Foraging":
                peasantWheatDecrease += 1;
                break;
            case "Chopping":
                woodMultiplier += .1f;
                break;
            case "Human Tech":
                humanTechMultiplier += .1f;
                break;
        }
    }

    public void BuildingUpgrade(string upgradeTitle) 
    {
        if (buildingLevels.ContainsKey(upgradeTitle)) {
            buildingLevels[upgradeTitle] += 1;
        } else {
            buildingLevels.Add(upgradeTitle, 1);
            addBuilding.Invoke(upgradeTitle);
        }
    }
}
