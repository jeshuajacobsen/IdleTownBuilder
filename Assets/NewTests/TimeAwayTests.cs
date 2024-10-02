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

public class TimeAwayTests
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
        CopyTestSaveFile();
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

    public void CopyTestSaveFile()
    {
        string sourcePath = Path.Combine(Application.persistentDataPath, "defaultTestSavefile.json");
        string destinationPath = Path.Combine(Application.persistentDataPath, "testSavefile.json");

        try
        {
            // Check if the source file exists
            if (File.Exists(sourcePath))
            {
                // Copy the file to the destination (overwrite if it exists)
                File.Copy(sourcePath, destinationPath, true);
                Debug.Log("File copied successfully.");
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

    [UnityTest]
    public IEnumerator MaxTimeAwayActuallyRestrictsTimeAway()
    {
        yield return null;

        DateTime now = DateTime.UtcNow.AddMinutes(-140);
        PlayerPrefs.SetString("testTime", now.ToString());
        PlayerPrefs.Save();
        timeManager.LoadTime("testTime", false);
        
        Assert.AreEqual("You were away for 120 minutes", GameObject.FindGameObjectWithTag("TimeAwayPanel").transform.Find("TimeAwayPanel").Find("AwayTime").GetComponent<TextMeshProUGUI>().text);
    }

        [UnityTest]
    public IEnumerator TimeAwayGeneratesWheat()
    {
        yield return null;

        DateTime now = DateTime.UtcNow.AddMinutes(-1);
        PlayerPrefs.SetString("testTime", now.ToString());
        PlayerPrefs.Save();
        timeManager.LoadTime("testTime", false);
        
        Assert.AreEqual(new BigInteger(8), timeManager.timeAwayResources["Wheat"]);
    }
}