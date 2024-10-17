using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;

public class Header : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI gemsText;
    [SerializeField] private TextMeshProUGUI cityPrestigeText;
    [SerializeField] private TextMeshProUGUI collectedPrestigeText;
    [SerializeField] private TextMeshProUGUI cityNameText;

    void Start()
    {
        GameManager.instance.OnCoinsChanged += UpdateCoinsText;
        GameManager.instance.OnGemsChanged += UpdateGemsText;
        GameManager.instance.OnCityPrestigeChanged += UpdateCityPrestigeText;
        GameManager.instance.OnCollectedPrestigeChanged += UpdateCollectedPrestigeText;
        GameManager.instance.OnCityNameChanged += UpdateCityNameText;

        UpdateCoinsText(GameManager.instance.Coins);
        UpdateGemsText(GameManager.instance.Gems);
        UpdateCityPrestigeText(GameManager.instance.CityPrestige);
        UpdateCollectedPrestigeText(GameManager.instance.CollectedPrestige);
        UpdateCityNameText(GameManager.instance.CityName);
    }

    void UpdateCoinsText(BigInteger coins)
    {
        if (coinsText != null)
            coinsText.text = GameManager.BigIntToExponentString(coins);
    }

    void UpdateGemsText(BigInteger gems)
    {
        if (gemsText != null)
            gemsText.text = GameManager.BigIntToExponentString(gems);
    }

    void UpdateCityPrestigeText(BigInteger prestige)
    {
        if (cityPrestigeText != null)
            cityPrestigeText.text = GameManager.BigIntToExponentString(prestige);
    }

    void UpdateCollectedPrestigeText(BigInteger prestige)
    {
        if (collectedPrestigeText != null)
            collectedPrestigeText.text = GameManager.BigIntToExponentString(prestige);
    }

    public void UpdateCityNameText(string cityName)
    {
        if (cityNameText != null)
            cityNameText.text = cityName;
    }
}
