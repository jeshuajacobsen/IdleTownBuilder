using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using SharpUI.Source.Common.UI.Elements.Loading;
using System.Numerics;

public class ProductionInput : MonoBehaviour
{

    private double productionTimer = 0;
    private const double requiredTime = 10;
    public string resource = "";
    public BigInteger requiredAmount = 1;
    public UnityEvent<string> onProductionClick;
    public bool locked = true;

    public LoadingBar loadingBar;

    void Awake()
    {
        onProductionClick = new UnityEvent<string>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.productionTimers.ContainsKey(resource))
        {
           productionTimer = GameManager.instance.productionTimers[resource]; 
        }
        loadingBar.UpdatePercentage((float)(productionTimer / requiredTime * 100));
    }

    public void InitValues(string newResource)
    {
        resource = newResource;
        requiredAmount = 1;
        transform.Find("Mask").Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetResourceSprite(newResource);
    }

    public void ProductionClick()
    {
        onProductionClick.Invoke(resource);
    }

    public void Unlock()
    {
        locked = false;
    }
}
