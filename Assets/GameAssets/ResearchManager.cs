using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;
using UnityEngine.UIElements;

public class ResearchManager : MonoBehaviour
{

    public static ResearchManager instance;

    public float peasantWheatDecrease = 0;
    public Dictionary<string, int> buildingResearchLevels = new Dictionary<string, int>();

    public Dictionary<string, int> prestigeResearchLevels = new Dictionary<string, int>();

    public Dictionary<string, int> scienceResearchLevels = new Dictionary<string, int>();

    public UnityEvent<string> addBuilding;
    public UnityEvent<string, int> setResearch;
    public UnityEvent<string, int> setScienceResearch = new UnityEvent<string, int>();
    public UnityEvent resetScienceResearch = new UnityEvent();


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.resetCity.AddListener(Reset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CityResearchUpgrade(string upgradeTitle)
    {
        if (upgradeTitle == "Festival")
        {
            PopulationContent popContent = GameManager.instance.popContent;
            BuildingContent buildingContent = GameManager.instance.buildingContent;
            BigInteger gainedPrestige = 0;
            foreach (Demographic demo in popContent.demographics)
            {
                if (demo.tier == 1 || demo.tier == 2)
                {
                    gainedPrestige += 10 * demo.Population;
                }
            }
            GameManager.instance.AddCityPrestige(gainedPrestige);
        } 
        if (upgradeTitle == "Levy")
        {
            foreach (Building building in GameManager.instance.buildingContent.buildings)
            {
                GameManager.instance.AddResources(building.outputResourceButton.resource, building.GetProductionQuantity() * 5);
            }
        }
        if (upgradeTitle == "Managers")
        {
            GameManager.instance.buildingContent.unlockManagers();
        }
        if (scienceResearchLevels.ContainsKey(upgradeTitle)) {
            scienceResearchLevels[upgradeTitle] += 1;
            
        } else {
            scienceResearchLevels.Add(upgradeTitle, 1);
        }
    }

    public void PrestigeResearchUpgrade(string upgradeTitle)
    {
        
        if (prestigeResearchLevels.ContainsKey(upgradeTitle)) {
            prestigeResearchLevels[upgradeTitle] += 1;
            
        } else {
            prestigeResearchLevels.Add(upgradeTitle, 1);
        }
    }

    public void BuildingResearchUpgrade(string upgradeTitle) 
    {
        if (buildingResearchLevels.ContainsKey(upgradeTitle)) {
            buildingResearchLevels[upgradeTitle] += 1;
        } else {
            buildingResearchLevels.Add(upgradeTitle, 1);
            addBuilding.Invoke(upgradeTitle);
        }
    }

    public void PrepForSave(SaveData saveData)
    {
        saveData.buildingResearchLevels = buildingResearchLevels;
        saveData.prestigeResearchLevels = prestigeResearchLevels;
        saveData.scienceResearchLevels = scienceResearchLevels;
    }

    public void LoadSavedData(SaveData saveData)
    {
        buildingResearchLevels = saveData.buildingResearchLevels;
        prestigeResearchLevels = saveData.prestigeResearchLevels;
        scienceResearchLevels = saveData.scienceResearchLevels;
        foreach (KeyValuePair<string, int> building in buildingResearchLevels)
        {
            addBuilding.Invoke(building.Key);
            setResearch.Invoke(building.Key, building.Value);
        }
        foreach (KeyValuePair<string, int> research in prestigeResearchLevels)
        {
            setResearch.Invoke(research.Key, research.Value);
        }
        foreach (KeyValuePair<string, int> research in scienceResearchLevels)
        {
            setScienceResearch.Invoke(research.Key, research.Value);
        }
    }

    public void StartNewGame()
    {
        BuildingResearchUpgrade("Farm");
        BuildingResearchUpgrade("Forester");
        BuildingResearchUpgrade("Clay Pit");
        BuildingResearchUpgrade("Lumber Mill");
        BuildingResearchUpgrade("Potter");
        BuildingResearchUpgrade("Kelpery");
        BuildingResearchUpgrade("Mushroom Cave");
    }

    public void Reset(string newCityName)
    {
        scienceResearchLevels = new Dictionary<string, int>();
        resetScienceResearch.Invoke();
    }
}
