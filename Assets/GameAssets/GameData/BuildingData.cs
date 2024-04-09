using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;



public class BuildingData
{
    public string outputResource;
    public string[] inputResources;
    public BigInteger baseCost;
    public int productionTime;
    public string race = "Human";
    public BigInteger researchBaseCost = 10;
    public string category = "Farm";
    public BuildingData(string buildingName)
    {
        switch(buildingName)
        {
        case "Farm":
            outputResource = "Wheat";
            inputResources = new string[] {};
            baseCost = 2;
            productionTime = 10;
            researchBaseCost = 10;
            category = "Farm";
            break;
        case "Forester":
            outputResource = "Wood";
            inputResources = new string[] {};
            baseCost = 6;
            productionTime = 12;
            researchBaseCost = 20;
            category = "Gathering";
            break;
        case "Clay Pit":
            outputResource = "Clay";
            inputResources = new string[] {};
            baseCost = 10;
            productionTime = 14;
            researchBaseCost = 40;
            category = "Gathering";
            break;
        case "Lumber Mill":
            outputResource = "Lumber";
            inputResources = new string[] {"Wood"};
            baseCost = 200;
            productionTime = 16;
            researchBaseCost = 60;
            category = "Processing";
            break;
        case "Potter":
            outputResource = "Pottery";
            inputResources = new string[] {"Clay"};
            baseCost = 600;
            productionTime = 18;
            researchBaseCost = 80;
            category = "Crafting";
            break;
        case "Stone Quarry":
            outputResource = "Stone";
            inputResources = new string[] {};
            baseCost = 1800;
            productionTime = 22;
            researchBaseCost = 100;
            category = "Gathering";
            break;
        case "Vegetable Farm":
            outputResource = "Vegetables";
            inputResources = new string[] {};
            baseCost = 6000;
            productionTime = 24;
            researchBaseCost = 300;
            category = "Farm";
            break;
        case "Hemp Farm":
            outputResource = "Hemp";
            inputResources = new string[] {};
            baseCost = 20000;
            productionTime = 24;
            researchBaseCost = 1000;
            category = "Farm";
            break;
        case "Weaver":
            outputResource = "Clothes";
            inputResources = new string[] {"Hemp"};
            baseCost = 80000;
            productionTime = 24;
            researchBaseCost = 5000;
            category = "Crafting";
            break;
        case "Copper Mine":
            outputResource = "Copper Ore";
            inputResources = new string[] {};
            baseCost = 320000;
            productionTime = 28;
            researchBaseCost = 30000;
            category = "Mine";
            break;
        case "Orchard":
            outputResource = "Fruit";
            inputResources = new string[] {};
            baseCost = 1000000;
            productionTime = 30;
            researchBaseCost = new BigInteger(Math.Pow(10, 5));
            category = "Farm";
            break;
        case "Tin Mine":
            outputResource = "Tin Ore";
            inputResources = new string[] {};
            baseCost = 5000000;
            productionTime = 28;
            researchBaseCost = new BigInteger(Math.Pow(10, 6));
            category = "Mine";
            break;
        case "Smelter":
            outputResource = "Bronze Ingot";
            inputResources = new string[] {"Copper Ore", "Tin Ore", "Wood"};
            baseCost = 25000000;
            productionTime = 30;
            researchBaseCost = new BigInteger(Math.Pow(10, 7));
            category = "Processing";
            break;
        case "Wind Mill":
            outputResource = "Flour";
            inputResources = new string[] {"Wheat"};
            baseCost = new BigInteger(Math.Pow(10, 7));
            productionTime = 26;
            researchBaseCost = new BigInteger(Math.Pow(10, 8));
            category = "Processing";
            break;
        case "Bakery":
            outputResource = "Bread";
            inputResources = new string[] {"Flour"};
            baseCost = new BigInteger(Math.Pow(10, 8));;
            productionTime = 30;
            researchBaseCost = new BigInteger(Math.Pow(10, 9));
            category = "Crafting";
            break;
        case "Vineyard":
            outputResource = "Grapes";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 9));
            productionTime = 32;
            researchBaseCost = new BigInteger(Math.Pow(10, 10));
            category = "Farm";
            break;
        case "Furniture Factory":
            outputResource = "Furniture";
            inputResources = new string[] {"Lumber", "Bronze Ingot"};
            baseCost = new BigInteger(Math.Pow(10, 10));
            productionTime = 32;
            researchBaseCost = new BigInteger(Math.Pow(10, 12));
            category = "Manufacturing";
            break;
        case "Pasture":
            outputResource = "Cow";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 11));
            productionTime = 32;
            researchBaseCost = new BigInteger(Math.Pow(10, 13));
            category = "Farm";
            break;
        case "Dairy":
            outputResource = "Milk";
            inputResources = new string[] {"Cow"};
            baseCost = new BigInteger(Math.Pow(10, 12));
            productionTime = 32;
            researchBaseCost = new BigInteger(Math.Pow(10, 14));
            category = "Processing";
            break;
        case "Barrel Maker":
            outputResource = "Barrel";
            inputResources = new string[] {"Lumber", "Iron Ingot"};
            baseCost = new BigInteger(Math.Pow(10, 13));
            productionTime = 32;
            researchBaseCost = new BigInteger(Math.Pow(10, 15));
            category = "Manufacturing";
            break;
        case "Winery":
            outputResource = "Wine";
            inputResources = new string[] {"Grapes", "Barrel"};
            baseCost = new BigInteger(Math.Pow(10, 14));
            productionTime = 32;
            researchBaseCost = new BigInteger(Math.Pow(10, 16));
            category = "Crafting";
            break;
        case "Butcher":
            outputResource = "Beef";
            inputResources = new string[] {"Cow"};
            baseCost = new BigInteger(Math.Pow(10, 15));
            productionTime = 32;
            researchBaseCost = new BigInteger(Math.Pow(10, 17));
            category = "Processing";
            break;
        case "Tannery":
            outputResource = "Leather";
            inputResources = new string[] {"Cow"};
            baseCost = new BigInteger(Math.Pow(10, 16));
            productionTime = 32;
            researchBaseCost = new BigInteger(Math.Pow(10, 18));
            category = "Processing";
            break;
        case "Paper Mill":
            outputResource = "Paper";
            inputResources = new string[] {"Wood"};
            baseCost = new BigInteger(Math.Pow(10, 17));
            productionTime = 32;
            researchBaseCost = new BigInteger(Math.Pow(10, 19));
            category = "Processing";
            break;
        case "Leather Shop":
            outputResource = "Leather Clothes";
            inputResources = new string[] {"Leather"};
            baseCost = new BigInteger(Math.Pow(10, 18));
            productionTime = 32;
            researchBaseCost = new BigInteger(Math.Pow(10, 20));
            category = "Crafting";
            break;
        case "Human Jeweler":
            outputResource = "Human Jewelry";
            inputResources = new string[] {"Gold Ingot", "Jewel"};
            baseCost = new BigInteger(Math.Pow(10, 19));
            productionTime = 32;
            researchBaseCost = new BigInteger(Math.Pow(10, 21));
            category = "Jeweler";
            break;
        case "Wizard University":
            outputResource = "High Arcana";
            inputResources = new string[] {"Mana", "Fairy Dust", "Life Gem"};
            baseCost = new BigInteger(Math.Pow(10, 20));
            productionTime = 32;
            researchBaseCost = new BigInteger(Math.Pow(10, 22));
            category = "Magic";
            break;

            //merfolk
        case "Kelpery":
            outputResource = "Kelp";
            inputResources = new string[] {};
            baseCost = 300;
            productionTime = 10;
            race = "Merfolk";
            researchBaseCost = 100;
            category = "Farm";
            break;
        case "Reef":
            outputResource = "Coral";
            inputResources = new string[] {};
            baseCost = 500;
            productionTime = 12;
            race = "Merfolk";
            researchBaseCost = 300;
            category = "Gathering";
            break;
        case "Fishery":
            outputResource = "Fish";
            inputResources = new string[] {};
            baseCost = 800;
            productionTime = 14;
            race = "Merfolk";
            researchBaseCost = 600;
            category = "Gathering";
            break;
        case "Reeds":
            outputResource = "Reed";
            inputResources = new string[] {};
            baseCost = 2400;
            productionTime = 16;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 3));
            category = "Gathering";
            break;
        case "Oystery":
            outputResource = "Pearl";
            inputResources = new string[] {};
            baseCost = 10000;
            productionTime = 18;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 4));
            category = "Farm";
            break;
        case "Basket Weaver":
            outputResource = "Basket";
            inputResources = new string[] {"Reed"};
            baseCost = 200000;
            productionTime = 18;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 5));
            category = "Crafting";
            break;
        case "Sand Banks":
            outputResource = "Sand";
            inputResources = new string[] {};
            baseCost = 9 * new BigInteger(Math.Pow(10, 4));
            productionTime = 20;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 6));
            category = "Gathering";
            break;
        case "Manatee Pasture":
            outputResource = "Manatee";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 5));
            productionTime = 20;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 7));
            category = "Farm";
            break;
        case "Merite Cave":
            outputResource = "Merite Ore";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 6));
            productionTime = 22;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 8));
            category = "Mine";
            break;
        case "Manatee Dairy":
            outputResource = "Milk";
            inputResources = new string[] {"Manatee"};
            baseCost = 6 * new BigInteger(Math.Pow(10, 7));
            productionTime = 20;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 9));
            category = "Processing";
            break;
        case "Newt Swamp":
            outputResource = "Eye Of Newt";
            inputResources = new string[] {};
            baseCost = 6 * new BigInteger(Math.Pow(10, 8));
            productionTime = 20;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 10));
            category = "Gathering";
            break;
        case "Squid Traps":
            outputResource = "Ink";
            inputResources = new string[] {};
            baseCost = 6 * new BigInteger(Math.Pow(10, 9));
            productionTime = 20;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 11));
            category = "Gathering";
            break;
        case "Witch Hut":
            outputResource = "Curse";
            inputResources = new string[] {"Eye Of Newt"};
            baseCost = 6 * new BigInteger(Math.Pow(10, 10));
            productionTime = 20;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 12));
            category = "Magic";
            break;
        case "Crab Pots":
            outputResource = "Crab";
            inputResources = new string[] {"Reed"};
            baseCost = new BigInteger(Math.Pow(10, 11));
            productionTime = 26;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 13));
            category = "Gathering";
            break;
        case "Thermal Vents":
            outputResource = "Magma Slug";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 12));;
            productionTime = 32;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 14));
            category = "Gathering";
            break;
        case "Rice Patties":
            outputResource = "Rice";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 13));
            productionTime = 26;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 15));
            category = "Farm";
            break;
        case "Slime Milker":
            outputResource = "Fire Slime";
            inputResources = new string[] {"Magma Slug"};
            baseCost = new BigInteger(Math.Pow(10, 14));;
            productionTime = 36;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 16));
            category = "Processing";
            break;
        case "Aqua Forge":
            outputResource = "Merite Ingot";
            inputResources = new string[] {"Fire Slime", "Merite Ore"};
            baseCost = new BigInteger(Math.Pow(10, 15));;
            productionTime = 40;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 17));
            category = "Processing";
            break;
        case "Aqua Jeweler":
            outputResource = "Mer Jewelry";
            inputResources = new string[] {"Fire Slime", "Merite Ore", "Pearl"};
            baseCost = new BigInteger(Math.Pow(10, 16));;
            productionTime = 40;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 18));
            category = "Jeweler";
            break;
        case "Aqua Armorer":
            outputResource = "Mermail";
            inputResources = new string[] {"Fire Slime", "Merite Ingot"};
            baseCost = new BigInteger(Math.Pow(10, 17));;
            productionTime = 40;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 19));
            category = "Crafting";
            break;
        case "Aqua Smith":
            outputResource = "Trident";
            inputResources = new string[] {"Fire Slime", "Merite Ingot"};
            baseCost = new BigInteger(Math.Pow(10, 18));;
            productionTime = 40;
            race = "Merfolk";
            researchBaseCost = new BigInteger(Math.Pow(10, 20));
            category = "Crafting";
            break;

        //Dwarves
        case "Mushroom Cave":
            outputResource = "Mushroom";
            inputResources = new string[] {};
            baseCost = 200;
            productionTime = 10;
            race = "Dwarf";
            researchBaseCost = 100;
            category = "Gathering";
            break;  
        case "Mana Well":
            outputResource = "Mana";
            inputResources = new string[] {};
            baseCost = 600;
            productionTime = 12;
            race = "Dwarf";
            researchBaseCost = 400;
            category = "Magic";
            break;
        case "Coal Mine":
            outputResource = "Coal";
            inputResources = new string[] {};
            baseCost = 800;
            productionTime = 14;
            race = "Dwarf";
            researchBaseCost = 600;
            category = "Mine";
            break;
        case "Iron Mine":
            outputResource = "Iron Ore";
            inputResources = new string[] {};
            baseCost = 2600;
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 3));
            category = "Mine";
            break;
        case "Apiary":
            outputResource = "Honey";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 5));
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 6));
            category = "Farm";
            break;
        case "Iron Smelter":
            outputResource = "Iron Ingot";
            inputResources = new string[] {"Coal", "Iron Ore"};
            baseCost = new BigInteger(Math.Pow(10, 6));
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 7));
            category = "Processing"; 
            break;
        case "Meadery":
            outputResource = "Mead";
            inputResources = new string[] {"Honey"};
            baseCost = new BigInteger(Math.Pow(10, 9));
            productionTime = 20;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 10));
            category = "Crafting";
            break;
        case "Gear Works":
            outputResource = "Mechanical Parts";
            inputResources = new string[] {"Wood", "Iron Ingot"};
            baseCost = new BigInteger(Math.Pow(10, 10));
            productionTime = 24;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 11));
            category = "Manufacturing";
            break;
        case "Blacksmith":
            outputResource = "Tools";
            inputResources = new string[] {"Coal", "Iron Ore", "Wood"};
            baseCost = new BigInteger(Math.Pow(10, 11));
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 12));
            category = "Crafting";
            break;
        case "Hops Farm":
            outputResource = "Hops";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 12));
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 13));
            category = "Farm";
            break;
        case "Gold Mine":
            outputResource = "Gold Ore";
            inputResources = new string[] {"Tools"};
            baseCost = new BigInteger(Math.Pow(10, 13));
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 14));
            category = "Mine";
            break;
        case "Glass Blower":
            outputResource = "Glass";
            inputResources = new string[] {"Sand"};
            baseCost = new BigInteger(Math.Pow(10, 14));
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 15));
            category = "Crafting";
            break;                
        case "Brewery":
            outputResource = "Beer";
            inputResources = new string[] {"Wheat", "Hops", "Bottles"};
            baseCost = new BigInteger(Math.Pow(10, 15));
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 16));
            category = "Crafting";
            break;
        case "Gold Smelter":
            outputResource = "Gold Ingot";
            inputResources = new string[] {"Coal", "Iron Ore"};
            baseCost = new BigInteger(Math.Pow(10, 16));
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 17));
            category = "Processing";
            break;
        case "Artificer":
            outputResource = "Artifact";
            inputResources = new string[] {"Mechanical Parts", "Mana", "Gold"};
            baseCost = new BigInteger(Math.Pow(10, 17));
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 18));
            category = "Crafting";
            break;
        case "Jewel Mine":
            outputResource = "Jewel";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 18));
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 19));
            category = "Mine";
            break;
        case "Printing Press":
            outputResource = "Book";
            inputResources = new string[] {"Leather", "Paper", "Ink"};
            baseCost = new BigInteger(Math.Pow(10, 19));
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 20));
            category = "Manufacturing";
            break;
        case "Manufactory":
            outputResource = "Golem";
            inputResources = new string[] {"Mechanical Parts", "Mana", "Clay"};
            baseCost = new BigInteger(Math.Pow(10, 20));
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 21));
            category = "Manufacturing";
            break;  
        case "Rune Workshop":
            outputResource = "Rune";
            inputResources = new string[] {"Mana", "Stone"};
            baseCost = new BigInteger(Math.Pow(10, 21));
            productionTime = 32;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 22));
            category = "Magic";
            break;  
        case "Deep Mine":
            outputResource = "Mithril Ore";
            inputResources = new string[] {"Tools", "Golem"};
            baseCost = new BigInteger(Math.Pow(10, 22));
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 23));
            category = "Mine";
            break;
        case "Volcanic Forge":
            outputResource = "Mithril Ingot";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 23));
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 24));
            category = "Processing";
            break;
        case "Mithril Smith":
            outputResource = "Mithril Armor";
            inputResources = new string[] {"Mithril Ingot"};
            baseCost = new BigInteger(Math.Pow(10, 24));
            productionTime = 18;
            race = "Dwarf";
            researchBaseCost = new BigInteger(Math.Pow(10, 25));
            category = "Crafting";
            break;  

        //Fairies
        case "Garden":
            outputResource = "Flowers";
            inputResources = new string[] {};
            baseCost = 50000;
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 6));
            category = "Farm";
            break;
        case "Fairy Circle":
            outputResource = "Magic Mushroom";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 6));
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 7));
            category = "Magic";
            break;
        case "Berry Field":
            outputResource = "Berry";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 7));
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 8));
            category = "Gathering";
            break;
        case "Dustery":
            outputResource = "Fairy Dust";
            inputResources = new string[] {"Magic Mushroom"};
            baseCost = new BigInteger(Math.Pow(10, 8));
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 9));
            category = "Processing";
            break;       
        case "Mana Siphon":
            outputResource = "Mana";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 9));
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 10));
            category = "Magic";
            break;
        case "Cobbler":
            outputResource = "Shoes";
            inputResources = new string[] {"Leather"};
            baseCost = new BigInteger(Math.Pow(10, 10));
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 11));
            category = "Crafting";
            break;
        case "Rainbow":
            outputResource = "Luck Charm";
            inputResources = new string[] {"Mana", "Iron Ingot"};
            baseCost = new BigInteger(Math.Pow(10, 11));
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 12));
            category = "Magic";
            break;
        case "Illusionist":
            outputResource = "Beauty Charm";
            inputResources = new string[] {"Mana", "Living Wood", "Fairy Dust"};
            baseCost = new BigInteger(Math.Pow(10, 12));
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 13));
            category = "Magic";
            break;
        case "Luminous Garden":
            outputResource = "Light Bulb";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 13));
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 14));
            category = "Farm";
            break;
        case "Tea Farm":
            outputResource = "Tea";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 14));
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 15));
            category = "Farm";
            break;
        case "Worm Forest":
            outputResource = "Silk";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 15));
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 16));
            category = "Gathering";
            break;
        case "Fairy Alchemist":
            outputResource = "Fairyelium";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 16));
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 17));
            category = "Manufactury";
            break;
        case "Spell Weaver":
            outputResource = "Whisper Silk";
            inputResources = new string[] {"Silk", "Mana", "Fairy Dust"};
            baseCost = new BigInteger(Math.Pow(10, 17));
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 18));
            category = "Processing";
            break;
        case "Crystalizer":
            outputResource = "Fairy Crystal";
            inputResources = new string[] {"Fairyelium", "Mana"};
            baseCost = new BigInteger(Math.Pow(10, 18));
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 19));
            category = "Processing";
            break;
        case "Fairy Seamstress":
            outputResource = "Invisibility Cloak";
            inputResources = new string[] {"Whisper Silk", "Mana"};
            baseCost = new BigInteger(Math.Pow(10, 19));
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 20));
            category = "Crafting";
            break;
        case "Fairy Jeweler":
            outputResource = "Fairy Jewelry";
            inputResources = new string[] {"Fairy Crystal", "Gold Ingot"};
            baseCost = new BigInteger(Math.Pow(10, 20));
            productionTime = 32;
            race = "Fairy";
            researchBaseCost = new BigInteger(Math.Pow(10, 21));
            category = "Jeweler";
            break;


        //elf
        case "Anima Tree":
            outputResource = "Anima Fruit";
            inputResources = new string[] {};
            baseCost = 50000;
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 6));
            category = "Gathering";
            break;
        case "Treant Grove":
            outputResource = "Living Wood";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 6));
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 7));
            category = "Gathering";
            break;
        case "Chicken Coup":
            outputResource = "Egg";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 7));
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 8));
            category = "Farm";
            break;
        case "Cotton Plantation":
            outputResource = "Cotton";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 8));
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 9));
            category = "Farm";
            break;
        case "Unicorn Stable":
            outputResource = "Unicorn Hair";
            inputResources = new string[] {"Wheat"};
            baseCost = new BigInteger(Math.Pow(10, 9));
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 10));
            category = "Farm";
            break;
        case "Cotton Weaver":
            outputResource = "Cotton Clothes";
            inputResources = new string[] {"Cotton"};
            baseCost = new BigInteger(Math.Pow(10, 10));
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 11));
            category = "Crafting";
            break;
        case "Hatchery":
            outputResource = "Chicken";
            inputResources = new string[] {"Egg"};
            baseCost = new BigInteger(Math.Pow(10, 11));
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 12));
            category = "Farm";
            break;
        case "Druid Circle":
            outputResource = "Enchantment";
            inputResources = new string[] {"Mana", "Living Wood"};
            baseCost = new BigInteger(Math.Pow(10, 12));
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 13));
            category = "Magic";
            break;
        case "Sugarcane Plantation":
            outputResource = "Sugar";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 13));
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 14));
            category = "Farm";
            break;
        case "Cake Bakery":
            outputResource = "Cake";
            inputResources = new string[] {"Flour", "Egg", "Milk"};
            baseCost = new BigInteger(Math.Pow(10, 14));
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 15));
            category = "Crafting";
            break;
        case "Wand Maker":
            outputResource = "Wand";
            inputResources = new string[] {"Mana", "Living Wood", "Unicorn Hair"};
            baseCost = new BigInteger(Math.Pow(10, 15));
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 16));
            category = "Crafting";
            break;
        case "Coffee Farm":
            outputResource = "Coffee";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 16));
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 17));
            category = "Farm";
            break;
        case "Dark Wood Forest":
            outputResource = "Dark Wood";
            inputResources = new string[] {};
            baseCost = new BigInteger(Math.Pow(10, 17));
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 18));
            category = "Gathering";
            break;
        case "Distillery":
            outputResource = "Liqour";
            inputResources = new string[] {"Sugar"};
            baseCost = new BigInteger(Math.Pow(10, 18));
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 19));
            category = "Manufacturing";
            break;
        case "Life Condenser":
            outputResource = "Life Gem";
            inputResources = new string[] {"Mana", "Wheat"};
            baseCost = new BigInteger(Math.Pow(10, 19));
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 20));
            category = "Processing";
            break;
        case "Elvish Jeweler":
            outputResource = "Elvish Jewelry";
            inputResources = new string[] {"Life Gem", "Gold Ingot"};
            baseCost = new BigInteger(Math.Pow(10, 20));
            productionTime = 32;
            race = "Elf";
            researchBaseCost = new BigInteger(Math.Pow(10, 21));
            category = "Jeweler";
            break;
        }
    }
}