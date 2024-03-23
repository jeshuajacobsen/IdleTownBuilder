using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public class TasksManager : MonoBehaviour
{
    public static TasksManager instance;
    public Dictionary<string, bool[]> cityTasksCompletionStatus = new Dictionary<string, bool[]>();
    public UnityEvent<string, int> taskCompleted = new UnityEvent<string, int>();
    public TasksPanel tasksPanel;

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
}
