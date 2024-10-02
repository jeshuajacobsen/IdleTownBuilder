using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ResearchTier : MonoBehaviour
{
    private List<PrestigeResearchButton> researchButtons = new List<PrestigeResearchButton>();

    public PrestigeResearchButton researchButtonPrefab;
    [SerializeField] private Transform contentTransform;
    public UnityEvent<string> onUpgrade;
    public int tier;
    public string raceName;
    public int researchedCount = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InitValues(int tier, string raceName)
    {
        this.tier = tier;
        this.raceName = raceName;
        
        transform.Find("TitlePanel").Find("Title").GetComponent<TextMeshProUGUI>().text = raceName + " Tier " + (tier + 1);
        if (tier == 0)
        {
            unlock();
        }
    }

    public void AddResearchButton(string title)
    {
        if (researchButtonPrefab == null)
        {
            researchButtonPrefab = Resources.Load<PrestigeResearchButton>("PrestigeResearchButton");
        }
        PrestigeResearchButton researchButton = Instantiate(researchButtonPrefab, contentTransform);
        researchButton.InitValues(title, false);
        researchedCount += researchButton.level;
        researchButtons.Add(researchButton);
    }

    public void AddBuildingButton(string title)
    {
        if (researchButtonPrefab == null)
        {
            researchButtonPrefab = Resources.Load<PrestigeResearchButton>("PrestigeResearchButton");
        }
        PrestigeResearchButton researchButton = Instantiate(researchButtonPrefab, contentTransform);
        researchButton.InitValues(title, true);
        researchedCount += researchButton.level;
        researchButtons.Add(researchButton);
        //onUpgrade.Invoke(title);
    }
    
    public void unlock()
    {
        transform.Find("LockPanel")?.gameObject.SetActive(false);
    }
}
