using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System;
using System.Numerics;

public class NewTestScript
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
        while (unloadOperation != null && !unloadOperation.isDone)
        {
            yield return null; // Wait for the unload to complete
        }
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
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
        yield return null; 
        timeManager = GameObject.Find("TimeManager").transform.GetComponent<TimeManager>();
        //timeManager.Load();
        gameManager = GameObject.Find("GameManager").transform.GetComponent<GameManager>();
        researchManager = GameObject.Find("ResearchManager").transform.GetComponent<ResearchManager>();
    }

    [TearDown]
    public void TearDown()
    {
        var unloadOperation = SceneManager.UnloadSceneAsync("Main");
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

        Assert.AreEqual(time + 1.2, gameManager.productionTimers["Wheat"], .05);
        
    }


    [UnityTest]
    public IEnumerator BuildingResearchWorks()
    {
        yield return null;

        gameManager.buildingContent.buildings[0].Level = 10;
        Assert.AreEqual(10, gameManager.buildingContent.buildings[0].GetProductionQuantity());

        researchManager.BuildingResearchUpgrade("Farm");

        Assert.AreEqual(11, gameManager.buildingContent.buildings[0].GetProductionQuantity());
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
    public IEnumerator FertilizerResearchWorks()
    {
        yield return null;

        gameManager.buildingContent.buildings[0].Level = 20;
        BigInteger cost = gameManager.buildingContent.buildings[0].CalculateCost();

        researchManager.PrestigeResearchUpgrade("Fertilizer");

        Assert.AreEqual((cost * 90) / 100, gameManager.buildingContent.buildings[0].CalculateCost());
    }
    
}
