using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResearchTier : MonoBehaviour
{
    private List<PrestigeResearchButton> researchButtons = new List<PrestigeResearchButton>();

    public PrestigeResearchButton researchButtonPrefab;
    [SerializeField] private Transform contentTransform;
    public UnityEvent<string> onUpgrade;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddResearchButton(string title)
    {
        if (researchButtonPrefab == null)
        {
            researchButtonPrefab = Resources.Load<PrestigeResearchButton>("PrestigeResearchButton");
        }
        PrestigeResearchButton researchButton = Instantiate(researchButtonPrefab, contentTransform);
        researchButton.InitValues(title, false);
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
        researchButtons.Add(researchButton);
        onUpgrade.Invoke(title);
    }
}
