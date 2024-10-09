using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
using System.Transactions;
using System;
using UnityEngine.Events;

public class Demographic : MonoBehaviour, Unlockable
{
    [SerializeField] private TextMeshProUGUI nameText;
    public string Name
    {
        get { return nameText.text; }
        set 
        { 
            nameText.text = value;
        }
    }
    private BigInteger baseCost = 1;
    private BigInteger basePrestigeGenerated;
    public string race = "Human";
    public Canvas canvas;
    public int tier;

    private double happiness = 1;
    public double Happiness
    {
        get { return happiness; }
        set 
        { 
            happiness = value;
        }
    }
    private int capacityLevel = 1;
    public int CapacityLevel
    {
        get { return capacityLevel; }
        set 
        { 
            capacityLevel = value;
            transform.Find("CapacityPanel").Find("CapacityText").GetComponent<TextMeshProUGUI>().text = GameManager.BigIntToExponentString(CalculateCapacity(false)) + " -> " + GameManager.BigIntToExponentString(CalculateCapacity(true));
            transform.Find("CapacityPanel").Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "$" + GameManager.BigIntToExponentString(CalculateCapacityCost());
        }
    }
    private int growthLevel = 1;

    public int GrowthLevel
    {
        get { return growthLevel; }
        set 
        { 
            growthLevel = value;
            transform.Find("GrowthPanel").Find("GrowthText").GetComponent<TextMeshProUGUI>().text = GameManager.BigIntToExponentString(CalculateGrowth(false)) + " -> " + GameManager.BigIntToExponentString(CalculateGrowth(true));
            transform.Find("GrowthPanel").Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "$" + GameManager.BigIntToExponentString(CalculateGrowthCost());
        }
    }

    private BigInteger population = 0;
    public BigInteger Population
    {
        get { return population; }
        set 
        { 
            //TODO - this is a hack to get the requirements to update.  Need to refactor
            foreach (Requirement requirement in transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().requirements)
            {
                requirement.population = value;
            }
            population = value;
            transform.Find("PopulationPanel").Find("PopulationText").GetComponent<TextMeshProUGUI>().text = "Pop: " + population.ToString();
        }
    }

    private double popGrowthPercentComplete = 0;

    public double PopGrowthPercentComplete
    {
        get { return popGrowthPercentComplete; }
        set 
        { 
            popGrowthPercentComplete = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("DemoImage").GetComponent<Image>().sprite = SpriteManager.instance.GetDemoSprite(nameText.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitValues(string newName)
    {
        GrowthLevel = 1;
        CapacityLevel = 1;
        Population = 0;
        Name = newName;
        ConsumptionPanel consumptionPanel = transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>();

        DemographicData demographicData = GameManager.instance.gameData.GetDemographicData(newName);
        baseCost = demographicData.baseCost;
        basePrestigeGenerated = demographicData.basePrestigeGenerated;
        race = demographicData.race;
        tier = demographicData.tier;
        foreach (KeyValuePair<string, int> requirement in demographicData.requirements)
        {
            consumptionPanel.AddRequirement(requirement.Key, requirement.Value);
        }
    }

    public void GrowPopulation()
    {
        if (Population < CalculateCapacity(false) || popGrowthPercentComplete <= 100)
        {   
            popGrowthPercentComplete += GrowthLevel * Happiness / 100;
            if (Population < CalculateCapacity(false) && popGrowthPercentComplete >= 100)
            {
                Population += 1;
                popGrowthPercentComplete -= 100;
                TasksManager.instance.CheckTasks("PopulationGoal", Name, Population);
            } else if (popGrowthPercentComplete > 100) {
                popGrowthPercentComplete = 100;
            }
            SetPopGrowthProgress();
        }
    }

    private void SetPopGrowthProgress()
    {
        const int width = 200;
        RectTransform rectTransform = transform.Find("PopulationPanel").Find("BarFill").GetComponent<RectTransform>();
        float desiredWidth = (float)popGrowthPercentComplete / 100 * width;
        rectTransform.sizeDelta = new UnityEngine.Vector2(desiredWidth, rectTransform.sizeDelta.y);

        rectTransform.anchoredPosition = new UnityEngine.Vector2(-width / 2 + desiredWidth / 2, rectTransform.anchoredPosition.y);
    }

    public BigInteger CalculateCapacity(bool nextLevel)
    {
        int capLevel = nextLevel ? CapacityLevel + 1 : CapacityLevel;
        double multiplier = 1;
        if (race == "Human")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Human Capacity") ? ResearchManager.instance.scienceResearchLevels["Human Capacity"] * .1f : 0;
        }
        else if (race == "Merfolk")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Merfolk Capacity") ? ResearchManager.instance.scienceResearchLevels["Merfolk Capacity"] * .1f : 0;
        }
        else if (race == "Dwarf")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Dwarf Capacity") ? ResearchManager.instance.scienceResearchLevels["Dwarf Capacity"] * .1f : 0;
        }
        else if (race == "Fairy")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Fairy Capacity") ? ResearchManager.instance.scienceResearchLevels["Fairy Capacity"] * .1f : 0;
        }
        else if (race == "Elf")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Elf Capacity") ? ResearchManager.instance.scienceResearchLevels["Elf Capacity"] * .1f : 0;
        }

        if (tier == 1 || tier == 2)
        { 
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Housing") ? ResearchManager.instance.scienceResearchLevels["Housing"] * .1f : 0;
        } 
        else if (tier == 3 || tier == 4)
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Neighborhood") ? ResearchManager.instance.scienceResearchLevels["Neighborhood"] * .1f : 0;
        }
        else if (tier == 5 || tier == 6)
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Estates") ? ResearchManager.instance.scienceResearchLevels["Estates"] * .1f : 0;
        }

        multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Population Capacity") ? ResearchManager.instance.prestigeResearchLevels["Population Capacity"] * .05f : 0;

        return capLevel * (int)(100 * multiplier) / 100;
    }

    public BigInteger CalculateGrowth(bool nextLevel)
    {
        int growLevel = nextLevel ? GrowthLevel + 1 : GrowthLevel;
        double multiplier = 1;
        if (race == "Human")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Human Growth") ? ResearchManager.instance.scienceResearchLevels["Human Growth"] * .1f : 0;
        }
        else if (race == "Merfolk")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Merfolk Growth") ? ResearchManager.instance.scienceResearchLevels["Merfolk Growth"] * .1f : 0;
        }
        else if (race == "Dwarf")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Dwarf Growth") ? ResearchManager.instance.scienceResearchLevels["Dwarf Growth"] * .1f : 0;
        }
        else if (race == "Fairy")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Fairy Growth") ? ResearchManager.instance.scienceResearchLevels["Fairy Growth"] * .1f : 0;
        }
        else if (race == "Elf")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Elf Growth") ? ResearchManager.instance.scienceResearchLevels["Elf Growth"] * .1f : 0;
        }

        if (tier == 1 || tier == 2)
        { 
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Immigration") ? ResearchManager.instance.scienceResearchLevels["Immigration"] * .1f : 0;
        } 
        else if (tier == 3 || tier == 4)
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Child Care") ? ResearchManager.instance.scienceResearchLevels["Child Care"] * .1f : 0;
        }
        else if (tier == 5 || tier == 6)
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Fertility Rites") ? ResearchManager.instance.scienceResearchLevels["Fertility Rites"] * .1f : 0;
        }

        multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Population Growth") ? ResearchManager.instance.prestigeResearchLevels["Population Growth"] * .05f : 0;

        return growLevel * (int)(100 * multiplier) / 100;
    }

    public BigInteger CalculateCapacityCost()
    {
         return GameManager.GrowthFunction(capacityLevel, baseCost);;
    }

    public BigInteger CalculateGrowthCost()
    {
        return GameManager.GrowthFunction(growthLevel, baseCost);
    }

    public void GrowthLevelUp()
    {
        BigInteger cost = CalculateGrowthCost();
        if (GameManager.instance.HasEnoughCoin(cost))
        {
            GrowthLevel = GrowthLevel + 1;
            GameManager.instance.SubtractCoins(cost);
        }
    }

    public void CapacityLevelUp()
    {
        BigInteger cost = CalculateCapacityCost();
        if (GameManager.instance.HasEnoughCoin(cost))
        {
            CapacityLevel = CapacityLevel + 1;
            GameManager.instance.SubtractCoins(cost);
        }
    }

    public BigInteger GetPrestigeGenerated()
    {
        double multiplier = 1;
        int addToBase = 0;
        if (nameText.text == "Peasants")
        {
            addToBase += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Peasanting") ? ResearchManager.instance.prestigeResearchLevels["Peasanting"] * 1 : 0;
        }
        return population * (basePrestigeGenerated + addToBase) * (int)(multiplier * 100) / 100;
    }

    public void Unlock()
    {
        Population = 1;
        SetPopGrowthProgress();
        GrowthLevel = 1;
        CapacityLevel = 1;
        transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().Unlock();
        transform.Find("LockedPanel").gameObject.SetActive(false);
    }

    public BigInteger GetUnlockCost()
    {
        return baseCost * 3;
    }

    public void Tick()
    {
        transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().Tick();
    }
}
