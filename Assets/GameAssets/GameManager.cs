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

    // Events to notify when properties change
    public event Action<BigInteger> OnCoinsChanged;
    public event Action<BigInteger> OnGemsChanged;
    public event Action<BigInteger> OnCityPrestigeChanged;
    public event Action<BigInteger> OnCollectedPrestigeChanged;
    public event Action<String> OnCityNameChanged;

    // Properties
    private BigInteger coins = 1;
    public BigInteger Coins
    {
        get { return coins; }
        set
        {
            coins = value;
            OnCoinsChanged?.Invoke(coins);
            TasksManager.instance?.CheckTasks("CoinGoal", "Coins", coins);
        }
    }

    private BigInteger gems = 0;
    public BigInteger Gems
    {
        get { return gems; }
        set
        {
            gems = value;
            OnGemsChanged?.Invoke(gems);
        }
    }

    private BigInteger cityPrestige = 0;
    public BigInteger CityPrestige
    {
        get { return cityPrestige; }
        set
        {
            cityPrestige = value;
            OnCityPrestigeChanged?.Invoke(cityPrestige);
        }
    }

    private BigInteger collectedPrestige = 0;
    public BigInteger CollectedPrestige
    {
        get { return collectedPrestige; }
        set
        {
            collectedPrestige = value;
            OnCollectedPrestigeChanged?.Invoke(collectedPrestige);
        }
    }

    private string cityName;
    public string CityName
    {
        get { return cityName; }
        set
        {
            cityName = value;
            OnCityNameChanged?.Invoke(cityName);
        }
    }

    public UnityEvent<string, BigInteger> onResourcesChanged;
    public UnityEvent<string, BigInteger> onResourcesAdded;

    public Dictionary<string, BigInteger> resources = new Dictionary<string, BigInteger>();
    public Dictionary<string, double> productionTimers = new Dictionary<string, double>();

    public UnityEvent<string> resetCity = new UnityEvent<string>();

    public List<Manager> managers = new List<Manager>();
    public ManagersPanel managersPanel;
    public Manager ManagerPrefab;
    public Transform contentTransform;

    public BuildingContent buildingContent;
    public GameObject tabMarketContentGameObject;
    public PopulationContent popContent;
    public TabContentKingdom kingdomContent;
    public ResearchInfoPanel kingdomSelectedResearch;
    public CityResearchContent cityResearchContent;

    void Awake()
    {

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
        return gameData.resourcePrices[resourceName] * multiplier / 100;
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

    public static string BigIntToExponentString(BigInteger number)
    {
        // Define thresholds and suffixes
        // You can add more scales as needed (e.g., 't' for trillion)
        var scales = new (BigInteger threshold, BigInteger divisor, string suffix)[]
        {
            (new BigInteger(1_000_000_000_000_000_000), new BigInteger(1_000_000_000_000_000_000), "Q"),
            (new BigInteger(1_000_000_000_000_000), new BigInteger(1_000_000_000_000_000), "q"),
            (new BigInteger(1_000_000_000_000), new BigInteger(1_000_000_000_000), "t"),
            (new BigInteger(1_000_000_000), new BigInteger(1_000_000_000), "b"),
            (new BigInteger(1_000_000), new BigInteger(1_000_000), "m"),
            (new BigInteger(1_000), new BigInteger(1_000), "k")
        };

        // If less than 1000, just return the number
        if (number < 1000)
        {
            return number.ToString();
        }

        // Find the appropriate scale
        foreach (var (threshold, divisor, suffix) in scales)
        {
            if (number >= threshold)
            {
                double scaledValue = (double)number / (double)divisor;
                return FormatToThreeSignificantDigits(scaledValue) + suffix;
            }
        }

        // If it's at least 1,000 but less than 1,000,000 (k scale)
        double val = (double)number / 1000.0;
        return FormatToThreeSignificantDigits(val) + "k";
    }

    private static string FormatToThreeSignificantDigits(double value)
    {
        // value will be at least 1 and less than 1000 because of how we're scaling
        // Determine how many decimals we need based on the integer part's length
        int intPart = (int)value;

        string format;
        if (intPart < 10)
        {
            // e.g. value = 1.11 -> need two decimal places to get total 3 sig digits
            // 1.11 has three digits: '1', '1', '1'
            format = "0.00";
        }
        else if (intPart < 100)
        {
            // e.g. value = 12.3 -> '12.3' is 3 digits total: '1','2','3'
            format = "0.0";
        }
        else
        {
            // e.g. value = 100 -> '100' is already 3 digits: '1','0','0'
            format = "0";
        }

        // Format the number using the chosen format
        return value.ToString(format);
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

    public static BigInteger GrowthFunction(int level, BigInteger baseCost)
    {
        if (level == 0)
        {
            return baseCost;
        }
        //return baseCost * (Pow(level, 3) + Pow(level, 2) + level);
        return baseCost * Pow(2, level);
    }

    public static BigInteger ResearchGrowthFunction(int level, BigInteger baseCost)
    {
        if (level == 0)
        {
            return baseCost;
        }
        return baseCost * (10 * Pow(level, 3) + 10 * Pow(level, 2) + level);
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
        if (manager != null && manager.AssignedBuilding != null)
        {
            manager.AssignedBuilding.LevelUp();
        }
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
        this.CityName = newCityName;
        resetCity.Invoke(newCityName);
        TasksManager.instance.SetupCityTasks(newCityName);
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
        CityName = saveData.cityName;
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
        CollectedPrestige = 1000;
        Coins = 5;
        Gems = 0;
        CityPrestige = 0;
        resources = new Dictionary<string, BigInteger>();
        AddResources("Wheat", 1);
        CityName = "Peasantry";
    }
}
