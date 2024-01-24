using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI effect1Text;
    [SerializeField] private TextMeshProUGUI effect2Text;

    public string effect1Type;
    public string effect2Type;

    public int level;

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

    public double GetEffectMagnitude(string effectType)
    {
        return level * .1f;
    }

    public void InitValues(string name, int level)
    {
        this.level = level;
        string[] names = {"Wedge", "Biggs"};
        int index = random.Next(names.Length); // Generates a random index within the bounds of the array

        name = name == "" ? names[index] : name;
        nameText.text = name;
        switch(name)
        {
            case "Wedge":
                effect1Text.text = "10% Production";
                effect2Text.text = "10% Speed";
                effect1Type = "ProductionQuantity";
                effect2Type = "ProductionSpeed";
                break;
            case "Biggs":
                effect1Text.text = "10% Less Consumption";
                effect2Text.text = "10% Speed";
                effect1Type = "LessConsumption";
                effect2Type = "ProductionSpeed";
                break;

        }
    }
}
