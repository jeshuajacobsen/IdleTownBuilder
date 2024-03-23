using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        raceButton.transform.GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite(raceName + "Crest");
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

        CityData cityData = GameManager.instance.gameData.GetCityData(newCityName);
        foreach(string raceName in cityData.races)
        {
            AddRace(raceName);
        }

    }
}
