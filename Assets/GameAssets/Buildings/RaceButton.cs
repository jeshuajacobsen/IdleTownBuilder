using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceButton : MonoBehaviour
{
    public string raceName;
    public string page;
    
    void Start()
    {
        Button button = transform.GetComponent<Button>();
        button.onClick.AddListener(FilterByRace);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FilterByRace()
    {
        if (page == "kingdom")
        {
            GameManager.instance.kingdomContent.showRaceResearch(raceName);
        }
        else
        {
            GameManager.instance.buildingContent.FilterBuildings(raceName);
            GameManager.instance.popContent.FilterDemographics(raceName);
        }
    }

}
