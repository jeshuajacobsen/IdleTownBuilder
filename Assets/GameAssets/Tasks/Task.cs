using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{

    public string taskName;
    public BigInteger quantity;
    public string target;
    public bool completed = false;
    private int index;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InitValues(CityData.TaskData taskData, int index)
    {
        taskName = taskData.name;
        quantity = taskData.quantity;
        target = taskData.target;
        this.index = index;
        completed = TasksManager.instance.cityTasksCompletionStatus.ContainsKey(GameManager.instance.currentCity) && TasksManager.instance.cityTasksCompletionStatus[GameManager.instance.currentCity][index];
        if (completed)
        {
            transform.GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("StarFilled");
        }
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
