using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PrestigeTechTree : MonoBehaviour
{
    public List<ResearchTier> researchTiers;

    [SerializeField] private ResearchTier researchTierPrefab;
    [SerializeField] private Transform contentTransform;

    public ScrollRect scrollRect;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InitValues(string raceName)
    {
        List<PrestigeResearchData> researchData = GameManager.instance.gameData.GetPrestigeResearchDataList(raceName);
        List<BuildingData> buildingData = GameManager.instance.gameData.GetBuildingDataByRace(raceName);


        int buildingIndex = 0;
        for (int i = 0; i < Math.Ceiling((double)(researchData.Count + buildingData.Count) / 8); i++)
        {
            int count = 0;
            ResearchTier tier = Instantiate(researchTierPrefab, contentTransform).GetComponent<ResearchTier>();
            List<PrestigeResearchData> tierResearchData = researchData.Where(tier => tier.tier - 1 == i).ToList();
            foreach (PrestigeResearchData data in tierResearchData)
            {
                tier.AddResearchButton(data.title);
                count++;
            }
            for (int j = count; buildingIndex < buildingData.Count; j++)
            {
                tier.AddBuildingButton(buildingData[buildingIndex].name);
                buildingIndex++;
                count++;
                if (count % 8 == 0)
                {
                    break;
                }
            }
            researchTiers.Add(tier);
            
        }
        foreach (ResearchTier tier in researchTiers.Reverse<ResearchTier>())
        {
            tier.transform.SetParent(transform.Find("Scroll View").Find("Viewport").Find("TechTreeContent"), false);
        }
        scrollRect.verticalNormalizedPosition = 0.0f;
    }
}
