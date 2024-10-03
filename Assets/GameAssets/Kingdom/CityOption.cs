using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;

public class CityOption : MonoBehaviour, Unlockable
{
    public string cityName;
    int unlockCost = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitValues(string newName)
    {
        cityName = newName;
        transform.Find("CityNameText").GetComponent<TextMeshProUGUI>().text = newName;
        transform.Find("BuildButton").GetComponent<Button>().onClick.AddListener(SelectNewCity);

        CityData cityData = GameManager.instance.gameData.GetCityData(newName);
        unlockCost = cityData.unlockCost;
        transform.Find("LockedPanel").Find("UnlockCostText").GetComponent<TextMeshProUGUI>().text = unlockCost +"X";

        Transform raceButtonPanel = transform.Find("RaceButtonPanel");
        foreach (string race in cityData.races)
        {
            
            GameObject imageGameObject = new GameObject("RaceButtonImage");
            imageGameObject.AddComponent<Image>();
            imageGameObject.GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite(race + "Crest");
            imageGameObject.transform.SetParent(raceButtonPanel, false);
        }
        transform.Find("TasksPanel").GetComponent<TasksPanel>().InitValues(newName);

        TasksManager.instance.taskCompleted.AddListener((cityName, index) =>
        {
            if (TasksManager.instance.checkHowManyTaskCompleted() >= unlockCost)
            {
                Unlock();
            }
        });
    }

    public void SelectNewCity()
    {
        GameManager.instance.StartNewCity(cityName);
    }

    public void Unlock()
    {
        transform.Find("LockedPanel").gameObject.SetActive(false);
    }

    public BigInteger GetUnlockCost()
    {
        return unlockCost;
    }
}
