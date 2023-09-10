using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SharpUI.Source.Common.UI.Elements.Loading;

public class Building : MonoBehaviour
{
    private int level = 1;
    private float productionTimer = 0;
    private const float requiredTime = 10;
    public string buildingName = "";
    public string resource = "";
    public int costForUpgrade = 1;

    [SerializeField] private LoadingBar loadingBar;
    public TextMeshProUGUI levelText;

    void Start()
    {
        levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        Button button = transform.Find("UpgradeButton").GetComponent<Button>();

        button.onClick.AddListener(LevelUp);

        InvokeRepeating("Tick", 1.0f, 1.0f);
    }

    void Update()
    {
        
    }

    public void initValues(string newName, string newResource)
    {
        buildingName = newName;
        resource = newResource;

        TextMeshProUGUI nameText = transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = buildingName;
    }

    public void LevelUp()
    {
        if (GameManager.instance.HasEnoughCoin(costForUpgrade))
        {
            level++;
            levelText.text = "Level: " + level;
            GameManager.instance.SubtractCoins(costForUpgrade);
            costForUpgrade++;
        }
    }

    private void Tick()
    {
        productionTimer++;
        if (productionTimer >= requiredTime)
        {
            productionTimer -= requiredTime;
            GameManager.instance.AddResources(resource, level);
        }
        loadingBar.UpdatePercentage(productionTimer / requiredTime * 100);
    }
}
