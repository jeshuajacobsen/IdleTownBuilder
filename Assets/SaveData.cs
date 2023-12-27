using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SaveData
{
    //game
    public int coins = 0;
    public int cityPrestige = 0;
    public int collectedPrestige = 0;

    public string cityName;
    public Dictionary<string, int> resources;

    //research
    public Dictionary<string, float> multipliers;
    public Dictionary<string, int> buildingResearchLevels;
    
    //building content
    public Dictionary<string, int> buildingLevels;

    public Dictionary<string, int> scienceResearchLevels;
    public Dictionary<string, int> prestigeResearchLevels;

    public void SetBuildingSaveData(List<Building> buildings)
    {
        buildingLevels = new Dictionary<string, int>();
        foreach(Building building in buildings)
        {
            buildingLevels.Add(building.buildingName, building.Level);
        }
    }

    //market content
    public Dictionary<string, bool> autosellSettings;

    //population content

}