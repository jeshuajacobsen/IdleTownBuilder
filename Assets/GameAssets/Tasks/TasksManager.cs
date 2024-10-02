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
    private int taskCompletedCount = 0;

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
                if (cityTasksCompletionStatus.ContainsKey(GameManager.instance.currentCity))
                {
                    cityTasksCompletionStatus[GameManager.instance.currentCity][index] = true;
                }
                else
                {
                    cityTasksCompletionStatus.Add(GameManager.instance.currentCity, new bool[3]);
                    cityTasksCompletionStatus[GameManager.instance.currentCity][index] = true;
                }
                taskCompleted.Invoke(GameManager.instance.currentCity, index);
            }
        } 
        else if (type == "CoinGoal")
        {
            int index = tasksPanel.CheckCoinTask(quantity);
            if (index >= 0)
            {
                if (cityTasksCompletionStatus.ContainsKey(GameManager.instance.currentCity))
                {
                    cityTasksCompletionStatus[GameManager.instance.currentCity][index] = true;
                }
                else
                {
                    cityTasksCompletionStatus.Add(GameManager.instance.currentCity, new bool[3]);
                    cityTasksCompletionStatus[GameManager.instance.currentCity][index] = true;
                }
                taskCompleted.Invoke(GameManager.instance.currentCity, index);
            }
        }
        else if (type == "BuildingGoal")
        {
            int index = tasksPanel.CheckBuildingTask(target, quantity);
            if (index >= 0)
            {
                if (cityTasksCompletionStatus.ContainsKey(GameManager.instance.currentCity))
                {
                    cityTasksCompletionStatus[GameManager.instance.currentCity][index] = true;
                }
                else
                {
                    cityTasksCompletionStatus.Add(GameManager.instance.currentCity, new bool[3]);
                    cityTasksCompletionStatus[GameManager.instance.currentCity][index] = true;
                }
                taskCompleted.Invoke(GameManager.instance.currentCity, index);
            }
        }
    }

    public void SetupCityTasks(Dictionary<string, bool[]> tasksCompletion)
    {
        cityTasksCompletionStatus = tasksCompletion;
        foreach (string city in tasksCompletion.Keys)
        {
            for (int i = 0; i < tasksCompletion[city].Length; i++)
            {
                if (tasksCompletion[city][i])
                {
                    taskCompleted.Invoke(city, i);
                }
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
