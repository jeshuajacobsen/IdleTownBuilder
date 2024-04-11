using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;

public class DemographicData
{
    public BigInteger baseCost;
    public BigInteger basePrestigeGenerated;
    public int tier;
    public string race;

    public Dictionary<string, int> requirements = new Dictionary<string, int>();


    public DemographicData(string name)
    {

        switch(name)
        {
            case "Peasants":
                race = "Human";
                //baseCost = 20;
                //basePrestigeGenerated = 4;
                requirements.Add("Wheat", 5);
                requirements.Add("Pottery", 1);
                tier = 1;
                break;
            case "Commoners":
                race = "Human";
                //baseCost = 2000;
                //basePrestigeGenerated = 180;
                requirements.Add("Vegetables", 4);
                requirements.Add("Flour", 2);
                requirements.Add("Fruit", 1);
                tier = 2;
                break;
            case "Tradesmen":
                race = "Human";
                //baseCost = 80000;
                //basePrestigeGenerated = 10000;
                requirements.Add("Clothes", 2);
                requirements.Add("Fruit", 4);
                requirements.Add("Fish", 5);
                requirements.Add("Honey", 2);
                tier = 3;
                break;
            case "Patricians":
                race = "Human";
                //baseCost = 4000000;
                //basePrestigeGenerated = 2000000;
                requirements.Add("Bread", 2);
                requirements.Add("Furniture", 4);
                requirements.Add("Basket", 5);
                requirements.Add("Mead", 5);
                tier = 4;
                break;
            case "Wizards":
                race = "Human";
                //baseCost = 8000000;
                //basePrestigeGenerated = 20;
                requirements.Add("Milk", 5);
                requirements.Add("Artifact", 1);
                requirements.Add("Fairy Dust", 5);
                requirements.Add("Wand", 2);
                tier = 5;
                break;
            case "Nobles":
                race = "Human";
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                requirements.Add("Beef", 3);
                requirements.Add("Light Bulb", 2);
                requirements.Add("Cake", 2);
                requirements.Add("Tea", 2);
                tier = 6;
                break;
            case "Royalty":
                race = "Human";
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                requirements.Add("Human Jewelry", 1);
                requirements.Add("Elvish Jewelry", 1);
                requirements.Add("Fairy Jewelry", 1);
                requirements.Add("Mer Jewelry", 1);
                tier = 7;
                break;

            //merfolk
            case "Surfs":
                //baseCost = 200;
                //basePrestigeGenerated = 200;
                race = "Merfolk";
                requirements.Add("Kelp", 5);
                requirements.Add("Fish", 3);
                tier = 1;
                break;
            case "Middle Mer":
                //baseCost = 400;
                //basePrestigeGenerated = 20;
                race = "Merfolk";
                requirements.Add("Pottery", 5);
                requirements.Add("Pearl", 2);
                requirements.Add("Crab", 3);
                tier = 2;
                break;
            case "Sea Witches":
                //baseCost = 1000;
                //basePrestigeGenerated = 20;
                race = "Merfolk";
                requirements.Add("Beauty Charm", 2);
                requirements.Add("Rune", 3);
                requirements.Add("Eye Of Newt", 3);
                tier = 3;
                break;
            case "Mer-chants":
                //baseCost = 4000;
                //basePrestigeGenerated = 20;
                race = "Merfolk";
                requirements.Add("Mer Jewelry", 1);
                requirements.Add("Luck Charm", 2);
                requirements.Add("Beer", 2);
                requirements.Add("Rice", 3);
                tier = 4;
                break;
            case "High Mer":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Merfolk";
                requirements.Add("Mer Jewelry", 3);
                requirements.Add("Artifact", 2);
                requirements.Add("Light Bulb", 2);
                requirements.Add("Liqour", 1);
                tier = 5;
                break;
            case "Tritons":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Merfolk";
                requirements.Add("Mermail", 2);
                requirements.Add("Trident", 1);
                requirements.Add("Wine", 5);
                requirements.Add("Liqour", 2);
                tier = 6;
                break;


            //Dwarves
            case "Miners":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Dwarf";
                requirements.Add("Mushroom", 5);
                requirements.Add("Flour", 2);
                tier = 1;
                break;
            case "Workers":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Dwarf";
                requirements.Add("Tools", 2);
                requirements.Add("Milk", 2);
                requirements.Add("Beer", 1);
                tier = 2;
                break;
            case "Mages":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Dwarf";
                requirements.Add("Rune", 2);
                requirements.Add("Magic Mushroom", 2);
                requirements.Add("Wine", 1);
                requirements.Add("Enchantment", 2);
                tier = 3;
                break;
            case "Artificers":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Dwarf";
                requirements.Add("Book", 3);
                requirements.Add("Golem", 2);
                requirements.Add("Cake", 1);
                requirements.Add("Leather Clothes", 1);
                tier = 4;
                break;
            case "Dwarf Lords":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Dwarf";
                requirements.Add("Mithril Armor", 1);
                requirements.Add("Invisibility Cloak", 2);
                requirements.Add("Liqour", 3);
                tier = 5;
                break;

            //Fairies
            case "Changelings":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Fairy";
                requirements.Add("Flowers", 5);
                requirements.Add("Honey", 2);
                requirements.Add("Bread", 1);
                tier = 1;
                break;
            case "Brownies":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Fairy";
                requirements.Add("Flowers", 10);
                requirements.Add("Shoes", 2);
                requirements.Add("Berry", 3);
                tier = 2;
                break;
            case "Leprechauns":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Fairy";
                requirements.Add("Luck Charm", 2);
                requirements.Add("Sugar", 3);
                requirements.Add("Milk", 2);
                tier = 3;
                break;
            case "Selkies":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Fairy";
                requirements.Add("Fish", 2);
                requirements.Add("Rice", 2);
                requirements.Add("Mana", 5);
                requirements.Add("Egg", 3);
                tier = 4;
                break;
            case "Clurichaun":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Fairy";
                requirements.Add("Light Bulb", 2);
                requirements.Add("Beer", 2);
                requirements.Add("Wine", 2);
                requirements.Add("Liqour", 2);
                tier = 5;
                break;
            case "Aos Si":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Fairy";
                requirements.Add("Fairy Jewelry", 2);
                requirements.Add("Coffee", 3);
                requirements.Add("Wine", 3);
                requirements.Add("Liqour", 1);
                tier = 6;
                break;


            //Elf
            case "Worker Elves":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Elf";
                requirements.Add("Anima Fruit", 5);
                requirements.Add("Egg", 2);
                tier = 1;
                break;
            case "House Elves":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Elf";
                requirements.Add("Cotton Clothes", 3);
                requirements.Add("Shoes", 2);
                requirements.Add("Fish", 3);
                tier = 2;
                break;
            case "Druids":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Elf";
                requirements.Add("Chicken", 3);
                requirements.Add("Enchantment", 2);
                requirements.Add("Rune", 2);
                requirements.Add("Curse", 2);
                tier = 3;
                break;
            case "High Elves":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Elf";
                requirements.Add("Beauty Charm", 2);
                requirements.Add("Coffee", 3);
                requirements.Add("Rice", 3);
                requirements.Add("Liqour", 2);
                tier = 4;
                break;
            case "Perfects":
                //baseCost = 200;
                //basePrestigeGenerated = 20;
                race = "Elf";
                requirements.Add("Elvish Jewelry", 2);
                requirements.Add("Fairy Jewelry", 2);
                requirements.Add("Book", 3);
                requirements.Add("High Arcana", 1);
                tier = 5;
                break;
        }
    }
}