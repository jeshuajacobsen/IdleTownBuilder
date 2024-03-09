using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class GameData
{
    private Dictionary<string, BuildingData> buildingsData;
    public GameData()
    {
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

    }

    public BuildingData GetBuildingData(string name)
    {
        return buildingsData[name];
    }
}