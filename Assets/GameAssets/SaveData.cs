using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using System.Numerics;

[System.Serializable]
public class SaveData
{
    //game
    public BigInteger coins = 0;
    public BigInteger gems = 0;
    public BigInteger cityPrestige = 0;
    public BigInteger collectedPrestige = 0;

    public string cityName;
    public Dictionary<string, BigInteger> resources;

    //research
    public Dictionary<string, int> buildingResearchLevels;
    
    //building content
    public Dictionary<string, int> buildingLevels;

    public Dictionary<string, int> scienceResearchLevels;
    public Dictionary<string, int> prestigeResearchLevels;

    public Dictionary<string, int> managerLevels;
    public void SetManagerSaveData(List<Manager> managers)
    {
        managerLevels = new Dictionary<string, int>();
        foreach(Manager manager in managers)
        {
            managerLevels.Add(manager.nameText.text, manager.level);
        }
    }
    public Dictionary<string, string> equippedManagers;

    public void SetBuildingSaveData(List<Building> buildings)
    {
        buildingLevels = new Dictionary<string, int>();
        equippedManagers = new Dictionary<string, string>();
        foreach(Building building in buildings)
        {
            buildingLevels.Add(building.buildingName, building.Level);
            if (building.Manager != null)
            {
                equippedManagers.Add(building.buildingName, building.Manager.nameText.text);
            }
        }
    }

    //market content
    public Dictionary<string, bool> autosellSettings;

    //population content
    public Dictionary<string, DemographicSaveData> demographicLevels;
    public void SetDemographicSaveData(List<Demographic> demos)
    {
        demographicLevels = new Dictionary<string, DemographicSaveData>();
        foreach(Demographic demo in demos)
        {
            demographicLevels.Add(demo.Name, new DemographicSaveData(demo.Name, demo.CapacityLevel, demo.GrowthLevel, demo.Population, demo.PopGrowthPercentComplete));
        }
    }

    //kingdom content
    public Dictionary<string, bool> cityOptionLocks;

    public class DemographicSaveData
    {
        public string name;
        public int capacityLevel;
        public int growthLevel;
        public BigInteger population;

        public double popGrowthPercentComplete;

        public DemographicSaveData(string name, int capacity, int growth, BigInteger population, double popGrowthPercentComplete)
        {
            this.name = name;
            capacityLevel = capacity;
            growthLevel = growth;
            this.population = population;
            this.popGrowthPercentComplete = popGrowthPercentComplete;
        }
    }
}