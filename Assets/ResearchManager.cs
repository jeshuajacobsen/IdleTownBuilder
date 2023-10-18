using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{

    public static ResearchManager instance;

    public float farmMultiplier = 1;
    public float woodMultiplier = 1;
    public float incomeMiltiplier = 1;
    public float farmCostMultiplier = 1;
    public float peasentPrestigeMultiplier = 1;
    public float farmSpeedUp = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade(string upgradeTitle)
    {
        switch (upgradeTitle)
        {
            case "Farming":
                farmMultiplier += .1f;
                break;
            case "Forestry":
                woodMultiplier += .1f;
                break;
            case "Market":
                incomeMiltiplier += .1f;
                break;
            case "Fertilizer":
                farmCostMultiplier -= .1f;
                break;
            case "Peasentry":
                peasentPrestigeMultiplier += .1f;
                break;
            case "Fast Crops":
                farmSpeedUp += .2f;
                break;
        }
    }
}
