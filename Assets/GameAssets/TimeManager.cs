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

    public UnityEvent<int, Dictionary<string, BigInteger>, BigInteger> timeAwayShowing;
    public UnityEvent<bool> timeAwayHidden;
    public Dictionary<string, BigInteger> timeAwayResources = new Dictionary<string, BigInteger>(); 
    [SerializeField] private BuildingContent buildingContent;
    [SerializeField] private PopulationContent popContent;
    [SerializeField] private MarketContent marketContent;
    [SerializeField] private RaceButtons buildingRaceButtons;
    [SerializeField] private RaceButtons populationRaceButtons;

    [SerializeField] private NewCityContent newCityContent;

    [SerializeField] private TasksPanel tasksPanel;
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

        int processSeconds = isTimeWarping ? 
            (int)TimeSpan.FromHours(2).TotalSeconds : Math.Min((int)difference.TotalSeconds, maxAwayTime);

        Dictionary<string, BigInteger> oldResources = new Dictionary<string, BigInteger>(GameManager.instance.resources);
        BigInteger oldPrestige = GameManager.instance.CityPrestige;

        if (difference.TotalMinutes >= 1)
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
        
        
        Debug.Log($"You have been away for {difference.TotalMinutes} minutes.");
    }

    public void Save()
    {
        if (Environment.GetEnvironmentVariable("RUNNING_TESTS") != "true")
        {
            GameManager.instance.PrepForSave(saveData);
            ResearchManager.instance.PrepForSave(saveData);
            buildingContent.PrepForSave(saveData);
            marketContent.PrepForSave(saveData);
            popContent.PrepForSave(saveData);
            newCityContent.PrepForSave(saveData);
            saveData.taskCompletion = TasksManager.instance.cityTasksCompletionStatus;
            string jsonData = JsonConvert.SerializeObject(saveData);
        
            System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", jsonData);
            Debug.Log(jsonData);
            Debug.Log("Saved data to: " + Application.persistentDataPath + "/savefile.json");
        }
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (System.IO.File.Exists(path) && Environment.GetEnvironmentVariable("RUNNING_TESTS") != "true")
        {
            string jsonData = System.IO.File.ReadAllText(path);
            Debug.Log(jsonData);
            saveData = JsonConvert.DeserializeObject<SaveData>(jsonData);
            GameManager.instance.LoadSavedData(saveData);
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
            newCityContent.Setup();
            newCityContent.LoadSavedData(saveData);
            tasksPanel.InitValues(saveData.cityName);
            TasksManager.instance.SetupCityTasks(saveData.taskCompletion);
            Debug.Log("Loaded from: " + Application.persistentDataPath + "/savefile.json");
        } else {
            GameManager.instance.StartNewGame();
            ResearchManager.instance.StartNewGame();
            buildingContent.Reset("Peasantry");
            marketContent.Reset("Peasantry");
            popContent.Reset("Peasantry");
            buildingRaceButtons.Reset("Peasantry");
            populationRaceButtons.Reset("Peasantry");
            newCityContent.Setup();
            tasksPanel.InitValues("Peasantry");
            TasksManager.instance.cityTasksCompletionStatus = new Dictionary<string, bool[]>();
        }
    }
}
