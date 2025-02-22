using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;

public class TimeAwayPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TimeManager.instance.timeAwayShowing.AddListener(HandleTimeAway);
        TimeManager.instance.timeAwayHidden.AddListener(Close);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void Close(bool close)
    {
        transform.parent.gameObject.SetActive(false);
    }

    void Close()
    {
        transform.parent.gameObject.SetActive(false);
    }

    private void HandleTimeAway(int secondsAway, Dictionary<string, BigInteger> gainedResources, BigInteger gainedPrestige)
    {
        transform.Find("AwayTime").GetComponent<TextMeshProUGUI>().text = "You were away for " + (int)(secondsAway / 60) + " minutes";
        transform.Find("PrestigeGained").GetComponent<TextMeshProUGUI>().text = "Prestige Gained: " + gainedPrestige;
        transform.parent.gameObject.SetActive(true);
    }
}
