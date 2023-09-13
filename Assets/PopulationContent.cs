using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationContent : MonoBehaviour
{
    [SerializeField] private Demographic demoPrefab;
    [SerializeField] private Transform contentTransform;
    private List<Demographic> demographics;

    void Awake()
    {
        Demographic demo = Instantiate(demoPrefab, contentTransform);
        demo.transform.SetParent(transform, false);
        demo.InitValues("Peasants");
        demographics = new List<Demographic>();
        demographics.Add(demo);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.resetCity.AddListener(Reset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Reset(string newCityName)
    {
        foreach(Demographic item in demographics)
        {
            Destroy(item.gameObject);
        }
        demographics = new List<Demographic>();

        Demographic demo = Instantiate(demoPrefab, contentTransform);
        demo.transform.SetParent(transform, false);
        demo.InitValues("Peasants");
    }
}
