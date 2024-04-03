using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Numerics;

public class GameManager : MonoBehaviour
{
    public GameData gameData = new GameData();
    private const int managerPrice = 50;
    public static GameManager instance;

    private BigInteger coins = 1;

    public BigInteger Coins
    {
        get { return coins; }
        set 
        { 
            coins = value; 
            coinsText.text = "" + GameManager.BigIntToExponentString(coins);
            TasksManager.instance.CheckTasks("CoinGoal", "Coins", coins);
        }
    }

    private BigInteger gems = 0;
    private BigInteger Gems
    {
        get { return gems; }
        set 
        { 
            gems = value; 
            gemsText.text = "" + GameManager.BigIntToExponentString(gems);
        }
    }
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI gemsText;
    private  BigInteger cityPrestige = 0;
    public BigInteger CityPrestige
    {
        get { return cityPrestige; }
        set 
        { 
            cityPrestige = value; 
            cityPrestigeText.text = "" + GameManager.BigIntToExponentString(cityPrestige);
        }
    }
    private BigInteger collectedPrestige = 0;
    public BigInteger CollectedPrestige
    {
        get { return collectedPrestige; }
        set 
        { 
            collectedPrestige = value; 
            collectedPrestigeText.text = "" + GameManager.BigIntToExponentString(collectedPrestige);
        }
    }
    [SerializeField] private TextMeshProUGUI collectedPrestigeText;
    public string cityName;
    [SerializeField] private TextMeshProUGUI cityPrestigeText;

    public UnityEvent<string, BigInteger> onResourcesChanged;
    public UnityEvent<string, BigInteger> onResourcesAdded;

    public Dictionary<string, BigInteger> resources = new Dictionary<string, BigInteger>();
    public Dictionary<string, double> productionTimers = new Dictionary<string, double>();

    
    public Dictionary<string, BigInteger> resourcePrices;

    public UnityEvent<string> resetCity = new UnityEvent<string>();

    public List<Manager> managers = new List<Manager>();
    public ManagersPanel managersPanel;
    public Manager ManagerPrefab;
    public Transform contentTransform;

    public BuildingContent buildingContent;
    public GameObject tabMarketContentGameObject;
    public PopulationContent popContent;
    public string currentCity;
    public TextMeshProUGUI cityNameText;

    void Awake()
    {
        resourcePrices = new Dictionary<string, BigInteger>();
        resourcePrices["Wheat"] = 1;
        resourcePrices["Wood"] = 4;
        resourcePrices["Clay"] = 10;
        resourcePrices["Lumber"] = 100;
        resourcePrices["Pottery"] = 160;
        resourcePrices["Stone"] = 300;
        resourcePrices["Vegetables"] = 400;
        resourcePrices["Hemp"] = 400;
        resourcePrices["Clothes"] = 600;
        resourcePrices["Copper Ore"] = 700;
        resourcePrices["Fruit"] = 900;
        resourcePrices["Tin Ore"] = 9000;
        resourcePrices["Bronze Ingot"] = 100000;
        resourcePrices["Flour"] = new BigInteger(Math.Pow(10, 7));
        resourcePrices["Bread"] = new BigInteger(Math.Pow(10, 10));
        resourcePrices["Grapes"] = 160;
        resourcePrices["Furniture"] = 5000;
        resourcePrices["Cow"] = 5000;
        resourcePrices["Milk"] = 5000;
        resourcePrices["Barrel"] = 160;
        resourcePrices["Wine"] = 160;
        resourcePrices["Beef"] = 500;
        resourcePrices["Leather"] = 5000;
        resourcePrices["Paper"] = 5000;
        resourcePrices["Leather Clothes"] = 5000;
        resourcePrices["Human Jewelry"] = 5000;
        resourcePrices["High Arcana "] = 5000;

        //merfolk
        resourcePrices["Kelp"] = 200;
        resourcePrices["Coral"] = 500;
        resourcePrices["Fish"] = 2000;
        resourcePrices["Reed"] = 4000;
        resourcePrices["Pearl"] = 8000;
        resourcePrices["Basket"] = 9000;
        resourcePrices["Sand"] = 10000;
        resourcePrices["Manatee"] = 10000;
        resourcePrices["Merite Ore"] = 50000;
        //resourcePrices["Milk"] = 10000;
        resourcePrices["Eye Of Newt"] = 10000;
        resourcePrices["Ink"] = 10000;
        resourcePrices["Curse"] = 100000;
        resourcePrices["Crab"] = new BigInteger(Math.Pow(10, 8));
        resourcePrices["Magma Slug"] = new BigInteger(Math.Pow(10, 12));
        resourcePrices["Rice"] = 10000;
        resourcePrices["Fire Slime"] = 8000;
        resourcePrices["Merite Ingot"] = 8000;
        resourcePrices["Mer Jewelry"] = 10000;
        resourcePrices["Mermail"] = 10000;
        resourcePrices["Trident"] = 10000;

        //Dwarf
        resourcePrices["Mushroom"] = 200;
        resourcePrices["Mana"] = 600;
        resourcePrices["Coal"] = 2000;
        resourcePrices["Iron Ore"] = 8000;
        resourcePrices["Honey"] = 10000;
        resourcePrices["Iron Ingot"] = 1000000;
        resourcePrices["Mead"] = new BigInteger(Math.Pow(10, 9));
        resourcePrices["Mechanical Parts"] = 10000;
        resourcePrices["Tools"] = 10000;
        resourcePrices["Hops"] = 10000;
        resourcePrices["Gold Ore"] = 10000;
        resourcePrices["Glass"] = 10000;
        resourcePrices["Beer"] = 10000;
        resourcePrices["Gold Ingot"] = 10000;
        resourcePrices["Artifact"] = 10000;
        resourcePrices["Jewel"] = 10000;
        resourcePrices["Book"] = 10000;
        resourcePrices["Golem"] = 10000;
        resourcePrices["Rune"] = 10000;
        resourcePrices["Mithril Ore"] = 10000;
        resourcePrices["Mithril Ingot"] = 10000;
        resourcePrices["Mithril Armor"] = 10000;

        //Fairy
        resourcePrices["Flowers"] = 10000;
        resourcePrices["Magic Mushroom"] = 10000;
        resourcePrices["Berry"] = 10000;
        resourcePrices["Fairy Dust"] = 10000;
        //resourcePrices["Mana"] = 10000;
        resourcePrices["Shoes"] = 10000;
        resourcePrices["Luck Charm"] = 10000;
        resourcePrices["Beauty Charm"] = 10000;
        resourcePrices["Light Bulb"] = 10000;
        resourcePrices["Tea"] = 10000;
        resourcePrices["Silk"] = 10000;
        resourcePrices["Fairyelium"] = 10000;
        resourcePrices["Whisper Silk"] = 10000;
        resourcePrices["Fairy Crystal"] = 10000;
        resourcePrices["Invisibility Cloak"] = 10000;
        resourcePrices["Fairy Jewelry"] = 10000;

        //elf
        resourcePrices["Anima Fruit"] = 10000;
        resourcePrices["Living Wood"] = 10000;
        resourcePrices["Egg"] = 10000;
        resourcePrices["Cotton"] = 10000;
        resourcePrices["Unicorn Hair"] = 10000;
        resourcePrices["Cotton Clothes"] = 10000;
        resourcePrices["Chicken"] = 10000;
        resourcePrices["Enchantment"] = 10000;
        resourcePrices["Sugar"] = 10000;
        resourcePrices["Cake"] = 10000;
        resourcePrices["Wand"] = 10000;
        resourcePrices["Coffee"] = 10000;
        resourcePrices["Dark Wood"] = 10000;
        resourcePrices["Liqour"] = 10000;
        resourcePrices["Life Gem"] = 10000;
        resourcePrices["Elvish Jewelry"] = 10000;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        onResourcesChanged = new UnityEvent<string, BigInteger>();
        onResourcesAdded = new UnityEvent<string, BigInteger>();
    }

    public BigInteger GetResourcePrice(string resourceName)
    {
        BigInteger multiplier = 100;
        multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Marketing") ? new BigInteger(ResearchManager.instance.prestigeResearchLevels["Marketing"] * 100 * .1f) : 0;
        multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Economics") ? new BigInteger(ResearchManager.instance.scienceResearchLevels["Economics"] * 100 * .1f) : 0;
        multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Market") ? new BigInteger(ResearchManager.instance.scienceResearchLevels["Market"] * 100 * .1f) : 0;
        return resourcePrices[resourceName] * multiplier / 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        tabMarketContentGameObject.SetActive(true);
        tabMarketContentGameObject.SetActive(false);
        StartNewCity("Peasantry");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string BigIntToExponentString(BigInteger bigInteger)
    {
        string intString = bigInteger.ToString();
        if (intString.Length <= 3)
        {
            return intString;
        } else if (intString.Length <= 6) {
            if (intString.Length == 6) 
            {
                return intString.Substring(0, 3) + "," + intString.Substring(3, 3);
            }
            return intString.Substring(0, intString.Length % 3) + "," + intString.Substring(intString.Length % 3, 3);
        }
        int exponentBrackets = (int)((intString.Length - 6) / 3);
        if (intString.Length % 3 == 1)
        {
            return intString.Substring(0, 1) + "." + intString.Substring(1, 2) + ExponentLetters(exponentBrackets);
        } else if (intString.Length % 3 == 2) {
            return intString.Substring(0, 2) + "." + intString.Substring(2, 1) + ExponentLetters(exponentBrackets);
        } else {
            return intString.Substring(0, 3) + ExponentLetters(exponentBrackets);
        }
    }

    private static string ExponentLetters(int exponentBrackets)
    {
        string[] letters = new string[] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", 
            "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
        return letters[exponentBrackets / letters.Length] + letters[exponentBrackets % letters.Length];
    }

    public static BigInteger Pow(BigInteger baseValue, int exponent)
    {
        if (exponent < 0)
            throw new ArgumentException("Exponent cannot be negative", nameof(exponent));

        BigInteger result = BigInteger.One;
        while (exponent > 0)
        {
            if ((exponent & 1) == 1)
                result *= baseValue;

            baseValue *= baseValue;
            exponent >>= 1;
        }

        return result;
    }

    public void AddCoins(BigInteger quantity)
    {
        Coins = Coins + quantity;
    }

    public void SubtractCoins(BigInteger quantity)
    {
        Coins = Coins - quantity;
    }

    public void AddCityPrestige(BigInteger quantity)
    {
        CityPrestige += quantity;
    }

    public void AddCollectedPrestige(BigInteger quantity)
    {
        CollectedPrestige += quantity;
    }

    public void SubtractCollectedPrestige(BigInteger quantity)
    {
        CollectedPrestige -= quantity;
    }

    public bool HasEnoughCoin(BigInteger amount)
    {
        return coins >= amount;
    }

    public bool HasEnoughPrestige(BigInteger amount)
    {
        return CollectedPrestige >= amount;
    }

    public void AddResources(string resourceName, BigInteger quantity)
    {
        if (resources.ContainsKey(resourceName))
        {
            resources[resourceName] += quantity;
            onResourcesChanged.Invoke(resourceName, resources[resourceName]);
        } else {
            resources.Add(resourceName, quantity);
            onResourcesAdded.Invoke(resourceName, quantity);
        }
    }

    public void SubtractResources(string resourceName, BigInteger quantity)
    {
        if (resources.ContainsKey(resourceName))
        {
            resources[resourceName] -= quantity;
            onResourcesChanged.Invoke(resourceName, resources[resourceName]);
        } else {
            resources.Add(resourceName, 0);
        }
        if (resources[resourceName] < 0) 
        {
            resources[resourceName] = 0;
        }
        
    }

    public void PurchaseGems(int quantity, int cost)
    {
        Gems += quantity;
    }

    public Manager PurchaseManager()
    {
        Gems -= managerPrice;
        string rarity = "Common";
        if (UnityEngine.Random.Range(0, 100) < 15)
        {
            rarity = "Rare";
        } else if (UnityEngine.Random.Range(0, 100) < 40)
        {
            rarity = "Uncommon";
        } else
        {
            rarity = "Common";
        }
        
        Manager manager = Instantiate(ManagerPrefab, contentTransform);
        manager.InitValues("", 0, rarity);
        Manager existingManager = managers.Find((currentManager) => {return manager.nameText.text == currentManager.nameText.text;});
        if (existingManager != null)
        {
            existingManager.Level++;
            return existingManager;
        }
        else
        {
            managers.Add(manager);
            managersPanel.AddManagerToView(manager);
            return manager;
        }
       
    }

    public Manager AddManager(string name)
    {
        //TODO charge gems
        Manager manager = Instantiate(ManagerPrefab, contentTransform);
        manager.InitValues(name, 0, "");
        Manager existingManager = managers.Find((currentManager) => {return manager.nameText.text == currentManager.nameText.text;});
        if (existingManager != null)
        {
            existingManager.Level++;
            return existingManager;
        }
        else
        {
            managers.Add(manager);
            managersPanel.AddManagerToView(manager);
            return manager;
        }
       
    }

    public void ManagerLevelUpTriggered()
    {
        Manager manager = managers.Find((manager) => {return manager.Name == "Aeris";});
        manager.AssignedBuilding.LevelUp();
    }

    public void UnassignManagerFromOtherBuilding(Manager manager)
    {
        buildingContent.UnassignManagerFromOtherBuilding(manager);
    }

    public void StartNewCity(string newCityName)
    {
        AddCollectedPrestige(CityPrestige);
        CityPrestige = 0;
        resources = new Dictionary<string, BigInteger>();
        AddResources("Wheat", 1);
        Coins = 1;
        this.cityName = newCityName;
        cityNameText.text = newCityName;
        resetCity.Invoke(newCityName);
        currentCity = newCityName;
    }

    public void PrepForSave(SaveData saveData)
    {
        saveData.coins = Coins;
        saveData.gems = Gems;
        saveData.cityPrestige = CityPrestige;
        saveData.collectedPrestige = CollectedPrestige;
        saveData.resources = resources;
        saveData.cityName = cityName;

        saveData.SetManagerSaveData(managers);
    }

    public void LoadSavedData(SaveData saveData)
    {
        Coins = saveData.coins;
        Gems = saveData.gems;
        CityPrestige = saveData.cityPrestige;
        CollectedPrestige = saveData.collectedPrestige;
        resources = saveData.resources;
        cityName = saveData.cityName;
        cityNameText.text = saveData.cityName;
        currentCity = cityName;
        foreach (string key in saveData.managerLevels.Keys)
        {
            Manager manager = Instantiate(ManagerPrefab, contentTransform);
            manager.InitValues(key, saveData.managerLevels[key]);
            managers.Add(manager);
            managersPanel.AddManagerToView(manager);
        }
    }

    public void StartNewGame()
    {
        CollectedPrestige = 0;
        Coins = 5;
        Gems = 0;
        CityPrestige = 0;
        resources = new Dictionary<string, BigInteger>();
        AddResources("Wheat", 1);
        cityName = "Peasantry";
        cityNameText.text = cityName;
        currentCity = cityName;
    }
}
