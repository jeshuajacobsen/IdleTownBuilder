using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationContent : MonoBehaviour
{
    [SerializeField] private Demographic demoPrefab;
    [SerializeField] private Transform contentTransform;
    public List<Demographic> demographics;

    void Awake()
    {
        GameManager.instance.resetCity.AddListener(Reset);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TickAll()
    {
        foreach (Demographic currentDemo in demographics)
        {
            if (!currentDemo.transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().locked)
            {
                currentDemo.Tick();
            }
        }
    }

    void Reset(string newCityName)
    {
        if (demographics != null)
        {
            foreach(Demographic item in demographics)
            {
                Destroy(item.gameObject);
            }
        }
        demographics = new List<Demographic>();

        Demographic demo = Instantiate(demoPrefab, contentTransform);
        demo.transform.SetParent(transform, false);
        demo.InitValues("Peasants", 3);
        demographics.Add(demo);
    }
}
