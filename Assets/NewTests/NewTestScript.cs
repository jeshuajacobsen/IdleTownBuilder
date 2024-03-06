using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System;
using System.Numerics;

public class CityResearchTests
{

    GameManager gameManager;
    TimeManager timeManager;
    ResearchManager researchManager;
    private AsyncOperation unloadOperation;

    [SetUp]
    public void SetUp()
    {
        Environment.SetEnvironmentVariable("RUNNING_TESTS", "true");
    }

    [UnitySetUp]
    public IEnumerator UnitySetUp()
    {

        SceneManager.LoadScene("Main", LoadSceneMode.Single);

        yield return null; 
        timeManager = GameObject.Find("TimeManager").transform.GetComponent<TimeManager>();
        //timeManager.Load();
        gameManager = GameObject.Find("GameManager").transform.GetComponent<GameManager>();
        researchManager = GameObject.Find("ResearchManager").transform.GetComponent<ResearchManager>();
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        if (gameManager != null)
        {
            GameObject.DestroyImmediate(gameManager.gameObject);
        }
        if (timeManager != null)
        {
            GameObject.DestroyImmediate(timeManager.gameObject);
        }
        if (researchManager != null)
        {
            GameObject.DestroyImmediate(researchManager.gameObject);
        }
        SceneManager.UnloadScene("Main");
        if (unloadOperation != null)
        {
            yield return new WaitUntil(() => unloadOperation.isDone);
        }
        Environment.SetEnvironmentVariable("RUNNING_TESTS", "false");
        // Note: You cannot yield return here to wait for it to complete
    }

    

    [UnityTest]
    public IEnumerator TappingResearchWorks()
    {
        yield return null;

        researchManager.CityResearchUpgrade("Tapping");

        gameManager.buildingContent.buildings[0].outputResourceButton.Tick(false);
        double time = gameManager.productionTimers["Wheat"];
        gameManager.buildingContent.buildings[0].outputResourceButton.Tick(true);
        //yield return null;

        Assert.AreEqual(time + 1.2, gameManager.productionTimers["Wheat"], .1);
        
    }

        [UnityTest]
    public IEnumerator AdvancedTappingResearchWorks()
    {
        yield return null;

        researchManager.CityResearchUpgrade("Advanced Tapping");

        gameManager.buildingContent.buildings[0].outputResourceButton.Tick(false);
        double time = gameManager.productionTimers["Wheat"];
        gameManager.buildingContent.buildings[0].outputResourceButton.Tick(true);
        //yield return null;

        Assert.AreEqual(time + 1.2, gameManager.productionTimers["Wheat"], .1);
        
    }

        [UnityTest]
    public IEnumerator ExpertTappingResearchWorks()
    {
        yield return null;

        researchManager.CityResearchUpgrade("Expert Tapping");

        gameManager.buildingContent.buildings[0].outputResourceButton.Tick(false);
        double time = gameManager.productionTimers["Wheat"];
        gameManager.buildingContent.buildings[0].outputResourceButton.Tick(true);
        //yield return null;

        Assert.AreEqual(time + 1.2, gameManager.productionTimers["Wheat"], .1);
        
    }

    [UnityTest]
    public IEnumerator ForagingResearchWorks()
    {
        yield return null;
        
        Demographic demographic = gameManager.popContent.demographics.Find((Demographic demo) => {return demo.Name == "Peasants";});
        Requirement requirement = demographic.transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().requirements.Find((Requirement req) => {return req.resource == "Wheat";});
        Assert.AreEqual(new BigInteger(30), requirement.Cost);

        researchManager.CityResearchUpgrade("Foraging");

        Assert.AreEqual(new BigInteger(27), requirement.Cost);
    }

    [UnityTest]
    public IEnumerator FastCropsResearchWorks()
    {
        yield return null;

        gameManager.buildingContent.buildings[0].outputResourceButton.Tick(false);
        double time = gameManager.productionTimers["Wheat"];
        researchManager.CityResearchUpgrade("Fast Crops");
        gameManager.buildingContent.buildings[0].outputResourceButton.Tick(false);
        //yield return null;

        Assert.AreEqual(time + 1.1, gameManager.productionTimers["Wheat"], .05);
        
    }

    [UnityTest]
    public IEnumerator MiningResearchWorks()
    {
        yield return null;

        researchManager.BuildingResearchUpgrade("Copper Mine");

        Building mine = gameManager.buildingContent.buildings.Find((building) => {return building.buildingName == "Copper Mine";});
        mine.Level = 10;
        Assert.AreEqual(10, mine.GetProductionQuantity());

        researchManager.CityResearchUpgrade("Mining");
        

        Assert.AreEqual(11, mine.GetProductionQuantity());
        
    }

    [UnityTest]
    public IEnumerator GatheringResearchWorks()
    {
        yield return null;

        researchManager.BuildingResearchUpgrade("Reef");

        Building reef = gameManager.buildingContent.buildings.Find((building) => {return building.buildingName == "Reef";});
        reef.Level = 10;
        Assert.AreEqual(10, reef.GetProductionQuantity());

        researchManager.CityResearchUpgrade("Gathering");
        

        Assert.AreEqual(11, reef.GetProductionQuantity());
        
    }

    [UnityTest]
    public IEnumerator ProcessingResearchWorks()
    {
        yield return null;

        researchManager.BuildingResearchUpgrade("Butcher");

        Building butcher = gameManager.buildingContent.buildings.Find((building) => {return building.buildingName == "Butcher";});
        butcher.Level = 10;
        Assert.AreEqual(10, butcher.GetProductionQuantity());

        researchManager.CityResearchUpgrade("Processing");
        

        Assert.AreEqual(11, butcher.GetProductionQuantity());
        
    }

    [UnityTest]
    public IEnumerator CraftingResearchWorks()
    {
        yield return null;

        researchManager.BuildingResearchUpgrade("Bakery");

        Building building = gameManager.buildingContent.buildings.Find((building) => {return building.buildingName == "Bakery";});
        building.Level = 10;
        Assert.AreEqual(10, building.GetProductionQuantity());

        researchManager.CityResearchUpgrade("Crafting");
        

        Assert.AreEqual(11, building.GetProductionQuantity());
        
    }

    [UnityTest]
    public IEnumerator FarmResearchWorks()
    {
        yield return null;

        researchManager.BuildingResearchUpgrade("Pasture");

        Building building = gameManager.buildingContent.buildings.Find((building) => {return building.buildingName == "Pasture";});
        building.Level = 10;
        Assert.AreEqual(10, building.GetProductionQuantity());

        researchManager.CityResearchUpgrade("Farming");
        

        Assert.AreEqual(11, building.GetProductionQuantity());
        
    }
    [UnityTest]
    public IEnumerator ManufacturingResearchWorks()
    {
        yield return null;

        researchManager.BuildingResearchUpgrade("Barrel Maker");

        Building building = gameManager.buildingContent.buildings.Find((building) => {return building.buildingName == "Barrel Maker";});
        building.Level = 10;
        Assert.AreEqual(10, building.GetProductionQuantity());

        researchManager.CityResearchUpgrade("Manufacturing");
        

        Assert.AreEqual(11, building.GetProductionQuantity());
        
    }

    [UnityTest]
    public IEnumerator JewelerResearchWorks()
    {
        yield return null;

        researchManager.BuildingResearchUpgrade("Human Jeweler");

        Building building = gameManager.buildingContent.buildings.Find((building) => {return building.buildingName == "Human Jeweler";});
        building.Level = 10;
        Assert.AreEqual(10, building.GetProductionQuantity());

        researchManager.CityResearchUpgrade("Jeweler");
        

        Assert.AreEqual(11, building.GetProductionQuantity());
        
    }

    [UnityTest]
    public IEnumerator MagicResearchWorks()
    {
        yield return null;

        researchManager.BuildingResearchUpgrade("Wizard University");

        Building building = gameManager.buildingContent.buildings.Find((building) => {return building.buildingName == "Wizard University";});
        building.Level = 10;
        Assert.AreEqual(10, building.GetProductionQuantity());

        researchManager.CityResearchUpgrade("Magic");
        

        Assert.AreEqual(11, building.GetProductionQuantity());
        
    }

    [UnityTest]
    public IEnumerator GleaningResearchWorks()
    {
        yield return null;
        
        Demographic demographic = gameManager.popContent.demographics.Find((Demographic demo) => {return demo.Name == "Commoners";});
        Requirement requirement = demographic.transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().requirements.Find((Requirement req) => {return req.resource == "Vegetables";});
        Assert.AreEqual(new BigInteger(5), requirement.Cost);

        researchManager.CityResearchUpgrade("Gleaning");
        researchManager.CityResearchUpgrade("Gleaning");

        Assert.AreEqual(new BigInteger(4), requirement.Cost);
        
    }

    [UnityTest]
    public IEnumerator HousingResearchWorks()
    {
        yield return null;
        
        Demographic demographic = gameManager.popContent.demographics.Find((Demographic demo) => {return demo.Name == "Peasants";});
        demographic.CapacityLevel = 10;
        Assert.AreEqual(new BigInteger(10), demographic.CalculateCapacity(false));
        
        researchManager.CityResearchUpgrade("Housing");

        Assert.AreEqual(new BigInteger(11), demographic.CalculateCapacity(false));
        
    }

    [UnityTest]
    public IEnumerator NeighborhoodResearchWorks()
    {
        yield return null;
        gameManager.popContent.AddDemographic("Druids");
        Demographic demographic = gameManager.popContent.demographics.Find((Demographic demo) => {return demo.Name == "Druids";});
        demographic.CapacityLevel = 10;
        Assert.AreEqual(new BigInteger(10), demographic.CalculateCapacity(false));
        
        researchManager.CityResearchUpgrade("Neighborhood");

        Assert.AreEqual(new BigInteger(11), demographic.CalculateCapacity(false));
        
    }

    [UnityTest]
    public IEnumerator EstatesResearchWorks()
    {
        yield return null;
        gameManager.popContent.AddDemographic("Wizards");
        Demographic demographic = gameManager.popContent.demographics.Find((Demographic demo) => {return demo.Name == "Wizards";});
        demographic.CapacityLevel = 10;
        Assert.AreEqual(new BigInteger(10), demographic.CalculateCapacity(false));
        
        researchManager.CityResearchUpgrade("Estates");

        Assert.AreEqual(new BigInteger(11), demographic.CalculateCapacity(false));
        
    }

    [UnityTest]
    public IEnumerator ImmigrationResearchWorks()
    {
        yield return null;
        gameManager.popContent.AddDemographic("Commoners");
        Demographic demographic = gameManager.popContent.demographics.Find((Demographic demo) => {return demo.Name == "Commoners";});
        demographic.GrowthLevel = 10;
        Assert.AreEqual(new BigInteger(10), demographic.CalculateGrowth(false));
        
        researchManager.CityResearchUpgrade("Immigration");

        Assert.AreEqual(new BigInteger(11), demographic.CalculateGrowth(false));
        
    }

    [UnityTest]
    public IEnumerator ChildCareResearchWorks()
    {
        yield return null;
        gameManager.popContent.AddDemographic("Patricians");
        Demographic demographic = gameManager.popContent.demographics.Find((Demographic demo) => {return demo.Name == "Patricians";});
        demographic.GrowthLevel = 10;
        Assert.AreEqual(new BigInteger(10), demographic.CalculateGrowth(false));
        
        researchManager.CityResearchUpgrade("Child Care");

        Assert.AreEqual(new BigInteger(11), demographic.CalculateGrowth(false));
        
    }

    [UnityTest]
    public IEnumerator FertilityRitesResearchWorks()
    {
        yield return null;
        gameManager.popContent.AddDemographic("Nobles");
        Demographic demographic = gameManager.popContent.demographics.Find((Demographic demo) => {return demo.Name == "Nobles";});
        demographic.GrowthLevel = 10;
        Assert.AreEqual(new BigInteger(10), demographic.CalculateGrowth(false));
        
        researchManager.CityResearchUpgrade("Fertility Rites");

        Assert.AreEqual(new BigInteger(11), demographic.CalculateGrowth(false));
        
    }

    [UnityTest]
    public IEnumerator WellsResearchWorks()
    {
        yield return null;
        gameManager.popContent.AddDemographic("Peasants");
        Demographic demographic = gameManager.popContent.demographics.Find((Demographic demo) => {return demo.Name == "Peasants";});
        gameManager.resources["Wheat"] = new BigInteger(100);
        ConsumptionPanel consumptionPanel = demographic.transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>();
        Assert.AreEqual(50, consumptionPanel.CalculateHappiness());
        
        researchManager.CityResearchUpgrade("Wells");

        Assert.AreEqual(55,consumptionPanel.CalculateHappiness());
        
    }

    [UnityTest]
    public IEnumerator FountainsResearchWorks()
    {
        yield return null;
        gameManager.popContent.AddDemographic("Tradesmen");
        Demographic demographic = gameManager.popContent.demographics.Find((Demographic demo) => {return demo.Name == "Tradesmen";});
        gameManager.resources["Clothes"] = new BigInteger(100);
        gameManager.resources["Fruit"] = new BigInteger(100);
        ConsumptionPanel consumptionPanel = demographic.transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>();
        Assert.AreEqual(50, consumptionPanel.CalculateHappiness());
        
        researchManager.CityResearchUpgrade("Fountains");

        Assert.AreEqual(55,consumptionPanel.CalculateHappiness());
        
    }

    [UnityTest]
    public IEnumerator BathsResearchWorks()
    {
        yield return null;
        gameManager.popContent.AddDemographic("Nobles");
        Demographic demographic = gameManager.popContent.demographics.Find((Demographic demo) => {return demo.Name == "Nobles";});
        gameManager.resources["Beef"] = new BigInteger(100);
        gameManager.resources["Cake"] = new BigInteger(100);
        ConsumptionPanel consumptionPanel = demographic.transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>();
        Assert.AreEqual(50, consumptionPanel.CalculateHappiness());
        
        researchManager.CityResearchUpgrade("Baths");

        Assert.AreEqual(55, consumptionPanel.CalculateHappiness());
        
    }

    [UnityTest]
    public IEnumerator FestivalResearchWorks()
    {
        yield return null;
        Assert.AreEqual(new BigInteger(0), gameManager.CityPrestige);
        
        researchManager.CityResearchUpgrade("Festival");

        Assert.AreEqual(new BigInteger(20), gameManager.CityPrestige);
    }

    [UnityTest]
    public IEnumerator ArchitectureResearchWorks()
    {
        yield return null;
        Building building = gameManager.buildingContent.buildings.Find((building) => {return building.buildingName == "Farm";});
        building.Level = 3;
        Assert.AreEqual(new BigInteger(16), building.CalculateCost());
        
        researchManager.CityResearchUpgrade("Architecture");

        Assert.AreEqual(new BigInteger(14), building.CalculateCost());
    }

    [UnityTest]
    public IEnumerator TaxationResearchWorks()
    {
        yield return null;
        Demographic demographic = gameManager.popContent.demographics.Find((Demographic demo) => {return demo.Name == "Peasants";});
        Assert.AreEqual(new BigInteger(5), GameManager.instance.Coins);
        gameManager.resources["Wheat"] = new BigInteger(100);
        researchManager.CityResearchUpgrade("Taxation");
        demographic.transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().requirements.Find((Requirement req) => {return req.resource == "Wheat";}).ConsumeResource();
        
        Assert.AreEqual(new BigInteger(8), GameManager.instance.Coins);
    }

    [UnityTest]
    public IEnumerator TaxationResearchDoesntAffectHighTier()
    {
        yield return null;
        gameManager.popContent.AddDemographic("Wizards");
        Demographic demographic = gameManager.popContent.demographics.Find((Demographic demo) => {return demo.Name == "Wizards";});
        Assert.AreEqual(new BigInteger(5), GameManager.instance.Coins);
        gameManager.resources["Milk"] = new BigInteger(100);
        researchManager.CityResearchUpgrade("Taxation");
        demographic.transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().requirements.Find((Requirement req) => {return req.resource == "Milk";}).ConsumeResource();
        
        Assert.AreEqual(new BigInteger(5), GameManager.instance.Coins);
    }

    [UnityTest]
    public IEnumerator LuxuryTaxResearchWorks()
    {
        yield return null;
        gameManager.popContent.AddDemographic("Wizards");
        Demographic demographic = gameManager.popContent.demographics.Find((Demographic demo) => {return demo.Name == "Wizards";});
        Assert.AreEqual(new BigInteger(5), GameManager.instance.Coins);
        gameManager.resources["Milk"] = new BigInteger(100);
        researchManager.CityResearchUpgrade("Luxury Tax");
        demographic.transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().requirements.Find((Requirement req) => {return req.resource == "Milk";}).ConsumeResource();
        
        Assert.AreEqual(5 + gameManager.resourcePrices["Milk"] * 20 * 10 / 100, GameManager.instance.Coins);
    }
}
