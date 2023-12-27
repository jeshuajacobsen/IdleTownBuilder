using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewCityContent : MonoBehaviour
{
    [SerializeField] private CityOption cityOptionPrefab;
    private Transform contentTransform;

    // Start is called before the first frame update
    void Start()
    {
        CityOption city = Instantiate(cityOptionPrefab, contentTransform);
        city.transform.SetParent(transform, false);
        city.InitValues("Peasantry");

        city = Instantiate(cityOptionPrefab, contentTransform);
        city.transform.SetParent(transform, false);
        city.InitValues("Aquias");

        city = Instantiate(cityOptionPrefab, contentTransform);
        city.transform.SetParent(transform, false);
        city.InitValues("Dwarvary");

        city = Instantiate(cityOptionPrefab, contentTransform);
        city.transform.SetParent(transform, false);
        city.InitValues("Harmony");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
