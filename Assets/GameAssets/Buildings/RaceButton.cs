using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceButton : MonoBehaviour
{
    public string raceName;
    public string page = "buildings";
    
    void Start()
    {
        Button button = transform.GetComponent<Button>();
        button.onClick.AddListener(FilterByRace);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FilterByRace()
    {
        if (page == "buildings")
        {
            GameObject.FindWithTag("BuildingContent").transform.GetComponent<BuildingContent>().FilterBuildings(raceName);
        } else {
            GameObject.FindWithTag("PopulationContent").transform.GetComponent<PopulationContent>().FilterDemographics(raceName);
        }
    }
}
