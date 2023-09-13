using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CityOption : MonoBehaviour
{
    string cityName;
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
    }

    public void SelectNewCity()
    {
        GameManager.instance.StartNewCity(cityName);
    }
}
