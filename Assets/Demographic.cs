using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Demographic : MonoBehaviour, Unlockable
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI nameText;
    private int level = 1;
    private int unlockCost = 1;
    private int baseCost = 1;


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
        transform.Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "$" + CalculateCost();
    }

    public void LevelUp()
    {
        int cost = CalculateCost();
        if (GameManager.instance.HasEnoughCoin(cost))
        {
            level++;
            levelText.text = "Level: " + level;
            GameManager.instance.SubtractCoins(cost);
            transform.Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "$" + CalculateCost();
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

    public void Unlock()
    {
        transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().Unlock();
    }

    public int GetUnlockCost()
    {
        return unlockCost;
    }

    public int CalculateCost()
    {
        return (int)(baseCost * level * 1.6);
    }

    public void Tick()
    {
        transform.Find("ConsumptionPanel").GetComponent<ConsumptionPanel>().Tick();
    }
}
