using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void Reset(string newCityName)
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
                AddDemographic("Surfs");
                break;
            case "Dwarvary":
                AddDemographic("Peasants");
                AddDemographic("Commoners");
                AddDemographic("Miners");
                break;
            case "place":
                AddDemographic("Peasants");
                AddDemographic("Commoners");
                AddDemographic("Tradesmen"); 
                AddDemographic("Surfs");
                AddDemographic("Middle Mer");
                AddDemographic("Miners");
                AddDemographic("Workers");
                break;
        }

        FilterDemographics(demographics[0].race);
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
                if (saveData.demographicLevels[key].capacityLevel > 0)
                {
                    demo.Unlock();
                }
                demo.CapacityLevel = saveData.demographicLevels[key].capacityLevel;
                demo.GrowthLevel = saveData.demographicLevels[key].growthLevel;
                demo.Population = saveData.demographicLevels[key].population;
            } else {
                Debug.Log("Couldn't find demographic to load. " + key);
            }
        }
        FilterDemographics(demographics[0].race);
    }

    public void FilterDemographics(string raceName)
    {
        foreach (Demographic demo in demographics)
        {
            if (demo.race == raceName)
            {
                demo.canvas.enabled = true;
                
                RectTransform rectTransform = demo.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 320);
            } else {
                demo.canvas.enabled = false;

                RectTransform rectTransform = demo.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 0);
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetComponent<RectTransform>());
    }
}
