using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TabContentKingdom : MonoBehaviour
{

    public PrestigeTechTree prestigeTechTreePrefab;
    
    [SerializeField] private PrestigeTechTree prestigeTechTreeHuman;
    [SerializeField] private PrestigeTechTree prestigeTechTreeElf;
    [SerializeField] private PrestigeTechTree prestigeTechTreeDwarf;
    [SerializeField] private PrestigeTechTree prestigeTechTreeFairy;
    [SerializeField] private PrestigeTechTree prestigeTechTreeMerfolk;

    void Start()
    {
        Button button = transform.Find("PrestigeButton").GetComponent<Button>();
        button.onClick.AddListener(ToggleNewCity);
        GameManager.instance.resetCity.AddListener(Reset);

        prestigeTechTreeHuman.InitValues("Human");
        prestigeTechTreeElf.InitValues("Elf");
        prestigeTechTreeDwarf.InitValues("Dwarf");
        prestigeTechTreeFairy.InitValues("Fairy");
        prestigeTechTreeMerfolk.InitValues("Merfolk");
    }

    void Update()
    {
        
    }

    public void showRaceResearch(string raceName)
    {
        prestigeTechTreeHuman.gameObject.SetActive(raceName == "Human");
        prestigeTechTreeElf.gameObject.SetActive(raceName == "Elf");
        prestigeTechTreeDwarf.gameObject.SetActive(raceName == "Dwarf");
        prestigeTechTreeFairy.gameObject.SetActive(raceName == "Fairy");
        prestigeTechTreeMerfolk.gameObject.SetActive(raceName == "Merfolk");

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
