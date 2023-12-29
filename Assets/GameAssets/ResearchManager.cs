using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResearchManager : MonoBehaviour
{

    public static ResearchManager instance;

    public float peasantWheatDecrease = 0;
    public Dictionary<string, float> multipliers = new Dictionary<string, float>();
    public Dictionary<string, int> buildingResearchLevels = new Dictionary<string, int>();

    public Dictionary<string, int> prestigeResearchLevels = new Dictionary<string, int>();

    public Dictionary<string, int> scienceResearchLevels = new Dictionary<string, int>();

    public UnityEvent<string> addBuilding;
    public UnityEvent<string, int> setResearch;
    public UnityEvent<string, int> setScienceResearch;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CityResearchUpgrade(string upgradeTitle)
    {
        if (multipliers.ContainsKey(upgradeTitle)) {
            multipliers[upgradeTitle] += .1f;
            scienceResearchLevels[upgradeTitle] += 1;
            
        } else {
            multipliers.Add(upgradeTitle, .1f);
            scienceResearchLevels.Add(upgradeTitle, 1);
        }
    }

    public void PrestigeResearchUpgrade(string upgradeTitle)
    {
        if (multipliers.ContainsKey(upgradeTitle)) {
            multipliers[upgradeTitle] += .1f;
            prestigeResearchLevels[upgradeTitle] += 1;
            
        } else {
            multipliers.Add(upgradeTitle, .1f);
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
        saveData.multipliers = multipliers;
        saveData.buildingResearchLevels = buildingResearchLevels;
        saveData.prestigeResearchLevels = prestigeResearchLevels;
        saveData.scienceResearchLevels = scienceResearchLevels;
    }

    public void LoadSavedData(SaveData saveData)
    {
        multipliers = saveData.multipliers;
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
    }
}
