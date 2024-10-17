using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{

    public string taskName;
    public string cityName;
    public BigInteger quantity;
    public string target;
    public bool completed = false;
    private int index;

    void Start()
    {
        
    }

    void Update()
    {
        if (TasksManager.instance.cityTasksCompletionStatus.ContainsKey(cityName))
        {
            bool[] tasks = TasksManager.instance.cityTasksCompletionStatus[cityName];
            if (tasks[index])
            {
                completed = true;
                this.transform.GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("StarFilled");
            }
        }
    }

    public void InitValues(CityData.TaskData taskData, int index, string cityName)
    {
        this.cityName = cityName;
        taskName = taskData.name;
        quantity = taskData.quantity;
        target = taskData.target;
        this.index = index;
    }

    public void OpenDescription()
    {
        string description;
        if (taskName == "PopulationGoal")
        {
            description = "Reach a " + target + " Population of " + quantity;
        }
        else if (taskName == "BuildingGoal")
        {
            description = "Reach " + target + " level " + quantity;
        }
        else
        {
            description = "Unknown Task";
        }
        TasksManager.instance.OpenTaskDescription(description);
    }
}
