using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System;
using System.Numerics;

public class ManagerTests
{

    GameManager gameManager;
    TimeManager timeManager;
    ResearchManager researchManager;
    private AsyncOperation unloadOperation;
    TasksManager tasksManager;

    [SetUp]
    public void SetUp()
    {
        Environment.SetEnvironmentVariable("RUNNING_TESTS", "true");
    }

    [UnitySetUp]
    public IEnumerator UnitySetUp()
    {
        Environment.SetEnvironmentVariable("RUNNING_TESTS", "true");
        SceneManager.LoadScene("Main", LoadSceneMode.Single);

        yield return null; 
        timeManager = GameObject.Find("TimeManager").transform.GetComponent<TimeManager>();
        //timeManager.Load();
        gameManager = GameObject.Find("GameManager").transform.GetComponent<GameManager>();
        researchManager = GameObject.Find("ResearchManager").transform.GetComponent<ResearchManager>();
        tasksManager = GameObject.Find("TasksManager").transform.GetComponent<TasksManager>();
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
        if (tasksManager != null)
        {
            GameObject.DestroyImmediate(tasksManager.gameObject);
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
    public IEnumerator ProductionQuantityWorks()
    {
        yield return null;

        gameManager.AddManager("Wedge");

        Building building = gameManager.buildingContent.buildings[0];
        building.Level = 10;
        Assert.AreEqual(new BigInteger(10), building.GetProductionQuantity());
        //yield return null;
        building.Manager = gameManager.managers[0];

        Assert.AreEqual(new BigInteger(11), building.GetProductionQuantity());
        
    }

    [UnityTest]
    public IEnumerator LessConsumptionWorks()
    {
        yield return null;

        gameManager.AddManager("Biggs");
        
        ResearchManager.instance.BuildingResearchUpgrade("Potter");
        Building building = gameManager.buildingContent.buildings.Find((building) => building.buildingName == "Potter");
        building.Level = 10;
        gameManager.AddResources("Clay", 9);
        Assert.IsFalse(building.outputResourceButton.CanStartNextProduction());

        building.Manager = gameManager.managers[0];

        Assert.IsTrue(building.outputResourceButton.CanStartNextProduction());
        
    }

    [UnityTest]
    public IEnumerator NoConsumptionWorks()
    {
        yield return null;

        gameManager.AddManager("Cloud");
        
        ResearchManager.instance.BuildingResearchUpgrade("Potter");
        Building building = gameManager.buildingContent.buildings.Find((building) => building.buildingName == "Potter");
        building.Level = 10;
        gameManager.AddResources("Clay", 0);
        Assert.IsFalse(building.outputResourceButton.CanStartNextProduction());

        building.Manager = gameManager.managers[0];

        Assert.IsTrue(building.outputResourceButton.CanStartNextProduction());
        
    }

    [UnityTest]
    public IEnumerator ProductionSpeedWorks()
    {
        yield return null;

        gameManager.AddManager("Barret");

        gameManager.productionTimers["Wheat"] = 0;
        gameManager.buildingContent.buildings[0].outputResourceButton.Tick(false);
        double time = gameManager.productionTimers["Wheat"];
        gameManager.buildingContent.buildings[0].Manager = gameManager.managers[0];

        gameManager.buildingContent.buildings[0].outputResourceButton.Tick(false);
        //yield return null;

        Assert.AreEqual(time + 1.1, gameManager.productionTimers["Wheat"], .05);
        
    }

    [UnityTest]
    public IEnumerator BuildingLevelUpWorks()
    {
        yield return null;

        gameManager.AddManager("Aeris");

        gameManager.buildingContent.buildings[0].Manager = gameManager.managers[0];
        gameManager.managers[0].AssignedBuilding = gameManager.buildingContent.buildings[0];

        timeManager.managerTime = 4000;
        timeManager.Tick();
        //yield return null;

        Assert.AreEqual(2, gameManager.buildingContent.buildings[0].Level);
        
    }
}