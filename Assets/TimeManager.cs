using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{

    public static TimeManager instance;

    public UnityEvent<int, Dictionary<string, int>, int> timeAwayShowing;
    public UnityEvent<bool> timeAwayHidden;
    public Dictionary<string, int> timeAwayResources = new Dictionary<string, int>(); 
    [SerializeField] private BuildingContent buildingContent;
    [SerializeField] private PopulationContent popContent;

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
        LoadTime("ExitTime");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        SaveTime("ExitTime");
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveTime("PauseTime");
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
                timeAwayResources[resource] = (int)(GameManager.instance.resources[resource] - oldResources[resource]);
            }
            timeAwayShowing.Invoke((int)difference.TotalSeconds, timeAwayResources, GameManager.instance.cityPrestige - oldPrestige);
        } else {
            timeAwayHidden.Invoke(true);
        }
        
        
        Debug.Log($"You have been away for {difference.TotalMinutes} minutes.");
    }

}
