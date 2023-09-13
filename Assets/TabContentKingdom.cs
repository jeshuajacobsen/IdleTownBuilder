using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TabContentKingdom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = transform.Find("PrestigeButton").GetComponent<Button>();
        button.onClick.AddListener(ToggleNewCity);
        GameManager.instance.resetCity.AddListener(Reset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleNewCity()
    {
        transform.Find("PrestigePanel").gameObject.SetActive(!transform.Find("PrestigePanel").gameObject.activeSelf);
        transform.Find("PrestigeButton").Find("Text").GetComponent<TextMeshProUGUI>().text = 
            transform.Find("PrestigePanel").gameObject.activeSelf ? "Close" : "New City";
    }

    void Reset(string newCityName)
    {
        ToggleNewCity();
    }
}
