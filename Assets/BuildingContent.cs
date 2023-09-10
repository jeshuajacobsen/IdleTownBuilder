using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingContent : MonoBehaviour
{
    public Building BuildingPrefab;
    public Transform contentTransform;

    // Start is called before the first frame update
    void Start()
    {
        Building building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        building.initValues("Farm", "Wheat");

        building = Instantiate(BuildingPrefab, contentTransform);
        building.transform.SetParent(transform, false);
        building.initValues("Forester", "Wood");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
