using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{

    public static ResearchManager instance;

    public float farmMultiplier = 1;

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
        farmMultiplier += .1f;
    }
}
