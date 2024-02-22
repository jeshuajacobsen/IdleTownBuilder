using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System;

public class NewTestScript
{

    GameManager gameManager;

    [SetUp]
    public void SetUp()
    {
        Environment.SetEnvironmentVariable("RUNNING_TESTS", "true");
    }

    [UnitySetUp]
    public IEnumerator UnitySetUp()
    {
        yield return SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        yield return null; 
        TimeManager timeManager = GameObject.Find("TimeManager").transform.GetComponent<TimeManager>();
        //timeManager.Load();
        gameManager = GameObject.Find("GameManager").transform.GetComponent<GameManager>();
    }

    

    [UnityTest]
    public IEnumerator NewGameInitialization()
    {
        yield return null;

        ResearchManager researchManager = GameObject.Find("ResearchManager").transform.GetComponent<ResearchManager>();
        
        researchManager.CityResearchUpgrade("Tapping");

        double time = gameManager.productionTimers["Wheat"];
        gameManager.buildingContent.buildings[0].outputResourceButton.Tick(true);
        //yield return null;

        Assert.AreEqual(time + 1.2, gameManager.productionTimers["Wheat"]);
        
        Assert.AreEqual(6, gameManager.buildingContent.buildings.Count);
    }


    [UnityTest]
    public IEnumerator BuildingResearchWorks()
    {
        //yield return SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
        ResearchManager researchManager = GameObject.Find("ResearchManager").transform.GetComponent<ResearchManager>();
        
        Assert.AreEqual(1, gameManager.buildingContent.buildings[0].GetProductionQuantity());

        gameManager.buildingContent.buildings[0].Level = 10;

        Assert.AreEqual(11, gameManager.buildingContent.buildings[0].GetProductionQuantity());
    }

    
}
