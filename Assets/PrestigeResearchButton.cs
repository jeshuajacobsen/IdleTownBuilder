using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeResearchButton : MonoBehaviour
{

    int level = 0;
    int maxLevel = 20;
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private int baseCost = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInSelectedResearch()
    {
        transform.parent.parent.Find("SelectedResearchBackground")
            .Find("ResearchInfoPanel").GetComponent<ResearchInfoPanel>().Setup(title, description, baseCost, level, maxLevel);
    }
}
