using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Rendering;
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
        for (int i = 0; i < researchTiers.Count; i++)
        {
            if (researchTiers[i].researchedCount >= 5)
            {
                ResearchTier researchTier = researchTiers.Find((tier) => tier.tier == researchTiers[i].tier + 1);
                researchTier?.unlock();
            } 
            else if (researchTiers[i].researchedCount < 5)
            {
                ResearchTier researchTier = researchTiers.Find((tier) => tier.tier == researchTiers[i].tier - 1);
                if (researchTier != null)
                {
                    researchTier.transform.Find("LockPanel").Find("UnlockRequirementText").GetComponent<TextMeshProUGUI>().text = "Research tier " + researchTier.tier + " to unlock";
                    researchTier.transform.Find("LockPanel").Find("UnlockProgressText").GetComponent<TextMeshProUGUI>().text = researchTier.researchedCount + "/" + 5;
                }
                if (researchTier?.researchedCount >= 5)
                {
                    researchTiers[0].unlock();
                }
                
            }
        }
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
            tier.InitValues(i, raceName);
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
