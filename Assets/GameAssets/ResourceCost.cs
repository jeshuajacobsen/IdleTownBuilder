using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceCost : MonoBehaviour
{
    public TextMeshProUGUI resourceName;
    public TextMeshProUGUI resourceCost;

    public int requiredAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.resources.ContainsKey(resourceName.text))
        {
            resourceCost.text =  "0/" + requiredAmount;
            resourceCost.color = Color.red;
        } 
        else 
        {
            resourceCost.text = "" + GameManager.instance.resources[resourceName.text] + "/" + requiredAmount;
            if (requiredAmount > GameManager.instance.resources[resourceName.text])
            {
                resourceCost.color = Color.red;
            } else {
                resourceCost.color = Color.green;
            }
        }
    }

    public void InitValues(string newResourceName, int newRequiredAmount)
    {
        requiredAmount = newRequiredAmount;
        resourceName.text = newResourceName;
        if (GameManager.instance.resources.ContainsKey(newResourceName))
        {
            resourceCost.text = "" + GameManager.instance.resources[newResourceName] + "/" + requiredAmount;

            if (requiredAmount > GameManager.instance.resources[newResourceName])
            {
                resourceCost.color = Color.red;
            } else {
                resourceCost.color = Color.green;
            }
        }
        else
        {
            resourceCost.text = "0/" + requiredAmount;
            resourceCost.color = Color.red;
        }
        transform.Find("Mask").Find("Image").GetComponent<Image>().sprite = SpriteManager.instance.GetResourceSprite(newResourceName);
    }
}
