using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class TasksManager : MonoBehaviour
{
    public static TasksManager instance;
    public Dictionary<string, bool[]> cityTasksCompletionStatus = new Dictionary<string, bool[]>();
    public UnityEvent<string, int> taskCompleted = new UnityEvent<string, int>();
    public TasksPanel tasksPanel;
    public GameObject taskDescriptionPanel;

    void Start()
    {
    }

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

    void Update()
    {
        
    }

    public void SetupCityTasks(string cityName)
    {
        Debug.Log("Setting up tasks for " + cityName);
        tasksPanel.InitValues(cityName);
    }

    public void OpenTaskDescription(string description)
    {
        TextMeshProUGUI descriptionText = taskDescriptionPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>();
        if (taskDescriptionPanel.activeSelf && descriptionText.text == description)
        {
            taskDescriptionPanel.SetActive(false);
            return;
        }
        taskDescriptionPanel.SetActive(true);
        descriptionText.text = description;
    }

    public void CheckTasks(string type, string target, BigInteger quantity)
    {
        if (type == "PopulationGoal")
        {
            int index = tasksPanel.CheckPopulationTask(target, quantity);
            if (index >= 0)
            {
                if (cityTasksCompletionStatus.ContainsKey(GameManager.instance.CityName))
                {
                    cityTasksCompletionStatus[GameManager.instance.CityName][index] = true;
                }
                else
                {
                    cityTasksCompletionStatus.Add(GameManager.instance.CityName, new bool[3]);
                    cityTasksCompletionStatus[GameManager.instance.CityName][index] = true;
                }
                taskCompleted.Invoke(GameManager.instance.CityName, index);
            }
        } 
        else if (type == "CoinGoal")
        {
            int index = tasksPanel.CheckCoinTask(quantity);
            if (index >= 0)
            {
                if (cityTasksCompletionStatus.ContainsKey(GameManager.instance.CityName))
                {
                    cityTasksCompletionStatus[GameManager.instance.CityName][index] = true;
                }
                else
                {
                    cityTasksCompletionStatus.Add(GameManager.instance.CityName, new bool[3]);
                    cityTasksCompletionStatus[GameManager.instance.CityName][index] = true;
                }
                taskCompleted.Invoke(GameManager.instance.CityName, index);
            }
        }
        else if (type == "BuildingGoal")
        {
            int index = tasksPanel.CheckBuildingTask(target, quantity);
            if (index >= 0)
            {
                if (cityTasksCompletionStatus.ContainsKey(GameManager.instance.CityName))
                {
                    cityTasksCompletionStatus[GameManager.instance.CityName][index] = true;
                }
                else
                {
                    cityTasksCompletionStatus.Add(GameManager.instance.CityName, new bool[3]);
                    cityTasksCompletionStatus[GameManager.instance.CityName][index] = true;
                }
                taskCompleted.Invoke(GameManager.instance.CityName, index);
            }
        }
    }

    public int checkHowManyTaskCompleted()
    {
        int totalTrueCount = 0;

        foreach (var kvp in cityTasksCompletionStatus)
        {
            totalTrueCount += kvp.Value.Count(status => status);
        }

        return totalTrueCount;
    }
}
