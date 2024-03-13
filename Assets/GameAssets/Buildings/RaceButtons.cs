using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceButtons : MonoBehaviour
{

    public RaceButton raceButtonPrefab;
    private List<RaceButton> raceButtons;
    public Transform contentTransform;

    public string page;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.resetCity.AddListener(Reset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddRace(string raceName)
    {
        RaceButton raceButton = Instantiate(raceButtonPrefab, contentTransform);
        raceButton.transform.SetParent(transform, false);
        raceButton.raceName = raceName;
        raceButton.page = page;
        raceButtons.Add(raceButton);
    }

    public void Reset(string newCityName)
    {

        if (raceButtons != null)
        {
            foreach(RaceButton item in raceButtons)
            {
                Destroy(item.gameObject);
            }
        }
        
        raceButtons = new List<RaceButton>();

        switch(newCityName)
        {
            case "Peasantry":
                AddRace("Human");
                break;
            case "Aquias":
                AddRace("Human");
                AddRace("Merfolk");
                break;
            case "Dwarvary":
                AddRace("Human");
                AddRace("Dwarf");
                break;
            case "Mountain Port":
                AddRace("Human");
                AddRace("Merfolk");
                AddRace("Dwarf");
                break;
            case "Fairia":
                AddRace("Human");
                AddRace("Merfolk");
                AddRace("Dwarf");
                AddRace("Fairy");
                break;
            case "Elveryn":
                AddRace("Human");
                AddRace("Merfolk");
                AddRace("Dwarf");
                AddRace("Elf");
                break;
        }
    }
}
