using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private int coins = 0;
    public TextMeshProUGUI coinsText;
    private int cityPrestige = 0;
    private int collectedPrestige = 0;
    [SerializeField] private TextMeshProUGUI cityPrestigeText;

    public UnityEvent<string, int> onResourcesChanged;
    public UnityEvent<string, int> onResourcesAdded;

    public Dictionary<string, int> resources = new Dictionary<string, int>();
    public Dictionary<string, int> productionTimers = new Dictionary<string, int>();

    
    public Dictionary<string, int> resourcePrices;

    public UnityEvent<string> resetCity;

    void Awake()
    {
        resourcePrices = new Dictionary<string, int>();
        resourcePrices["Wheat"] = 1;
        resourcePrices["Wood"] = 4;
        resourcePrices["Lumber"] = 10;

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
        UpdateCoinsText();
        UpdateCityPrestigeText();
        AddResources("Wheat", 1);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateCoinsText()
    {
        coinsText.text = "" + coins;
    }

    public void AddCoins(int quantity)
    {
        coins += quantity;
        UpdateCoinsText();
    }

    public void SubtractCoins(int quantity)
    {
        coins -= quantity;
        UpdateCoinsText();
    }

    void UpdateCityPrestigeText()
    {
        cityPrestigeText.text = "" + cityPrestige;
    }

    public void AddCityPrestige(int quantity)
    {
        cityPrestige += quantity;
        UpdateCityPrestigeText();
    }

    public void AddCollectedPrestige(int quantity)
    {
        collectedPrestige += quantity;
        GameObject.FindWithTag("CollectedPrestige").transform.GetComponent<TextMeshProUGUI>().text = "" + collectedPrestige;
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
        AddCollectedPrestige(cityPrestige);
        cityPrestige = 0;
        UpdateCityPrestigeText();
        resources = new Dictionary<string, int>();
        resetCity.Invoke(newCityName);
        AddResources("Wheat", 1);
    }
}
