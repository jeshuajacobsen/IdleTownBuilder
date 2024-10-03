using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using Newtonsoft.Json;
using System.Numerics;

public class TimeManager : MonoBehaviour
{

    public static TimeManager instance;
    private int maxAwayTime = (int)TimeSpan.FromHours(2).TotalSeconds;

    public double managerTime = 0;

    public UnityEvent<int, Dictionary<string, BigInteger>, BigInteger> timeAwayShowing;
    public UnityEvent<bool> timeAwayHidden;
    public Dictionary<string, BigInteger> timeAwayResources = new Dictionary<string, BigInteger>(); 
    [SerializeField] private BuildingContent buildingContent;
    [SerializeField] private PopulationContent popContent;
    [SerializeField] private MarketContent marketContent;
    [SerializeField] private RaceButtons buildingRaceButtons;
    [SerializeField] private RaceButtons populationRaceButtons;
    [SerializeField] private RaceButtons kingdomRaceButtons;

    public NewCityContent newCityContent;

    [SerializeField] private TasksPanel tasksPanel;
    [SerializeField] private GameObject scienceContent;
    private SaveData saveData = new SaveData();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        InvokeRepeating("Tick", 1.0f, 1.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        Load();
        LoadTime("ExitTime", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tick()
    {
        managerTime++;
        if (managerTime >= 3600)
        {
            managerTime -= 3600;
            GameManager.instance.ManagerLevelUpTriggered();
        }
    }
    

    void OnApplicationQuit()
    {
        SaveTime("ExitTime");
        Save();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveTime("PauseTime");
            Save();
        }
        else
        {
            LoadTime("PauseTime", false);
        }
    }

    void SaveTime(string key)
    {
        DateTime now = DateTime.UtcNow;
        PlayerPrefs.SetString(key, now.ToString());
        PlayerPrefs.Save();
    }

    public void LoadTime(string key, bool isTimeWarping)
    {
        string oldTime = PlayerPrefs.GetString(key, DateTime.UtcNow.ToString());
        DateTime oldDateTime = DateTime.Parse(oldTime);
        
        // Get the current time
        DateTime now = DateTime.UtcNow;
        
        // Calculate the difference
        TimeSpan difference = now - oldDateTime;


        int finalMaxAwayTime = (int)TimeSpan.FromMinutes(120 + (ResearchManager.instance.prestigeResearchLevels.ContainsKey("Max Time Away") ? ResearchManager.instance.prestigeResearchLevels["Max Time Away"] * 20 : 0)).TotalSeconds;
        int processSeconds = isTimeWarping ? 
            (int)TimeSpan.FromHours(2).TotalSeconds : Math.Min((int)difference.TotalSeconds, finalMaxAwayTime);

        Dictionary<string, BigInteger> oldResources = new Dictionary<string, BigInteger>(GameManager.instance.resources);
        BigInteger oldPrestige = GameManager.instance.CityPrestige;

        if (processSeconds >= 1)
        {
            for (int i = 0; i < processSeconds; i++)
            {
                buildingContent.TickAll();
                popContent.TickAll();
            }
            timeAwayResources = new Dictionary<string, BigInteger>();
            foreach (string resource in GameManager.instance.resources.Keys)
            {
                BigInteger oldAmount = oldResources.TryGetValue(resource, out BigInteger value) ? oldResources[resource] : 0;
                timeAwayResources[resource] = GameManager.instance.resources[resource] - oldAmount;
            }
            timeAwayShowing.Invoke((int)processSeconds, timeAwayResources, GameManager.instance.CityPrestige - oldPrestige);
        } else {
            timeAwayHidden.Invoke(true);
        }
        
        
        Debug.Log($"You have been away for {(int)difference.TotalMinutes} minutes.");
    }

    public void Save()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (Environment.GetEnvironmentVariable("RUNNING_TESTS") == "true")
        {
            path = Application.persistentDataPath + "/testSavefile.json";
        }
        GameManager.instance.PrepForSave(saveData);
        ResearchManager.instance.PrepForSave(saveData);
        buildingContent.PrepForSave(saveData);
        marketContent.PrepForSave(saveData);
        popContent.PrepForSave(saveData);
        newCityContent.PrepForSave(saveData);
        saveData.taskCompletion = TasksManager.instance.cityTasksCompletionStatus;
        string jsonData = JsonConvert.SerializeObject(saveData);
    
        System.IO.File.WriteAllText(path, jsonData);
        Debug.Log(jsonData);
        Debug.Log("Saved data to: " + path);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (Environment.GetEnvironmentVariable("RUNNING_TESTS") == "true")
        {
            path = Application.persistentDataPath + "/testSavefile.json";
        }
        
        if (System.IO.File.Exists(path))
        {
            string jsonData = System.IO.File.ReadAllText(path);
            Debug.Log(jsonData);
            saveData = JsonConvert.DeserializeObject<SaveData>(jsonData);
            GameManager.instance.LoadSavedData(saveData);
            scienceContent.SetActive(true);
            scienceContent.SetActive(false);
            ResearchManager.instance.Reset(saveData.cityName);
            ResearchManager.instance.LoadSavedData(saveData);
            buildingContent.Reset(saveData.cityName);
            buildingContent.LoadSavedData(saveData);
            marketContent.Reset(saveData.cityName);
            marketContent.LoadSavedData(saveData);
            popContent.Reset(saveData.cityName);
            popContent.LoadSavedData(saveData);
            buildingRaceButtons.Reset(saveData.cityName);
            populationRaceButtons.Reset(saveData.cityName);
            kingdomRaceButtons.Reset(saveData.cityName);
            tasksPanel.InitValues(saveData.cityName);
            TasksManager.instance.SetupCityTasks(saveData.taskCompletion);
            newCityContent.Setup();
            newCityContent.LoadSavedData(saveData);
            Debug.Log("Loaded from: " + path);
        } else {
            GameManager.instance.StartNewGame();
            ResearchManager.instance.StartNewGame();
            buildingContent.Reset("Peasantry");
            marketContent.Reset("Peasantry");
            popContent.Reset("Peasantry");
            buildingRaceButtons.Reset("Peasantry");
            populationRaceButtons.Reset("Peasantry");
            kingdomRaceButtons.Reset("Peasantry");
            newCityContent.Setup();
            tasksPanel.InitValues("Peasantry");
            TasksManager.instance.cityTasksCompletionStatus = new Dictionary<string, bool[]>();
        }
    }
}
