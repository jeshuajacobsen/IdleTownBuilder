using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System;
using System.Numerics;

public class PrestigeResearchTests
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
    public IEnumerator BuildingResearchWorks()
    {
        yield return null;
        gameManager.buildingContent.buildings[0].Level = 10;
        Assert.AreEqual(new BigInteger(10), gameManager.buildingContent.buildings[0].GetProductionQuantity());

        researchManager.BuildingResearchUpgrade("Farm");

        Assert.AreEqual(new BigInteger(11), gameManager.buildingContent.buildings[0].GetProductionQuantity());
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

    [UnityTest]
    public IEnumerator TapPowerResearchWorks()
    {
        yield return null;

        researchManager.PrestigeResearchUpgrade("Tap Power");

        gameManager.buildingContent.buildings[0].outputResourceButton.Tick(false);
        double time = gameManager.productionTimers["Wheat"];
        gameManager.buildingContent.buildings[0].outputResourceButton.Tick(true);
        //yield return null;

        Assert.AreEqual(time + 1.1, gameManager.productionTimers["Wheat"], .05);
        
    }

    [UnityTest]
    public IEnumerator MarketingResearchWorks()
    {
        yield return null;

        Assert.AreEqual(new BigInteger(5), GameManager.instance.Coins);
        gameManager.AddResources("Milk", 10);
        BigInteger price = gameManager.GetResourcePrice("Milk");
        researchManager.PrestigeResearchUpgrade("Marketing");
        
        gameManager.tabMarketContentGameObject.GetComponent<TabMarketContent>().SellResources("Milk");
        
        Assert.AreEqual(price + price * 10 / 100, gameManager.GetResourcePrice("Milk"));
        Assert.AreEqual(5 + gameManager.GetResourcePrice("Milk") * 10, GameManager.instance.Coins);
    }

    [UnityTest]
    public IEnumerator PeasantingResearchWorks()
    {
        yield return null;
        Demographic demographic = gameManager.popContent.demographics.Find((Demographic demo) => {return demo.Name == "Peasants";});
        demographic.Population = 1;
        Assert.AreEqual(new BigInteger(5), demographic.GetPrestigeGenerated());
   
        researchManager.PrestigeResearchUpgrade("Peasanting");

        Assert.AreEqual(new BigInteger(6), demographic.GetPrestigeGenerated());
    }
}
