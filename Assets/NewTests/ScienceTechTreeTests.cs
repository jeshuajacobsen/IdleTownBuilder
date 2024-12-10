using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System;
using System.Numerics;
using TMPro;
using UnityEngine.UI;

public class ScienceTechTreeTests
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
    }

    [UnityTest]
    public IEnumerator TestUnlockingTech()
    {
        yield return null;
        researchManager.CityResearchUpgrade("Tapping");
        gameManager.resources["Hemp"] = new BigInteger(100);
        CityResearchButton cityResearchButton = gameManager.cityResearchContent.transform.Find("CityResearchButtonTapping").GetComponent<CityResearchButton>();
        CityResearchInfoPanel cityResearchInfoPanel = cityResearchButton.transform.parent.parent.Find("SelectedResearchBackground")
            .Find("CityResearchInfoPanel").GetComponent<CityResearchInfoPanel>();
        cityResearchButton.OpenInSelectedResearch();
        bool upgradeCompleted = false;
        cityResearchButton.onUpgrade.AddListener(() => upgradeCompleted = true);
        cityResearchInfoPanel.Upgrade();
        yield return new WaitUntil(() => upgradeCompleted);
        yield return null;

        // Ensure the background image is updated after the upgrade
        var advancedTappingButton = gameManager.cityResearchContent.transform.Find("CityResearchButtonAdvancedTapping");
        advancedTappingButton.GetComponent<CityResearchButton>().UpdateResearch();
        var backgroundImage = advancedTappingButton.Find("BackgroundImage").GetComponent<Image>();
        Assert.IsNotNull(backgroundImage);
        Assert.IsFalse(backgroundImage.sprite.name.Contains("Grey"));
    }

    // [UnityTest]
    // public IEnumerator TestUnlockingTechOnLoad()
    // {
    //     yield return null;
    //     researchManager.CityResearchUpgrade("Tapping");
    //     gameManager.cityResearchContent.transform.Find("CityResearchButtonTapping").GetComponent<CityResearchButton>().Upgrade("Tapping");
    //     Assert.IsFalse(gameManager.cityResearchContent.transform.Find("CityResearchButtonAdvancedTapping").transform.Find("Mask").Find("LockedPanel").gameObject.activeSelf);
    // }
}