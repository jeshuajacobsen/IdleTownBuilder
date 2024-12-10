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
    }

    void Update()
    {
        
    }

    public void InitValues(string cityName)
    {
        this.cityName = cityName;
        if (tasks != null)
        {
            foreach (Task task in tasks)
            {
                Destroy(task.gameObject);
            }
        }
        tasks = new Task[3];
        CityData cityData = GameManager.instance.gameData.GetCityData(cityName);
        Debug.Log("CityData: " + cityData.tasks[0].name);
        Debug.Log("CityData: " + cityData.tasks[0].target);
        Debug.Log("CityData: " + cityData.tasks[1].name);
        Debug.Log("CityData: " + cityData.tasks[1].target);
        int index = 0;
        foreach (CityData.TaskData task in cityData.tasks)
        {
            tasks[index] = Instantiate(taskPrefab, contentTransform);
            tasks[index].InitValues(task, index, cityName);
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
                return index;
            }
            index++;
        }
        return -1;
    }
}
