using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private int coins = 1;

    private int Coins
    {
        get { return coins; }
        set 
        { 
            coins = value; 
            coinsText.text = "" + coins;
        }
    }
    public TextMeshProUGUI coinsText;
    public int cityPrestige = 0;
    private int CityPrestige
    {
        get { return cityPrestige; }
        set 
        { 
            cityPrestige = value; 
            cityPrestigeText.text = "" + cityPrestige;
        }
    }
    private int collectedPrestige = 0;
    public string cityName;
    [SerializeField] private TextMeshProUGUI cityPrestigeText;

    public UnityEvent<string, int> onResourcesChanged;
    public UnityEvent<string, int> onResourcesAdded;

    public Dictionary<string, int> resources = new Dictionary<string, int>();
    public Dictionary<string, float> productionTimers = new Dictionary<string, float>();

    
    public Dictionary<string, int> resourcePrices;

    public UnityEvent<string> resetCity;

    void Awake()
    {
        resourcePrices = new Dictionary<string, int>();
        resourcePrices["Wheat"] = 1;
        resourcePrices["Wood"] = 4;
        resourcePrices["Clay"] = 10;
        resourcePrices["Lumber"] = 100;
        resourcePrices["Pottery"] = 160;
        resourcePrices["Vegetables"] = 200;
        resourcePrices["Copper Ore"] = 700;
        resourcePrices["Tin Ore"] = 700;
        resourcePrices["Flour"] = 600;
        resourcePrices["Bronze Ingot"] = 4000;
        resourcePrices["Bread"] = 2000;
        resourcePrices["Furniture"] = 5000;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        onResourcesChanged = new UnityEvent<string, int>();
        onResourcesAdded = new UnityEvent<string, int>();
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

    public void AddCoins(int quantity)
    {
        Coins = Coins + quantity;
    }

    public void SubtractCoins(int quantity)
    {
        Coins = Coins - quantity;
    }

    public void AddCityPrestige(float quantity)
    {
        CityPrestige += (int)quantity;
    }

    public void AddCollectedPrestige(int quantity)
    {
        collectedPrestige += quantity;
        if ( GameObject.FindWithTag("CollectedPrestige") != null)
        {
           GameObject.FindWithTag("CollectedPrestige").transform.GetComponent<TextMeshProUGUI>().text = "" + collectedPrestige;
        }
    }

    public void SubtractCollectedPrestige(int quantity)
    {
        collectedPrestige -= quantity;
        GameObject.FindWithTag("CollectedPrestige").transform.GetComponent<TextMeshProUGUI>().text = "" + collectedPrestige;
    }

    public bool HasEnoughCoin(int amount)
    {
        return coins >= amount;
    }

    public bool HasEnoughPrestige(int amount)
    {
        return collectedPrestige >= amount;
    }

    public void AddResources(string resourceName, int quantity)
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

    public void SubtractResources(string resourceName, int quantity)
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
        resources = new Dictionary<string, int>();
        AddResources("Wheat", 1);
        Coins = 1;
        this.cityName = newCityName;
        resetCity.Invoke(newCityName);
        
    }

    public void PrepForSave(SaveData saveData)
    {
        saveData.coins = coins;
        saveData.cityPrestige = CityPrestige;
        saveData.collectedPrestige = collectedPrestige;
        saveData.resources = resources;
        saveData.cityName = cityName;
    }

    public void LoadSavedData(SaveData saveData)
    {
        Coins = saveData.coins;
        CityPrestige = saveData.cityPrestige;
        collectedPrestige = saveData.collectedPrestige;
        resources = saveData.resources;
        cityName = saveData.cityName;
    }

    public void StartNewGame()
    {
        Coins = 5;
        CityPrestige = 0;
        resources = new Dictionary<string, int>();
        AddResources("Wheat", 1);
        cityName = "Peasantry";
    }
}
