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
        transform.Find("LockedPanel").GetComponent<LockedPanel>().SetUsePrestige(true);

        switch(cityName)
        {
            case "Peasantry":
                Unlock();
                break;
            case "Aquias":
                unlockCost = 300;
                break;
            case "Dwarvary":
                unlockCost = 1000;
                break;
            case "Harmony":
                unlockCost = 5000;
                break;
        }
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
