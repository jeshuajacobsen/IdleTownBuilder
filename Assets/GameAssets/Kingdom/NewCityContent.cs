using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewCityContent : MonoBehaviour
{
    [SerializeField] private CityOption cityOptionPrefab;
    private Transform contentTransform;

    private List<CityOption> cityOptions = new List<CityOption>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setup()
    {
        this.AddCity("Peasantry");
        this.AddCity("Aquias");
        this.AddCity("Dwarvary");
        this.AddCity("Mountain Port");
        this.AddCity("Fairia");
        this.AddCity("Elveryn");
    }

    private void AddCity(string cityName)
    {
        CityOption city = Instantiate(cityOptionPrefab, contentTransform);
        city.transform.SetParent(transform, false);
        city.InitValues(cityName);
        cityOptions.Add(city);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrepForSave(SaveData saveData)
    {
        saveData.cityOptionLocks = new Dictionary<string, bool>();
        foreach (CityOption city in cityOptions)
        {
            saveData.cityOptionLocks[city.cityName] = city.transform.Find("LockedPanel").gameObject.activeSelf;
        }
    }

    public void LoadSavedData(SaveData saveData) 
    {
        foreach (string key in saveData.cityOptionLocks.Keys)
        {
            CityOption option = cityOptions.Find(city => city.cityName == key);
            if (option != null)
            {
                if (saveData.cityOptionLocks[key])
                {
                    option.Unlock();
                }
            } else {
                Debug.Log("Couldn't find city option to load. " + key);
            }
        }
    }
}
