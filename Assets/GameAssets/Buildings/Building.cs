using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using SharpUI.Source.Common.UI.Elements.Loading;
using System.Numerics;

public class Building : MonoBehaviour, Unlockable
{
    private int level = 0;
    public int Level
    {
        get { return level; }
        set 
        { 
            level = value; 
            levelText.text = "Level: " + level;
            transform.Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = 
                "$" + GameManager.BigIntToExponentString(CalculateCost());
        }
    }
    public ProductionInput inputResourceButton1;
    public ProductionInput inputResourceButton2;
    public ProductionInput inputResourceButton3;
    public ProductionOutput outputResourceButton;

    public string buildingName = "";
    public bool locked = true;
    private BigInteger baseCost = 1;
    private string category = "Gathering";

    public string race = "Human";

    public TextMeshProUGUI levelText;

    public UnityEvent<string> onProductionClick;

    public Canvas canvas;

    private Manager manager;
    public Manager Manager
    {
        get { return manager; }
        set 
        { 
            this.manager = value;
            if (this.manager != null)
            {
                transform.Find("ManagerButton").Find("Text").GetComponent<TextMeshProUGUI>().text = manager.nameText.text;
            } else {
                transform.Find("ManagerButton").Find("Text").GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    void Awake()
    {
        onProductionClick = new UnityEvent<string>();
    }

    void Start()
    {
        levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        Button button = transform.Find("UpgradeButton").GetComponent<Button>();
        button.onClick.AddListener(LevelUp);
        transform.Find("UpgradeButton").Find("ButtonText").GetComponent<TextMeshProUGUI>().text = 
            "$" + GameManager.BigIntToExponentString(CalculateCost());

        inputResourceButton1.onProductionClick.AddListener(ProductionClick);
        inputResourceButton2.onProductionClick.AddListener(ProductionClick);
        inputResourceButton3.onProductionClick.AddListener(ProductionClick);
        outputResourceButton.onProductionClick.AddListener(ProductionClick);


        transform.Find("ManagerButton").GetComponent<Button>().onClick.AddListener(OpenManagerPanel);

        transform.Find("Mask").Find("BuildingImage").GetComponent<Image>().sprite = SpriteManager.instance.GetBuildingSprite(buildingName);
    }

    void Update()
    {
        
    }

    public void InitValues(string newName)
    {
        buildingName = newName;
        GameObject arrow1 = transform.Find("ProductionDisplay").Find("Arrow1").gameObject;
        GameObject arrow2 = transform.Find("ProductionDisplay").Find("Arrow2").gameObject;
        GameObject arrow3 = transform.Find("ProductionDisplay").Find("Arrow3").gameObject;

        BuildingData buildingData = GameManager.instance.gameData.GetBuildingData(newName);
        string[] inputResources = {};
        string outputResource = "";
        int productionTime = 10;

        switch(newName)
        {
            case "Farm":
                Unlock();
                outputResource = "Wheat";
                baseCost = 1;
                productionTime = 10;
                category = "Farm";
                break;
            case "Forester":
                outputResource = "Wood";
                baseCost = 6;
                productionTime = 12;
                category = "Gathering";
                break;
            case "Clay Pit":
                outputResource = "Clay";
                baseCost = 10;
                productionTime = 14;
                category = "Gathering";
                break;
            case "Lumber Mill":
                outputResource = "Lumber";
                inputResources = new string[] {"Wood"};
                baseCost = 200;
                productionTime = 16;
                category = "Processing";
                break;
            case "Potter":
                outputResource = "Pottery";
                inputResources = new string[] {"Clay"};
                baseCost = 600;
                productionTime = 18;
                category = "Crafting";
                break;
            case "Stone Quarry":
                outputResource = "Stone";
                inputResources = new string[] {};
                baseCost = 1800;
                productionTime = 22;
                category = "Gathering";
                break;
            case "Vegetable Farm":
                outputResource = "Vegetables";
                inputResources = new string[] {};
                baseCost = 6000;
                productionTime = 24;
                category = "Farm";
                break;
            case "Hemp Farm":
                outputResource = "Hemp";
                inputResources = new string[] {};
                baseCost = 20000;
                productionTime = 24;
                category = "Farm";
                break;
            case "Weaver":
                outputResource = "Clothes";
                inputResources = new string[] {"Hemp"};
                baseCost = 80000;
                productionTime = 24;
                category = "Crafting";
                break;
            case "Copper Mine":
                outputResource = "Copper Ore";
                inputResources = new string[] {};
                baseCost = 320000;
                productionTime = 28;
                category = "Mine";
                break;
            case "Orchard":
                outputResource = "Fruit";
                inputResources = new string[] {};
                baseCost = 1000000;
                productionTime = 30;
                category = "Farm";
                break;
            case "Tin Mine":
                outputResource = "Tin Ore";
                inputResources = new string[] {};
                baseCost = 5000000;
                productionTime = 28;
                category = "Mine";
                break;
            case "Smelter":
                outputResource = "Bronze Ingot";
                inputResources = new string[] {"Copper Ore", "Tin Ore", "Wood"};
                baseCost = 25000000;
                productionTime = 30;
                category = "Processing";
                break;
            case "Wind Mill":
                outputResource = "Flour";
                inputResources = new string[] {"Wheat"};
                baseCost = new BigInteger(Math.Pow(10, 7));
                productionTime = 26;
                category = "Processing";
                break;
            case "Bakery":
                outputResource = "Bread";
                inputResources = new string[] {"Flour"};
                baseCost = new BigInteger(Math.Pow(10, 8));;
                productionTime = 30;
                category = "Crafting";
                break;
            case "Vineyard":
                outputResource = "Grapes";
                inputResources = new string[] {};
                baseCost = new BigInteger(Math.Pow(10, 9));
                productionTime = 32;
                category = "Farm";
                break;
            case "Furniture Factory":
                outputResource = "Furniture";
                inputResources = new string[] {"Lumber", "Bronze Ingot"};
                baseCost = new BigInteger(Math.Pow(10, 10));
                productionTime = 32;
                category = "Manufacturing";
                break;
            case "Pasture":
                outputResource = "Cow";
                inputResources = new string[] {};
                baseCost = new BigInteger(Math.Pow(10, 11));
                productionTime = 32;
                category = "Farm";
                break;
            case "Dairy":
                outputResource = "Milk";
                inputResources = new string[] {"Cow"};
                baseCost = new BigInteger(Math.Pow(10, 12));
                productionTime = 32;
                category = "Processing";
                break;
            case "Barrel Maker":
                outputResource = "Barrel";
                inputResources = new string[] {"Lumber", "Iron Ingot"};
                baseCost = new BigInteger(Math.Pow(10, 13));
                productionTime = 32;
                category = "Manufacturing";
                break;
            case "Winery":
                outputResource = "Wine";
                inputResources = new string[] {"Grapes", "Barrel"};
                baseCost = new BigInteger(Math.Pow(10, 14));
                productionTime = 32;
                category = "Crafting";
                break;
            case "Butcher":
                outputResource = "Beef";
                inputResources = new string[] {"Cow"};
                baseCost = new BigInteger(Math.Pow(10, 15));
                productionTime = 32;
                category = "Processing";
                break;
            case "Tannery":
                outputResource = "Leather";
                inputResources = new string[] {"Cow"};
                baseCost = new BigInteger(Math.Pow(10, 16));
                productionTime = 32;
                category = "Processing";
                break;
            case "Paper Mill":
                outputResource = "Paper";
                inputResources = new string[] {"Wood"};
                baseCost = new BigInteger(Math.Pow(10, 17));
                productionTime = 32;
                category = "Manufacturing";
                break;
            case "Leather Shop":
                outputResource = "Leather Clothes";
                inputResources = new string[] {"Leather"};
                baseCost = new BigInteger(Math.Pow(10, 18));
                productionTime = 32;
                category = "Crafting";
                break;
            case "Human Jeweler":
                outputResource = "Human Jewelry";
                inputResources = new string[] {"Gold Ingot", "Jewel"};
                baseCost = new BigInteger(Math.Pow(10, 19));
                productionTime = 32;
                category = "Jeweler";
                break;
            case "Wizard University":
                outputResource = "High Arcana";
                inputResources = new string[] {"Mana", "Fairy Dust", "Life Gem"};
                baseCost = new BigInteger(Math.Pow(10, 20));
                productionTime = 32;
                category = "Magic";
                break;

                //merfolk
            case "Kelpery":
                outputResource = "Kelp";
                inputResources = new string[] {};
                baseCost = 2000;
                productionTime = 10;
                race = "Merfolk";
                category = "Farm";
                break;
            case "Reef":
                outputResource = "Coral";
                inputResources = new string[] {};
                baseCost = 4000;
                productionTime = 12;
                race = "Merfolk";
                category = "Gathering";
                break;
            case "Fishery":
                outputResource = "Fish";
                inputResources = new string[] {};
                baseCost = 8000;
                productionTime = 14;
                race = "Merfolk";
                category = "Gathering";
                break;
            case "Reeds":
                outputResource = "Reed";
                inputResources = new string[] {};
                baseCost = 24000;
                productionTime = 16;
                race = "Merfolk";
                category = "Gathering";
                break;
            case "Oystery":
                outputResource = "Pearl";
                inputResources = new string[] {};
                baseCost = 75000;
                productionTime = 18;
                race = "Merfolk";
                category = "Farm";
                break;
            case "Basket Weaver":
                outputResource = "Basket";
                inputResources = new string[] {"Reed"};
                baseCost = 300000;
                productionTime = 18;
                race = "Merfolk";
                category = "Crafting";
                break;
            case "Sand Banks":
                outputResource = "Sand";
                inputResources = new string[] {};
                baseCost = 9 * new BigInteger(Math.Pow(10, 5));
                productionTime = 20;
                race = "Merfolk";
                category = "Gathering";
                break;
            case "Manatee Pasture":
                outputResource = "Manatee";
                inputResources = new string[] {};
                baseCost = new BigInteger(Math.Pow(10, 6));
                productionTime = 20;
                race = "Merfolk";
                category = "Farm";
                break;
            case "Merite Cave":
                outputResource = "Merite Ore";
                inputResources = new string[] {};
                baseCost = new BigInteger(Math.Pow(10, 7));
                productionTime = 22;
                race = "Merfolk";
                category = "Mine";
                break;
            case "Manatee Dairy":
                outputResource = "Milk";
                inputResources = new string[] {"Manatee"};
                baseCost = 6 * new BigInteger(Math.Pow(10, 8));
                productionTime = 20;
                race = "Merfolk";
                category = "Processing";
                break;
            case "Newt Swamp":
                outputResource = "Eye Of Newt";
                inputResources = new string[] {};
                baseCost = 6 * new BigInteger(Math.Pow(10, 9));
                productionTime = 20;
                race = "Merfolk";
                category = "Gathering";
                break;
            case "Squid Traps":
                outputResource = "Ink";
                inputResources = new string[] {};
                baseCost = 6 * new BigInteger(Math.Pow(10, 10));
                productionTime = 20;
                race = "Merfolk";
                category = "Gathering";
                break;
            case "Witch Hut":
                outputResource = "Curse";
                inputResources = new string[] {"Eye Of Newt"};
                baseCost = 6 * new BigInteger(Math.Pow(10, 11));
                productionTime = 20;
                race = "Merfolk";
                category = "Magic";
                break;
            case "Crab Pots":
                outputResource = "Crab";
                inputResources = new string[] {"Reed"};
                baseCost = new BigInteger(Math.Pow(10, 12));
                productionTime = 26;
                category = "Gathering";
                break;
            case "Thermal Vents":
                outputResource = "Magma Slug";
                inputResources = new string[] {};
                baseCost = new BigInteger(Math.Pow(10, 13));;
                productionTime = 32;
                race = "Merfolk";
                category = "Gathering";
                break;
            case "Rice Patties":
                outputResource = "Rice";
                inputResources = new string[] {};
                baseCost = new BigInteger(Math.Pow(10, 14));
                productionTime = 26;
                category = "Farm";
                break;
            case "Slime Milker":
                outputResource = "Fire Slime";
                inputResources = new string[] {"Magma Slug"};
                baseCost = new BigInteger(Math.Pow(10, 15));;
                productionTime = 36;
                race = "Merfolk";
                category = "Processing";
                break;
            case "Aqua Forge":
                outputResource = "Merite Ingot";
                inputResources = new string[] {"Fire Slime", "Merite Ore"};
                baseCost = new BigInteger(Math.Pow(10, 16));;
                productionTime = 40;
                race = "Merfolk";
                category = "Processing";
                break;
            case "Aqua Jeweler":
                outputResource = "Mer Jewelry";
                inputResources = new string[] {"Fire Slime", "Merite Ore", "Pearl"};
                baseCost = new BigInteger(Math.Pow(10, 17));;
                productionTime = 40;
                race = "Merfolk";
                category = "Jeweler";
                break;
            case "Aqua Armorer":
                outputResource = "Mermail";
                inputResources = new string[] {"Fire Slime", "Merite Ingot"};
                baseCost = new BigInteger(Math.Pow(10, 18));;
                productionTime = 40;
                race = "Merfolk";
                category = "Crafting";
                break;
            case "Aqua Smith":
                outputResource = "Trident";
                inputResources = new string[] {"Fire Slime", "Merite Ingot"};
                baseCost = new BigInteger(Math.Pow(10, 19));;
                productionTime = 40;
                race = "Merfolk";
                category = "Crafting";
                break;

            //Dwarves
            case "Mushroom Cave":
                outputResource = "Mushroom";
                inputResources = new string[] {};
                baseCost = 200;
                productionTime = 10;
                race = "Dwarf";
                category = "Gathering";
                break;  
            case "Mana Well":
                outputResource = "Mana";
                inputResources = new string[] {};
                baseCost = 2000;
                productionTime = 12;
                race = "Dwarf";
                category = "Magic";
                break;
            case "Coal Mine":
                outputResource = "Coal";
                inputResources = new string[] {};
                baseCost = 20000;
                productionTime = 14;
                race = "Dwarf";
                category = "Mine";
                break;
            case "Iron Mine":
                outputResource = "Iron Ore";
                inputResources = new string[] {};
                baseCost = new BigInteger(Math.Pow(10, 6));
                productionTime = 18;
                race = "Dwarf";
                category = "Mine";
                break;
            case "Apiary":
                outputResource = "Honey";
                inputResources = new string[] {};
                baseCost = new BigInteger(Math.Pow(10, 7));
                productionTime = 18;
                race = "Dwarf";
                category = "Farm";
                break;
            case "Iron Smelter":
                outputResource = "Iron Ingot";
                inputResources = new string[] {"Coal", "Iron Ore"};
                baseCost = new BigInteger(Math.Pow(10, 10));
                productionTime = 18;
                race = "Dwarf"; 
                category = "Processing";
                break;
            case "Meadery":
                outputResource = "Mead";
                inputResources = new string[] {"Honey"};
                baseCost = new BigInteger(Math.Pow(10, 13));
                productionTime = 20;
                race = "Dwarf";
                category = "Crafting";
                break;
            case "Gear Works":
                outputResource = "Mechanical Parts";
                inputResources = new string[] {"Wood", "Iron Ingot"};
                baseCost = 3000000000;
                productionTime = 24;
                race = "Dwarf";
                category = "Manufacturing";
                break;
            case "Blacksmith":
                outputResource = "Tools";
                inputResources = new string[] {"Coal", "Iron Ore", "Wood"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                category = "Crafting";
                break;
            case "Hops Farm":
                outputResource = "Hops";
                inputResources = new string[] {};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                category = "Farm";
                break;
            case "Gold Mine":
                outputResource = "Gold Ore";
                inputResources = new string[] {"Tools"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                category = "Mine";
                break;
            case "Glass Blower":
                outputResource = "Glass";
                inputResources = new string[] {"Sand"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                category = "Crafting";
                break;                
            case "Brewery":
                outputResource = "Beer";
                inputResources = new string[] {"Wheat", "Hops", "Bottles"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                category = "Crafting";
                break;
            case "Gold Smelter":
                outputResource = "Gold Ingot";
                inputResources = new string[] {"Coal", "Iron Ore"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                category = "Processing";
                break;
            case "Artificer":
                outputResource = "Artifact";
                inputResources = new string[] {"Mechanical Parts", "Mana", "Gold"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                category = "Crafting";
                break;
            case "Jewel Mine":
                outputResource = "Jewel";
                inputResources = new string[] {};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                category = "Mine";
                break;
            case "Printing Press":
                outputResource = "Book";
                inputResources = new string[] {"Leather", "Paper", "Ink"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                category = "Manufacturing";
                break;
            case "Manufactory":
                outputResource = "Golem";
                inputResources = new string[] {"Mechanical Parts", "Mana", "Clay"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                category = "Manufacturing";
                break;  
            case "Rune Workshop":
                outputResource = "Rune";
                inputResources = new string[] {"Mana", "Stone"};
                baseCost = 50000;
                productionTime = 32;
                race = "Dwarf";
                category = "Magic";
                break;  
            case "Deep Mine":
                outputResource = "Mithril Ore";
                inputResources = new string[] {"Tools", "Golem"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                category = "Mine";
                break;
            case "Volcanic Forge":
                outputResource = "Mithril Ingot";
                inputResources = new string[] {};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                category = "Processing";
                break;
            case "Mithril Smith":
                outputResource = "Mithril Armor";
                inputResources = new string[] {"Mithril Ingot"};
                baseCost = 3000000000;
                productionTime = 18;
                race = "Dwarf";
                category = "Crafting";
                break;  

            //Fairies
            case "Garden":
                outputResource = "Flowers";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Farm";
                break;
            case "Fairy Circle":
                outputResource = "Magic Mushroom";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Magic";
                break;
            case "Berry Field":
                outputResource = "Berry";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Gathering";
                break;
            case "Dustery":
                outputResource = "Fairy Dust";
                inputResources = new string[] {"Magic Mushroom"};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Processing";
                break;       
            case "Mana Siphon":
                outputResource = "Mana";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Magic";
                break;
            case "Cobbler":
                outputResource = "Shoes";
                inputResources = new string[] {"Leather"};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Crafting";
                break;
            case "Rainbow":
                outputResource = "Luck Charm";
                inputResources = new string[] {"Mana", "Iron Ingot"};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Magic";
                break;
            case "Illusionist":
                outputResource = "Beauty Charm";
                inputResources = new string[] {"Mana", "Living Wood", "Fairy Dust"};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Magic";
                break;
            case "Luminous Garden":
                outputResource = "Light Bulb";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Farm";
                break;
            case "Tea Farm":
                outputResource = "Tea";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Farm";
                break;
            case "Worm Forest":
                outputResource = "Silk";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Gathering";
                break;
            case "Fairy Alchemist":
                outputResource = "Fairyelium";
                inputResources = new string[] {"Fairy Dust"};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Manufacturing";
                break;
            case "Spell Weaver":
                outputResource = "Whisper Silk";
                inputResources = new string[] {"Silk", "Mana", "Fairy Dust"};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Processing";
                break;
            case "Crystalizer":
                outputResource = "Fairy Crystal";
                inputResources = new string[] {"Fairyelium", "Mana"};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Processing";
                break;
            case "Fairy Seamstress":
                outputResource = "Invisibility Cloak";
                inputResources = new string[] {"Whisper Silk", "Mana"};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Crafting";
                break;
            case "Fairy Jeweler":
                outputResource = "Fairy Jewelry";
                inputResources = new string[] {"Fairy Crystal", "Gold Ingot"};
                baseCost = 50000;
                productionTime = 32;
                race = "Fairy";
                category = "Jeweler";
                break;

            
            //elf
            case "Anima Tree":
                outputResource = "Anima Fruit";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Gathering";
                break;
            case "Treant Grove":
                outputResource = "Living Wood";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Gathering";
                break;
            case "Chicken Coup":
                outputResource = "Egg";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Farm";
                break;
            case "Cotton Plantation":
                outputResource = "Cotton";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Farm";
                break;
            case "Unicorn Stable":
                outputResource = "Unicorn Hair";
                inputResources = new string[] {"Wheat"};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Farm";
                break;
            case "Cotton Weaver":
                outputResource = "Cotton Clothes";
                inputResources = new string[] {"Cotton"};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Crafting";
                break;
            case "Hatchery":
                outputResource = "Chicken";
                inputResources = new string[] {"Egg"};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Farm";
                break;
            case "Druid Circle":
                outputResource = "Enchantment";
                inputResources = new string[] {"Mana", "Living Wood"};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Magic";
                break;
            case "Sugarcane Plantation":
                outputResource = "Sugar";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Farm";
                break;
            case "Cake Bakery":
                outputResource = "Cake";
                inputResources = new string[] {"Flour", "Egg", "Milk"};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Crafting";
                break;
            case "Wand Maker":
                outputResource = "Wand";
                inputResources = new string[] {"Mana", "Living Wood", "Unicorn Hair"};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Crafting";
                break;
            case "Coffee Farm":
                outputResource = "Coffee";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Farm";
                break;
            case "Dark Wood Forest":
                outputResource = "Dark Wood";
                inputResources = new string[] {};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Gathering";
                break;
            case "Distillery":
                outputResource = "Liqour";
                inputResources = new string[] {"Sugar"};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Manufacturing";
                break;
            case "Life Condenser":
                outputResource = "Life Gem";
                inputResources = new string[] {"Mana", "Wheat"};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Processing";
                break;
            case "Elvish Jeweler":
                outputResource = "Elvish Jewelry";
                inputResources = new string[] {"Life Gem", "Gold Ingot"};
                baseCost = 50000;
                productionTime = 32;
                race = "Elf";
                category = "Jeweler";
                break;
        }

        if (inputResources.Length == 0)
        {
            inputResourceButton1.gameObject.SetActive(false);
            inputResourceButton2.gameObject.SetActive(false);
            inputResourceButton3.gameObject.SetActive(false);

            arrow1.SetActive(false);
            arrow2.SetActive(false);
            arrow3.SetActive(false);
        } 
        else if (inputResources.Length == 1)
        {
            inputResourceButton1.gameObject.SetActive(false);
            inputResourceButton2.InitValues(inputResources[0]);
            inputResourceButton3.gameObject.SetActive(false);

            arrow1.SetActive(false);
            arrow2.SetActive(true);
            arrow3.SetActive(false);
        }
        else if (inputResources.Length == 2)
        {
            inputResourceButton1.InitValues(inputResources[0]);
            inputResourceButton2.InitValues(inputResources[1]);
            inputResourceButton3.gameObject.SetActive(false);

            arrow1.SetActive(true);
            arrow2.SetActive(true);
            arrow3.SetActive(false);
        } 
        else
        {
            inputResourceButton1.InitValues(inputResources[0]);
            inputResourceButton2.InitValues(inputResources[1]);
            inputResourceButton3.InitValues(inputResources[2]);

            arrow1.SetActive(true);
            arrow2.SetActive(true);
            arrow3.SetActive(true);
        }

        outputResourceButton.InitValues(outputResource, productionTime);
        TextMeshProUGUI nameText = transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        nameText.text = buildingName;
        if (!GameManager.instance.productionTimers.ContainsKey(outputResource))
        {
            GameManager.instance.productionTimers[outputResource] = 0;
        }
    }

    public void OpenManagerPanel()
    {
        GameManager.instance.managersPanel.Open(this);
    }


    public void LevelUp()
    {
        BigInteger cost = CalculateCost();
        if (GameManager.instance.HasEnoughCoin(cost))
        {
            Level = Level + 1;
            
            GameManager.instance.SubtractCoins(cost);
        }
    }

    public BigInteger CalculateCost()
    {
        double multiplier = 1;
        if (category == "Farm")
        {
            multiplier -= ResearchManager.instance.prestigeResearchLevels.ContainsKey("Fertilizer") ? ResearchManager.instance.prestigeResearchLevels["Fertilizer"] * .1 : 0;
        }

        multiplier -= ResearchManager.instance.scienceResearchLevels.ContainsKey("Architecture") ? ResearchManager.instance.scienceResearchLevels["Architecture"] * .1 : 0;

        if (Manager != null)
        {
            multiplier -= Manager.effect1Type == "LessConsumption" || Manager.effect2Type == "LessConsumption" ? Manager.GetEffectMagnitude("LessConsumption") : 0;
        }
        return baseCost * GameManager.Pow(level + 1, 2) * (int)(multiplier * 100) / 100;
    }

    public void ProductionClick(string resource)
    {
        onProductionClick.Invoke(resource);
    }

    public ProductionOutput GetOutputResource()
    {
        return outputResourceButton;
    }

    public ProductionInput GetInputResource(int index)
    {
        if (index == 1)
        {
            return inputResourceButton1;
        } else if (index == 2) {
            return inputResourceButton2;
        } else {
            return inputResourceButton3;
        }
    }

    public void HandleProductionClick()
    {
        outputResourceButton.HandleProductionClick();
    }

    public int GetProductionQuantity()
    {
        double multiplier = 1;

        if (category == "Gathering")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Gathering") ? ResearchManager.instance.scienceResearchLevels["Gathering"] * .1f : 0;
        }
        if (category == "Mine")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Mining") ? ResearchManager.instance.scienceResearchLevels["Mining"] * .1f : 0;
        }
        if (category == "Farm")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Farming") ? ResearchManager.instance.scienceResearchLevels["Farming"] * .1f : 0;
        }
        if (category == "Crafting")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Crafting") ? ResearchManager.instance.scienceResearchLevels["Crafting"] * .1f : 0;
        }
        if (category == "Processing")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Processing") ? ResearchManager.instance.scienceResearchLevels["Processing"] * .1f : 0;
        }
        if (category == "Manufacturing")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Manufacturing") ? ResearchManager.instance.scienceResearchLevels["Manufacturing"] * .1f : 0;
        }
        if (category == "Jeweler")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Jeweler") ? ResearchManager.instance.scienceResearchLevels["Jeweler"] * .1f : 0;
        }
        if (category == "Magic")
        {
            multiplier += ResearchManager.instance.scienceResearchLevels.ContainsKey("Magic") ? ResearchManager.instance.scienceResearchLevels["Magic"] * .1f : 0;
        }

        if (ResearchManager.instance.buildingResearchLevels.ContainsKey(buildingName))
        {
            multiplier += (ResearchManager.instance.buildingResearchLevels[buildingName] - 1) * .1f; 
        }
        if (race == "Human")
        {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Human Tech") ? ResearchManager.instance.prestigeResearchLevels["Human Tech"] * .1f : 0;
        } else if (race == "Merfolk") {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Merfolk Tech") ? ResearchManager.instance.prestigeResearchLevels["Merfolk Tech"] * .1f : 0;
        } else if (race == "Dwarf") {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Dwarf Tech") ? ResearchManager.instance.prestigeResearchLevels["Dwarf Tech"] * .1f : 0;
        } else if (race == "Fairy") {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Fairy Tech") ? ResearchManager.instance.prestigeResearchLevels["Fairy Tech"] * .1f : 0;
        } else if (race == "Elf") {
            multiplier += ResearchManager.instance.prestigeResearchLevels.ContainsKey("Elf Tech") ? ResearchManager.instance.prestigeResearchLevels["Elf Tech"] * .1f : 0;
        }
        multiplier += Manager != null && (Manager.effect1Type == "ProductionQuantity" || Manager.effect2Type == "ProductionQuantity") ? Manager.GetEffectMagnitude("ProductionQuantity") : 0;
        return (int)(level * multiplier);
    }

    public void Unlock()
    {
        Level = 1;
        locked = false;
        transform.Find("LockedPanel").gameObject.SetActive(false);
        Transform display = transform.Find("ProductionDisplay");
        display.Find("OutputProductionButton").GetComponent<ProductionOutput>().Unlock();
        display.Find("InputProductionButton1").GetComponent<ProductionInput>().Unlock();
        display.Find("InputProductionButton2").GetComponent<ProductionInput>().Unlock();
        display.Find("InputProductionButton3").GetComponent<ProductionInput>().Unlock();
    }

    public BigInteger GetUnlockCost()
    {
        BigInteger baseUnlock = baseCost * 3;
        if (ResearchManager.instance.scienceResearchLevels.ContainsKey("Architecture"))
        {
            baseUnlock -= baseUnlock * ResearchManager.instance.scienceResearchLevels["Architecture"] / 10;
        }
        return baseUnlock;
    }

    public void Tick()
    {
        if (transform.Find("ProductionDisplay").Find("PauseToggle").GetComponent<Toggle>().isOn == false)
        {
            outputResourceButton.Tick(false);
        }
    }
}
