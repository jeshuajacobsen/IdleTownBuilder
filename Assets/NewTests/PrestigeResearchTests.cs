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
    public IEnumerator BuildingResearchWorks()
    {
        yield return null;
        Debug.Log(gameManager.buildingContent.buildings[0]);
        gameManager.buildingContent.buildings[0].Level = 10;
        Assert.AreEqual(10, gameManager.buildingContent.buildings[0].GetProductionQuantity());

        researchManager.BuildingResearchUpgrade("Farm");

        Assert.AreEqual(11, gameManager.buildingContent.buildings[0].GetProductionQuantity());
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
