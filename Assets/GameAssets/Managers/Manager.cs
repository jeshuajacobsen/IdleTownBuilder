using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Codice.Client.Common;
using System.Linq;

public class Manager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public string Name
    {
        get { return nameText.text; }
        set { nameText.text = value; }
    }
    [SerializeField] private TextMeshProUGUI effect1Text;
    [SerializeField] private TextMeshProUGUI effect2Text;

    public string effect1Type;
    public string effect2Type;

    private int level;
    public int Level
    {
        get { return level; }
        set 
        { 
            transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Level: " + GetLevelFromExp(value);
            level = value; 
            SetExpProgress();
            
        }
    }

    public string rarity;

    public GameObject assignedBuildingMask;

    private Building assignedBuilding;
    public Building AssignedBuilding
    {
        get { return assignedBuilding; }
        set 
        { 
            assignedBuilding = value; 
            if (assignedBuilding != null)
            {
                assignedBuildingMask.SetActive(true);
                assignedBuildingMask.transform.Find("AssignedBuildingImage").GetComponent<Image>().sprite = SpriteManager.instance.GetBuildingSprite(assignedBuilding.buildingName);
                if (Name == "Aeris")
                {
                    TimeManager.instance.managerTime = 0;
                }
            } 
            else
            {
                assignedBuildingMask.SetActive(false);
            }
        }
    }

    private System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        Button button = transform.GetComponent<Button>();
        button.onClick.AddListener(() => {
            GameManager.instance.managersPanel.Close(this);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int ExpToNextLevel(int exp)
    {
        if (exp < 2)
        {
            return 2;
        } 
        else if (exp < 5)
        {
            return 3;
        }
        else if (exp < 10)
        {
            return 5;
        }
        else
        {
            return 10;
        }
    }

    private int GetLevelFromExp(int exp)
    {
        if (exp < 2)
        {
            return 1;
        } 
        else if (exp < 5)
        {
            return 2;
        }
        else if (exp < 10)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }

    private int GetRemainderExp(int exp)
    {
        if (exp < 2)
        {
            return exp;
        } 
        else if (exp < 5)
        {
            return exp - 2;
        }
        else if (exp < 10)
        {
            return exp - 5;
        }
        else
        {
            return exp - 10;
        }
    }

    private void SetExpProgress()
    {
        const int width = 110;
        RectTransform rectTransform = transform.Find("ExpPanel").Find("BarFill").GetComponent<RectTransform>();
        float desiredWidth = width * GetRemainderExp(Level) / ExpToNextLevel(Level);
        transform.Find("ExpPanel").Find("ProgressText").GetComponent<TextMeshProUGUI>().text = GetRemainderExp(Level) + "/" + ExpToNextLevel(Level);
        rectTransform.sizeDelta = new UnityEngine.Vector2(desiredWidth, rectTransform.sizeDelta.y);

        rectTransform.anchoredPosition = new UnityEngine.Vector2(-width / 2 + desiredWidth / 2, rectTransform.anchoredPosition.y);
    }

    public double GetEffectMagnitude(string effectType)
    {
        if (level < 3)
        {
            return .1f;
        } 
        else if (level >= 3 && level < 5) 
        {
            return .2f;
        }
        else if (level >= 5 && level < 8) 
        {
            return .3f;
        }
        else
        {
            return .4f;
        }
    }

    public void InitValues(string name, int level, string rarity = "")
    {
        this.rarity = rarity;
        this.Level = level;
        string[] commonNames = {"Wedge", "Biggs", "Barret"};
        string[] uncommonNames = {"Jessie", "Tifa"};
        string[] rareNames = {"Aeris", "Cloud"};

        if (commonNames.Contains(name))
        {
            rarity = "Common";
        }
        else if (uncommonNames.Contains(name))
        {
            rarity = "Uncommon";
        }
        else if (rareNames.Contains(name))
        {
            rarity = "Rare";
        }
        
        effect1Type = "";
        effect2Type = "";
        effect1Text.text = "";
        effect2Text.text = "";
        if (rarity == "Common")
        {
            transform.GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("ManagerBackgroundCommon");
            int index = random.Next(commonNames.Length);
            name = name == "" ? commonNames[index] : name;
            nameText.text = name;
            switch(name)
            {
                case "Wedge":
                    effect1Text.text = "10% production quantity";
                    effect1Type = "ProductionQuantity";
                    transform.Find("ProfileImage").GetComponent<Image>().sprite = SpriteManager.instance.GetManagerSprite("Wedge");
                    break;
                case "Biggs":
                    effect1Text.text = "10% less consumption";
                    effect1Type = "LessConsumption";
                    transform.Find("ProfileImage").GetComponent<Image>().sprite = SpriteManager.instance.GetManagerSprite("Biggs");
                    break;
                case "Barret":
                    effect1Text.text = "10% production speed";
                    effect1Type = "ProductionSpeed";
                    transform.Find("ProfileImage").GetComponent<Image>().sprite = SpriteManager.instance.GetManagerSprite("Biggs");
                    break;

            }
        }
        else if (rarity == "Uncommon")
        {
            transform.GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("ManagerBackgroundUncommon");
            int index = random.Next(uncommonNames.Length);
            name = name == "" ? uncommonNames[index] : name;
            nameText.text = name;
            switch(name)
            {
                case "Jessie":
                    effect1Text.text = "10% production quantity";
                    effect2Text.text = "10% production speed";
                    effect1Type = "ProductionQuantity";
                    effect2Type = "ProductionSpeed";
                    transform.Find("ProfileImage").GetComponent<Image>().sprite = SpriteManager.instance.GetManagerSprite("Jessie");
                    break;
                case "Tifa":
                    effect1Text.text = "10% less consumption";
                    effect2Text.text = "10% production speed";
                    effect1Type = "LessConsumption";
                    effect2Type = "ProductionSpeed";
                    transform.Find("ProfileImage").GetComponent<Image>().sprite = SpriteManager.instance.GetManagerSprite("Tifa");
                    break;

            }
        }
        else if (rarity == "Rare")
        {
            transform.GetComponent<Image>().sprite = SpriteManager.instance.GetInterfaceSprite("ManagerBackgroundRare");
            int index = random.Next(rareNames.Length);
            name = name == "" ? rareNames[index] : name;
            nameText.text = name;
            switch(name)
            {
                case "Aeris":
                    effect1Text.text = "Managed building gains 1 level every hour.";
                    effect1Type = "BuildingLevelUp";
                    transform.Find("ProfileImage").GetComponent<Image>().sprite = SpriteManager.instance.GetManagerSprite("Aeris");
                    break;
                case "Cloud":
                    effect1Text.text = "Managed building does not consume resources.";
                    effect1Type = "NoConsumption";
                    transform.Find("ProfileImage").GetComponent<Image>().sprite = SpriteManager.instance.GetManagerSprite("Cloud");
                    break;

            }
        }
        
    }
}
