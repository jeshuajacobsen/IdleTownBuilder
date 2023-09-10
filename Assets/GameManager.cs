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

    public UnityEvent<string, int> onResourcesChanged;
    public UnityEvent<string, int> onResourcesAdded;

    public Dictionary<string, int> resources = new Dictionary<string, int>();

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
        onResourcesChanged = new UnityEvent<string, int>();
        onResourcesAdded = new UnityEvent<string, int>();
        UpdateCoinsText();
        AddResources("Wood", 1);
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
            onResourcesAdded.Invoke(resourceName, quantity);
            resources.Add(resourceName, quantity);
        }
    }

    public void SubtractResources(string resourceName, int quantity)
    {
        if (resources.ContainsKey(resourceName))
        {
            resources[resourceName] -= quantity;
            onResourcesChanged.Invoke(resourceName, quantity);
        } else {
            resources.Add(resourceName, 0);
        }
        if (quantity < 0) 
        {
            quantity = 0;
        }
        
    }
}
