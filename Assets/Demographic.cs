using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Demographic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI nameText;
    private int level = 1;
    private int costForUpgrade = 1;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitValues(string newName)
    {
        levelText.text = "Level: " + level;
        nameText.text = newName;

        transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().InitValues(newName);
        transform.Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "$" + costForUpgrade;
    }

    public void LevelUp()
    {
        if (GameManager.instance.HasEnoughCoin(costForUpgrade))
        {
            level++;
            levelText.text = "Level: " + level;
            GameManager.instance.SubtractCoins(costForUpgrade);
            costForUpgrade++;
            transform.Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "$" + costForUpgrade;
            foreach (Requirement requirement in transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().requirements)
            {
                requirement.level = level;
            }
        }
    }

    public int GetPrestigeGenerated()
    {
        return level;
    }
}
