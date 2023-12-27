using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using Newtonsoft.Json;

public class TimeManager : MonoBehaviour
{

    public static TimeManager instance;

    public UnityEvent<int, Dictionary<string, int>, int> timeAwayShowing;
    public UnityEvent<bool> timeAwayHidden;
    public Dictionary<string, int> timeAwayResources = new Dictionary<string, int>(); 
    [SerializeField] private BuildingContent buildingContent;
    [SerializeField] private PopulationContent popContent;
    [SerializeField] private MarketContent marketContent;
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
        LoadTime("ExitTime");
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
            LoadTime("PauseTime");
        }
    }

    void SaveTime(string key)
    {
        DateTime now = DateTime.UtcNow;
        PlayerPrefs.SetString(key, now.ToString());
        PlayerPrefs.Save();
    }

    void LoadTime(string key)
    {
        string oldTime = PlayerPrefs.GetString(key, DateTime.UtcNow.ToString());
        DateTime oldDateTime = DateTime.Parse(oldTime);
        
        // Get the current time
        DateTime now = DateTime.UtcNow;
        
        // Calculate the difference
        TimeSpan difference = now - oldDateTime;

        Dictionary<string, int> oldResources = new Dictionary<string, int>(GameManager.instance.resources);
        int oldPrestige = GameManager.instance.cityPrestige;

        if (difference.TotalMinutes >= 1)
        {
            for (int i = 0; i < difference.TotalSeconds; i++)
            {
                buildingContent.TickAll();
                popContent.TickAll();
            }
            timeAwayResources = new Dictionary<string, int>();
            foreach (string resource in GameManager.instance.resources.Keys)
            {
                int oldAmount = oldResources.TryGetValue(resource, out int value) ? oldResources[resource] : 0;
                timeAwayResources[resource] = (int)(GameManager.instance.resources[resource] - oldAmount);
            }
            timeAwayShowing.Invoke((int)difference.TotalSeconds, timeAwayResources, GameManager.instance.cityPrestige - oldPrestige);
        } else {
            timeAwayHidden.Invoke(true);
        }
        
        
        Debug.Log($"You have been away for {difference.TotalMinutes} minutes.");
    }

    public void Save()
    {
        GameManager.instance.PrepForSave(saveData);
        ResearchManager.instance.PrepForSave(saveData);
        buildingContent.PrepForSave(saveData);
        marketContent.PrepForSave(saveData);
        string jsonData = JsonConvert.SerializeObject(saveData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", jsonData);
        Debug.Log(jsonData);
        Debug.Log("Saved data to: " + Application.persistentDataPath + "/savefile.json");

    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (System.IO.File.Exists(path))
        {
            string jsonData = System.IO.File.ReadAllText(path);
            Debug.Log(jsonData);
            saveData = JsonConvert.DeserializeObject<SaveData>(jsonData);
            GameManager.instance.LoadSavedData(saveData);
            ResearchManager.instance.LoadSavedData(saveData);
            buildingContent.LoadSavedData(saveData);
            marketContent.LoadSavedData(saveData);
            Debug.Log("Loaded from: " + Application.persistentDataPath + "/savefile.json");
        } else {
            GameManager.instance.StartNewGame();
            ResearchManager.instance.StartNewGame();
        }
    }
}
