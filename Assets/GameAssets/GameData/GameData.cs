using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class GameData
{
    private Dictionary<string, BuildingData> buildingsData;
    private Dictionary<string, CityData> cityData;

    public Dictionary<string, BigInteger> resourcePrices;
    public GameData()
    {

        resourcePrices = new Dictionary<string, BigInteger>();
        resourcePrices["Wheat"] = 1;
        resourcePrices["Wood"] = 4;
        resourcePrices["Clay"] = 10;
        resourcePrices["Lumber"] = 40;
        resourcePrices["Pottery"] = 100;
        resourcePrices["Stone"] = 300;
        resourcePrices["Vegetables"] = 500;
        resourcePrices["Hemp"] = 700;
        resourcePrices["Clothes"] = 1000;
        resourcePrices["Copper Ore"] = 7000;
        resourcePrices["Fruit"] = 15000;
        resourcePrices["Tin Ore"] = 90000;
        resourcePrices["Bronze Ingot"] = 100000;
        resourcePrices["Flour"] = 3 * new BigInteger(Math.Pow(10, 5));
        resourcePrices["Bread"] = new BigInteger(Math.Pow(10, 6));
        resourcePrices["Grapes"] = 2 * new BigInteger(Math.Pow(10, 7));
        resourcePrices["Furniture"] = 3 * new BigInteger(Math.Pow(10, 8));;
        resourcePrices["Cow"] = new BigInteger(Math.Pow(10, 9));;
        resourcePrices["Milk"] = new BigInteger(Math.Pow(10, 10));
        resourcePrices["Barrel"] = new BigInteger(Math.Pow(10, 11));
        resourcePrices["Wine"] = 9 * new BigInteger(Math.Pow(10, 11));
        resourcePrices["Beef"] = 7 * new BigInteger(Math.Pow(10, 12));
        resourcePrices["Leather"] = 6 * new BigInteger(Math.Pow(10, 13));
        resourcePrices["Paper"] = 7 * new BigInteger(Math.Pow(10, 14));
        resourcePrices["Leather Clothes"] = 2 * new BigInteger(Math.Pow(10, 15));
        resourcePrices["Human Jewelry"] = new BigInteger(Math.Pow(10, 16));
        resourcePrices["High Arcana"] = new BigInteger(Math.Pow(10, 19));

        //merfolk
        resourcePrices["Kelp"] = 100;
        resourcePrices["Coral"] = 200;
        resourcePrices["Fish"] = 400;
        resourcePrices["Reed"] = 800;
        resourcePrices["Pearl"] = 2000;
        resourcePrices["Basket"] = 9000;
        resourcePrices["Sand"] = 30000;
        resourcePrices["Manatee"] = 10000;
        resourcePrices["Merite Ore"] = 30000;
        //resourcePrices["Milk"] = 10000;
        resourcePrices["Eye Of Newt"] = 100000;
        resourcePrices["Ink"] = 4 * new BigInteger(Math.Pow(10, 5));
        resourcePrices["Curse"] = 3 * new BigInteger(Math.Pow(10, 6));;
        resourcePrices["Crab"] = 4 * new BigInteger(Math.Pow(10, 7));
        resourcePrices["Magma Slug"] = 6 * new BigInteger(Math.Pow(10, 8));
        resourcePrices["Rice"] = 2 * new BigInteger(Math.Pow(10, 9));
        resourcePrices["Fire Slime"] = 2 * new BigInteger(Math.Pow(10, 10));
        resourcePrices["Merite Ingot"] = 2 * new BigInteger(Math.Pow(10, 11));
        resourcePrices["Mer Jewelry"] = new BigInteger(Math.Pow(10, 12));
        resourcePrices["Mermail"] = 9 * new BigInteger(Math.Pow(10, 13));
        resourcePrices["Trident"] = 4 * new BigInteger(Math.Pow(10, 15));

        //Dwarf
        resourcePrices["Mushroom"] = 80;
        resourcePrices["Mana"] = 300;
        resourcePrices["Coal"] = 400;
        resourcePrices["Iron Ore"] = 1000;
        resourcePrices["Honey"] = 4000;
        resourcePrices["Iron Ingot"] = 11000;
        resourcePrices["Mead"] = 50000;
        resourcePrices["Mechanical Parts"] = 150000;
        resourcePrices["Tools"] = 5 * new BigInteger(Math.Pow(10, 5));
        resourcePrices["Hops"] = 6 * new BigInteger(Math.Pow(10, 6));;
        resourcePrices["Gold Ore"] = 7 * new BigInteger(Math.Pow(10, 7));
        resourcePrices["Glass"] = 9 * new BigInteger(Math.Pow(10, 8));
        resourcePrices["Beer"] = 4 * new BigInteger(Math.Pow(10, 9));
        resourcePrices["Gold Ingot"] = 4 * new BigInteger(Math.Pow(10, 10));
        resourcePrices["Artifact"] = 3 * new BigInteger(Math.Pow(10, 11));
        resourcePrices["Jewel"] = 2 * new BigInteger(Math.Pow(10, 12));
        resourcePrices["Book"] = 9 * new BigInteger(Math.Pow(10, 12));
        resourcePrices["Golem"] = new BigInteger(Math.Pow(10, 14));
        resourcePrices["Rune"] = 9 * new BigInteger(Math.Pow(10, 14));
        resourcePrices["Mithril Ore"] = 6 * new BigInteger(Math.Pow(10, 15));
        resourcePrices["Mithril Ingot"] = 5 * new BigInteger(Math.Pow(10, 16));
        resourcePrices["Mithril Armor"] = new BigInteger(Math.Pow(10, 20));

        //Fairy
        resourcePrices["Flowers"] = 12000;
        resourcePrices["Magic Mushroom"] = 60000;
        resourcePrices["Berry"] = 200000;
        resourcePrices["Fairy Dust"] = 7 * new BigInteger(Math.Pow(10, 5));
        //resourcePrices["Mana"] = 10000;
        resourcePrices["Shoes"] = 9 * new BigInteger(Math.Pow(10, 6));;
        resourcePrices["Luck Charm"] = 9 * new BigInteger(Math.Pow(10, 7));
        resourcePrices["Beauty Charm"] = 2 * new BigInteger(Math.Pow(10, 8));;
        resourcePrices["Light Bulb"] = 6 * new BigInteger(Math.Pow(10, 9));
        resourcePrices["Tea"] = 6 * new BigInteger(Math.Pow(10, 10));
        resourcePrices["Silk"] = 5 * new BigInteger(Math.Pow(10, 11));
        resourcePrices["Fairyelium"] = 4 * new BigInteger(Math.Pow(10, 12));
        resourcePrices["Whisper Silk"] = new BigInteger(Math.Pow(10, 13));
        resourcePrices["Fairy Crystal"] = 2 * new BigInteger(Math.Pow(10, 14));
        resourcePrices["Invisibility Cloak"] = new BigInteger(Math.Pow(10, 17));
        resourcePrices["Fairy Jewelry"] = new BigInteger(Math.Pow(10, 21));

        //elf
        resourcePrices["Anima Fruit"] = 10000;
        resourcePrices["Living Wood"] = 70000;
        resourcePrices["Egg"] = 200000;
        resourcePrices["Cotton"] = 9 * new BigInteger(Math.Pow(10, 5));
        resourcePrices["Unicorn Hair"] = new BigInteger(Math.Pow(10, 7));
        resourcePrices["Cotton Clothes"] = 2 * new BigInteger(Math.Pow(10, 8));
        resourcePrices["Chicken"] = 9 * new BigInteger(Math.Pow(10, 9));
        resourcePrices["Enchantment"] = 8 * new BigInteger(Math.Pow(10, 10));
        resourcePrices["Sugar"] = 8 * new BigInteger(Math.Pow(10, 11));
        resourcePrices["Cake"] = 6 * new BigInteger(Math.Pow(10, 12));
        resourcePrices["Wand"] = 3 * new BigInteger(Math.Pow(10, 13));
        resourcePrices["Coffee"] = 4 * new BigInteger(Math.Pow(10, 14));
        resourcePrices["Dark Wood"] = new BigInteger(Math.Pow(10, 15));;
        resourcePrices["Liqour"] = 8 * new BigInteger(Math.Pow(10, 15));
        resourcePrices["Life Gem"] = new BigInteger(Math.Pow(10, 18));
        resourcePrices["Elvish Jewelry"] = new BigInteger(Math.Pow(10, 22));


        buildingsData = new Dictionary<string, BuildingData>
        {
            { "Farm", new BuildingData("Farm") },
            { "Forester", new BuildingData("Forester") },
            { "Clay Pit", new BuildingData("Clay Pit") },
            { "Lumber Mill", new BuildingData("Lumber Mill") },
            { "Potter", new BuildingData("Potter") },
            { "Stone Quarry", new BuildingData("Stone Quarry") },
            { "Vegetable Farm", new BuildingData("Vegetable Farm") },
            { "Hemp Farm", new BuildingData("Hemp Farm") },
            { "Weaver", new BuildingData("Weaver") },
            { "Copper Mine", new BuildingData("Copper Mine") },
            { "Orchard", new BuildingData("Orchard") },
            { "Tin Mine", new BuildingData("Tin Mine") },
            { "Smelter", new BuildingData("Smelter") },
            { "Wind Mill", new BuildingData("Wind Mill") },
            { "Bakery", new BuildingData("Bakery") },
            { "Vineyard", new BuildingData("Vineyard") },
            { "Furniture Factory", new BuildingData("Furniture Factory") },
            { "Pasture", new BuildingData("Pasture") },
            { "Dairy", new BuildingData("Dairy") },
            { "Barrel Maker", new BuildingData("Barrel Maker") },
            { "Winery", new BuildingData("Winery") },
            { "Butcher", new BuildingData("Butcher") },
            { "Tannery", new BuildingData("Tannery") },
            { "Paper Mill", new BuildingData("Paper Mill") },
            { "Leather Shop", new BuildingData("Leather Shop") },
            { "Human Jeweler", new BuildingData("Human Jeweler") },
            { "Wizard University", new BuildingData("Wizard University") },

            //merfolk
            { "Kelpery", new BuildingData("Kelpery") },
            { "Reef", new BuildingData("Reef") },
            { "Fishery", new BuildingData("Fishery") },
            { "Reeds", new BuildingData("Reeds") },
            { "Oystery", new BuildingData("Oystery") },
            { "Basket Weaver", new BuildingData("Basket Weaver") },
            { "Sand Banks", new BuildingData("Sand Banks") },
            { "Manatee Pasture", new BuildingData("Manatee Pasture") },
            { "Merite Cave", new BuildingData("Merite Cave") },
            { "Manatee Dairy", new BuildingData("Manatee Dairy") },
            { "Newt Swamp", new BuildingData("Newt Swamp") },
            { "Squid Traps", new BuildingData("Squid Traps") },
            { "Witch Hut", new BuildingData("Witch Hut") },
            { "Crab Pots", new BuildingData("Crab Pots") },
            { "Thermal Vents", new BuildingData("Thermal Vents") },
            { "Rice Patties", new BuildingData("Rice Patties") },
            { "Slime Milker", new BuildingData("Slime Milker") },
            { "Aqua Forge", new BuildingData("Aqua Forge") },
            { "Aqua Jeweler", new BuildingData("Aqua Jeweler") },
            { "Aqua Armorer", new BuildingData("Aqua Armorer") },
            { "Aqua Smith", new BuildingData("Aqua Smith") },

            //dwarf
            { "Mushroom Cave", new BuildingData("Mushroom Cave") },
            { "Mana Well", new BuildingData("Mana Well") },
            { "Coal Mine", new BuildingData("Coal Mine") },
            { "Iron Mine", new BuildingData("Iron Mine") },
            { "Apiary", new BuildingData("Apiary") },
            { "Iron Smelter", new BuildingData("Iron Smelter") },
            { "Meadery", new BuildingData("Meadery") },
            { "Gear Works", new BuildingData("Gear Works") },
            { "Blacksmith", new BuildingData("Blacksmith") },
            { "Hops Farm", new BuildingData("Hops Farm") },
            { "Gold Mine", new BuildingData("Gold Mine") },
            { "Glass Blower", new BuildingData("Glass Blower") },
            { "Brewery", new BuildingData("Brewery") },
            { "Gold Smelter", new BuildingData("Gold Smelter") },
            { "Artificer", new BuildingData("Artificer") },
            { "Jewel Mine", new BuildingData("Jewel Mine") },
            { "Printing Press", new BuildingData("Printing Press") },
            { "Manufactory", new BuildingData("Manufactory") },
            { "Rune Workshop", new BuildingData("Rune Workshop") },
            { "Deep Mine", new BuildingData("Deep Mine") },
            { "Volcanic Forge", new BuildingData("Volcanic Forge") },
            { "Mithril Smith", new BuildingData("Mithril Smith") },

            //fairy
            { "Garden", new BuildingData("Garden") },
            { "Fairy Circle", new BuildingData("Fairy Circle") },
            { "Berry Field", new BuildingData("Berry Field") },
            { "Dustery", new BuildingData("Dustery") },
            { "Mana Siphon", new BuildingData("Mana Siphon") },
            { "Cobbler", new BuildingData("Cobbler") },
            { "Rainbow", new BuildingData("Rainbow") },
            { "Illusionist", new BuildingData("Illusionist") },
            { "Luminous Garden", new BuildingData("Luminous Garden") },
            { "Tea Farm", new BuildingData("Tea Farm") },
            { "Worm Forest", new BuildingData("Worm Forest") },
            { "Fairy Alchemist", new BuildingData("Fairy Alchemist") },
            { "Spell Weaver", new BuildingData("Spell Weaver") },
            { "Crystalizer", new BuildingData("Crystalizer") },
            { "Fairy Seamstress", new BuildingData("Fairy Seamstress") },
            { "Fairy Jeweler", new BuildingData("Fairy Jeweler") },

            //elf
            { "Anima Tree", new BuildingData("Anima Tree") },
            { "Treant Grove", new BuildingData("Treant Grove") },
            { "Chicken Coup", new BuildingData("Chicken Coup") },
            { "Cotton Plantation", new BuildingData("Cotton Plantation") },
            { "Unicorn Stable", new BuildingData("Unicorn Stable") },
            { "Cotton Weaver", new BuildingData("Cotton Weaver") },
            { "Hatchery", new BuildingData("Hatchery") },
            { "Druid Circle", new BuildingData("Druid Circle") },
            { "Sugarcane Plantation", new BuildingData("Sugarcane Plantation") },
            { "Cake Bakery", new BuildingData("Cake Bakery") },
            { "Wand Maker", new BuildingData("Wand Maker") },
            { "Coffee Farm", new BuildingData("Coffee Farm") },
            { "Dark Wood Forest", new BuildingData("Dark Wood Forest") },
            { "Distillery", new BuildingData("Distillery") },
            { "Life Condenser", new BuildingData("Life Condenser") },
            { "Elvish Jeweler", new BuildingData("Elvish Jeweler") }
        };

        foreach (var building in buildingsData.Values)
        {
            building.baseCost = CalculateBuildingBaseCost(building.outputResource);
            building.researchBaseCost = CalculateBuildingResearchCost(building.outputResource);
        }

        cityData = new Dictionary<string, CityData>
        {
            { "Peasantry", new CityData("Peasantry") },
            { "Aquias", new CityData("Aquias") },
            { "Dwarvary", new CityData("Dwarvary") },
            { "Mountain Port", new CityData("Mountain Port") },
            { "Fairia", new CityData("Fairia") },
            { "Elveryn", new CityData("Elveryn") }
        };

        
    }

    public BigInteger CalculateBuildingBaseCost(string resourceName)
    {
        return resourcePrices[resourceName] * (2 + ResourceOrder(resourceName) / 3);
    }

    public BigInteger CalculateBuildingResearchCost(string resourceName)
    {
        return resourcePrices[resourceName] * (int)(1.3 + (float)ResourceOrder(resourceName) / 3);
    }

    public int ResourceOrder(string resourceName)
    {
        List<BigInteger> sortedPrices = new List<BigInteger>(resourcePrices.Values);
        sortedPrices.Sort();

        int index = sortedPrices.IndexOf(resourcePrices[resourceName]);
        return index;
    }

    public BuildingData GetBuildingData(string name)
    {
        return buildingsData[name];
    }

    public CityData GetCityData(string name)
    {
        return cityData[name];
    }
}