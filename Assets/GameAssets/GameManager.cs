using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Numerics;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private BigInteger coins = 1;

    private BigInteger Coins
    {
        get { return coins; }
        set 
        { 
            coins = value; 
            coinsText.text = "" + GameManager.BigIntToExponentString(coins);
        }
    }
    public TextMeshProUGUI coinsText;
    public BigInteger cityPrestige = 0;
    private BigInteger CityPrestige
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

    public UnityEvent<string> resetCity;

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
        resourcePrices["Barrel"] = 160;
        resourcePrices["Wine"] = 160;

        //merfolk
        resourcePrices["Kelp"] = 200;
        resourcePrices["Coral"] = 500;
        resourcePrices["Fish"] = 2000;
        resourcePrices["Pearl"] = 8000;
        resourcePrices["Sand"] = 10000;
        resourcePrices["Merite Ore"] = 500000;
        resourcePrices["Crab"] = new BigInteger(Math.Pow(10, 8));
        resourcePrices["Magma Slug"] = new BigInteger(Math.Pow(10, 12));
        resourcePrices["Fire Slime"] = 8000;
        resourcePrices["Merite Ingot"] = 8000;

        //Dwarf
        resourcePrices["Mushroom"] = 200;
        resourcePrices["Mana"] = 600;
        resourcePrices["Coal"] = 2000;
        resourcePrices["Iron Ore"] = 8000;
        resourcePrices["Honey"] = 10000;
        resourcePrices["Iron Ingot"] = 1000000;
        resourcePrices["Mead"] = new BigInteger(Math.Pow(10, 9));

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

    // Start is called before the first frame update
    void Start()
    {
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
        int exponentBrackets = (int)(intString.Length / 3);
        exponentBrackets -= 2;
        if (exponentBrackets % 3 == 0)
        {
            exponentBrackets--;
        }
        return intString.Substring(0, 3) + "." + intString.Substring(3, 2) + ExponentLetters(exponentBrackets);
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

    public void StartNewCity(string newCityName)
    {
        AddCollectedPrestige(CityPrestige);
        CityPrestige = 0;
        resources = new Dictionary<string, BigInteger>();
        AddResources("Wheat", 1);
        Coins = 1;
        this.cityName = newCityName;
        resetCity.Invoke(newCityName);
        
    }

    public void PrepForSave(SaveData saveData)
    {
        saveData.coins = coins;
        saveData.cityPrestige = CityPrestige;
        saveData.collectedPrestige = CollectedPrestige;
        saveData.resources = resources;
        saveData.cityName = cityName;
    }

    public void LoadSavedData(SaveData saveData)
    {
        Coins = saveData.coins;
        CityPrestige = saveData.cityPrestige;
        CollectedPrestige = saveData.collectedPrestige;
        resources = saveData.resources;
        cityName = saveData.cityName;
    }

    public void StartNewGame()
    {
        Coins = 5;
        CityPrestige = 0;
        resources = new Dictionary<string, BigInteger>();
        AddResources("Wheat", 1);
        cityName = "Peasantry";
    }
}
