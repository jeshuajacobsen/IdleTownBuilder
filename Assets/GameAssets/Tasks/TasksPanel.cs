using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class TasksPanel : MonoBehaviour
{
    public Task taskPrefab;
    public Transform contentTransform;

    private Task[] tasks;
    private string cityName;
    
    void Start()
    {
        TasksManager.instance.taskCompleted.AddListener((cityName, index) =>
        {
            if (cityName != this.cityName)
            {
                return;
            }
            Debug.Log("Task completed listner");
            tasks[index].completed = true;
            tasks[index].transform.GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("StarFilled");
        });
    }

    void Update()
    {
        
    }

    public void InitValues(string cityName)
    {
        this.cityName = cityName;
        tasks = new Task[3];
        CityData cityData = GameManager.instance.gameData.GetCityData(cityName);
        int index = 0;
        foreach (CityData.TaskData task in cityData.tasks)
        {
            tasks[index] = Instantiate(taskPrefab, contentTransform);
            tasks[index].InitValues(task, index);
            tasks[index].transform.SetParent(transform, false);
            tasks[index].transform.localPosition = UnityEngine.Vector3.zero;
            index++;
        }
    }

    public int CheckPopulationTask(string demo, BigInteger quantity)
    {
        int index = 0;
        foreach (Task task in tasks)
        {
            if (task.target == demo && task.quantity <= quantity)
            {
                task.completed = true;
                task.transform.GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("StarFilled");
                return index;
            }
            index++;
        }
        return -1;
    }

    public int CheckCoinTask(BigInteger quantity)
    {
        if (tasks == null)
        {
            return -1;
        }
        int index = 0;
        foreach (Task task in tasks)
        {
            if (task.taskName == "CoinGoal" && task.quantity <= quantity)
            {
                task.completed = true;
                task.transform.GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("StarFilled");
                return index;
            }
            index++;
        }
        return -1;
    }

    public int CheckBuildingTask(string target, BigInteger quantity)
    {
        int index = 0;
        foreach (Task task in tasks)
        {
            if (task.target == target && task.quantity <= quantity)
            {
                task.completed = true;
                task.transform.GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("StarFilled");
                return index;
            }
            index++;
        }
        return -1;
    }
}
