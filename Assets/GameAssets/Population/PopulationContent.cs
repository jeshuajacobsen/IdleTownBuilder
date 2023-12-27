using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationContent : MonoBehaviour
{
    [SerializeField] private Demographic demoPrefab;
    [SerializeField] private Transform contentTransform;
    public List<Demographic> demographics;

    void Awake()
    {
        GameManager.instance.resetCity.AddListener(Reset);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TickAll()
    {
        foreach (Demographic currentDemo in demographics)
        {
            if (!currentDemo.transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().locked)
            {
                currentDemo.Tick();
            }
        }
    }

    void Reset(string newCityName)
    {
        if (demographics != null)
        {
            foreach(Demographic item in demographics)
            {
                Destroy(item.gameObject);
            }
        }
        demographics = new List<Demographic>();
        

        switch(newCityName)
        {
            case "Peasantry":
                AddDemographic("Peasants");
                AddDemographic("Commoners");
                break;
            case "Aquias":
                AddDemographic("Peasants");
                AddDemographic("Commoners");
                break;
            case "Dwarvary":
                AddDemographic("Peasants");
                AddDemographic("Commoners");
                break;
            case "Harmony":
                AddDemographic("Peasants");
                AddDemographic("Commoners");
                break;
        }

        
    }

    private void AddDemographic(string demoName)
    {
        Demographic demo = Instantiate(demoPrefab, contentTransform);
        demo.transform.SetParent(transform, false);
        demo.InitValues(demoName);
        demographics.Add(demo);
    }

    public void PrepForSave(SaveData saveData)
    {
       saveData.SetDemographicSaveData(demographics);
    }

    public void LoadSavedData(SaveData saveData) 
    {
        foreach (string key in saveData.demographicLevels.Keys)
        {
            Demographic demo = demographics.Find(demo => demo.Name == key);
            if (demo != null)
            {
                if (saveData.demographicLevels[key] > 0)
                {
                    demo.Unlock();
                }
                demo.Level = saveData.demographicLevels[key];
            } else {
                Debug.Log("Couldn't find demographic to load. " + key);
            }
        }
    }
}
