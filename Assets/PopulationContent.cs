using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationContent : MonoBehaviour
{
    [SerializeField] private Demographic demoPrefab;
    [SerializeField] private Transform contentTransform;

    // Start is called before the first frame update
    void Start()
    {
        Demographic demo = Instantiate(demoPrefab, contentTransform);
        demo.transform.SetParent(transform, false);
        demo.InitValues("Peasants");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
