using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System;
using System.Numerics;
using System.IO;
using TMPro;
using System.Reflection;

public class CityOptionTests
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
        CopyTestSaveFile("defaultTestSavefile.json");
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
        unloadOperation = SceneManager.UnloadSceneAsync("Main");
        if (unloadOperation != null)
        {
            yield return new WaitUntil(() => unloadOperation.isDone);
        }
        Environment.SetEnvironmentVariable("RUNNING_TESTS", "false");
        // Note: You cannot yield return here to wait for it to complete
    }

    public void CopyTestSaveFile(string fileName)
    {
        string sourcePath = Path.Combine(Application.persistentDataPath, fileName);
        string destinationPath = Path.Combine(Application.persistentDataPath, "testSavefile.json");

        try
        {
            if (File.Exists(sourcePath))
            {
                File.Copy(sourcePath, destinationPath, true);
            }
            else
            {
                Debug.LogError("Source file not found: " + sourcePath);
            }
        }
        catch (IOException ioEx)
        {
            Debug.LogError("File copy failed: " + ioEx.Message);
        }
    }

    [Test]
    public void TestCityOptionsAllLockedAfterLoad()
    {

        List<CityOption> cityOptions = TimeManager.instance.newCityContent.cityOptions;
        Assert.IsTrue(0 < cityOptions.Count, "City Options not found");

        foreach (CityOption cityOption in cityOptions)
        {
            Transform lockedPanelTransform = cityOption.transform.Find("LockedPanel");
            bool isLocked = lockedPanelTransform != null && lockedPanelTransform.gameObject.activeSelf;

            Assert.AreEqual(true, isLocked, $"City '{cityOption.cityName}' lock status does not match expected. Actual Locked: {isLocked}");
        }
    }

    [UnityTest]
    public IEnumerator TestCityOptionsUnlockAfterTaskComplete()
    {
        GameManager.instance.Coins = 1000000000000000000;
        List<CityOption> cityOptions = TimeManager.instance.newCityContent.cityOptions;
        Demographic peasants = GameManager.instance.popContent.demographics.Find(demographic => demographic.Name == "Peasants");
        //meet task of 10 peasants.
        for (int i = 0; i < 11; i++)
        {
            peasants.CapacityLevelUp();
            peasants.PopGrowthPercentComplete = 100;
            peasants.GrowPopulation();
        }
        GameManager.instance.kingdomContent.gameObject.SetActive(true);
        GameManager.instance.kingdomContent.transform.Find("PrestigePanel").gameObject.SetActive(true);
        yield return null;
        foreach (CityOption cityOption in cityOptions)
        {
            Transform lockedPanelTransform = cityOption.transform.Find("LockedPanel");
            bool isLocked = lockedPanelTransform != null && lockedPanelTransform.gameObject.activeSelf;

            if (cityOption.cityName == "Peasantry")
            {
                Assert.AreEqual(false, isLocked, $"City '{cityOption.cityName}' lock status does not match expected. Actual Locked: {isLocked}");
            }
            else
            {
                Assert.AreEqual(true, isLocked, $"City '{cityOption.cityName}' lock status does not match expected. Actual Locked: {isLocked}");
            }
        }
    }

    [UnityTest]
    public IEnumerator TestCityOptionUnlockedFromLoad()
    {
        GameManager.instance.Coins = 1000000000000000000;
        CopyTestSaveFile("cityOptionTestSavefile.json");
        TimeManager.instance.Load();
        GameManager.instance.kingdomContent.gameObject.SetActive(true);
        GameManager.instance.kingdomContent.transform.Find("PrestigePanel").gameObject.SetActive(true);
        yield return null;
        List<CityOption> cityOptions = TimeManager.instance.newCityContent.cityOptions;
        foreach (CityOption cityOption in cityOptions)
        {
            Transform lockedPanelTransform = cityOption.transform.Find("LockedPanel");
            bool isLocked = lockedPanelTransform != null && lockedPanelTransform.gameObject.activeSelf;
            if (cityOption.cityName == "Peasantry")
            {
                Assert.AreEqual(false, isLocked, $"City '{cityOption.cityName}' lock status does not match expected. Actual Locked: {isLocked}");
            }
            else
            {
                Assert.AreEqual(true, isLocked, $"City '{cityOption.cityName}' lock status does not match expected. Actual Locked: {isLocked}");
            }
        }
    }

    // Helper method to inject private fields using reflection
    private void InjectPrivateField(object target, string fieldName, object value)
    {
        var field = target.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        field.SetValue(target, value);
    }

    // Helper method to get private fields using reflection
    private T GetPrivateField<T>(object target, string fieldName)
    {
        var field = target.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        return (T)field.GetValue(target);
    }
}
