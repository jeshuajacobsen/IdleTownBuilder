using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class GameData
{
    private Dictionary<string, BuildingData> buildingsData;
    public GameData()
    {
        buildingsData = new Dictionary<string, BuildingData>();
    
        buildingsData.Add("Farm", new BuildingData("Farm"));
        buildingsData.Add("Forester", new BuildingData("Forester"));
        buildingsData.Add("Clay Pit", new BuildingData("Clay Pit"));
        buildingsData.Add("Lumber Mill", new BuildingData("Lumber Mill"));
        buildingsData.Add("Potter", new BuildingData("Potter"));
        buildingsData.Add("Stone Quarry", new BuildingData("Stone Quarry"));
        buildingsData.Add("Vegetable Farm", new BuildingData("Vegetable Farm"));
        buildingsData.Add("Hemp Farm", new BuildingData("Hemp Farm"));
        buildingsData.Add("Weaver", new BuildingData("Weaver"));
        buildingsData.Add("Copper Mine", new BuildingData("Copper Mine"));
        buildingsData.Add("Orchard", new BuildingData("Orchard"));
        buildingsData.Add("Tin Mine", new BuildingData("Tin Mine"));
        buildingsData.Add("Smelter", new BuildingData("Smelter"));
        buildingsData.Add("Wind Mill", new BuildingData("Wind Mill"));
        buildingsData.Add("Bakery", new BuildingData("Bakery"));
        buildingsData.Add("Vineyard", new BuildingData("Vineyard"));
        buildingsData.Add("Furniture Factory", new BuildingData("Furniture Factory"));
        buildingsData.Add("Pasture", new BuildingData("Pasture"));
        buildingsData.Add("Dairy", new BuildingData("Dairy"));
        buildingsData.Add("Barrel Maker", new BuildingData("Barrel Maker"));
        buildingsData.Add("Winery", new BuildingData("Winery"));
        buildingsData.Add("Butcher", new BuildingData("Butcher"));
        buildingsData.Add("Tannery", new BuildingData("Tannery"));
        buildingsData.Add("Paper Mill", new BuildingData("Paper Mill"));
        buildingsData.Add("Leather Shop", new BuildingData("Leather Shop"));
        buildingsData.Add("Human Jeweler", new BuildingData("Human Jeweler"));
        buildingsData.Add("Wizard University", new BuildingData("Wizard University"));
        
        //merfolk
        buildingsData.Add("Kelpery", new BuildingData("Kelpery"));
        buildingsData.Add("Reef", new BuildingData("Reef"));
        buildingsData.Add("Fishery", new BuildingData("Fishery"));
        buildingsData.Add("Reeds", new BuildingData("Reeds"));
        buildingsData.Add("Oystery", new BuildingData("Oystery"));
        buildingsData.Add("Basket Weaver", new BuildingData("Basket Weaver"));
        buildingsData.Add("Sand Banks", new BuildingData("Sand Banks"));
        buildingsData.Add("Manatee Pasture", new BuildingData("Manatee Pasture"));
        buildingsData.Add("Merite Cave", new BuildingData("Merite Cave"));
        buildingsData.Add("Manatee Dairy", new BuildingData("Manatee Dairy"));
        buildingsData.Add("Newt Swamp", new BuildingData("Newt Swamp"));
        buildingsData.Add("Squid Traps", new BuildingData("Squid Traps"));
        buildingsData.Add("Witch Hut", new BuildingData("Witch Hut"));
        buildingsData.Add("Crab Pots", new BuildingData("Crab Pots"));
        buildingsData.Add("Thermal Vents", new BuildingData("Thermal Vents"));
        buildingsData.Add("Rice Patties", new BuildingData("Rice Patties"));
        buildingsData.Add("Slime Milker", new BuildingData("Slime Milker"));
        buildingsData.Add("Aqua Forge", new BuildingData("Aqua Forge"));
        buildingsData.Add("Aqua Jeweler", new BuildingData("Aqua Jeweler"));
        buildingsData.Add("Aqua Armorer", new BuildingData("Aqua Armorer"));
        buildingsData.Add("Aqua Smith", new BuildingData("Aqua Smith"));

        //dwarf
        buildingsData.Add("Mushroom Cave", new BuildingData("Mushroom Cave"));
        buildingsData.Add("Mana Well", new BuildingData("Mana Well"));
        buildingsData.Add("Coal Mine", new BuildingData("Coal Mine"));
        buildingsData.Add("Iron Mine", new BuildingData("Iron Mine"));
        buildingsData.Add("Apiary", new BuildingData("Apiary"));
        buildingsData.Add("Iron Smelter", new BuildingData("Iron Smelter"));
        buildingsData.Add("Meadery", new BuildingData("Meadery"));
        buildingsData.Add("Gear Works", new BuildingData("Gear Works"));
        buildingsData.Add("Blacksmith", new BuildingData("Blacksmith"));
        buildingsData.Add("Hops Farm", new BuildingData("Hops Farm"));
        buildingsData.Add("Gold Mine", new BuildingData("Gold Mine"));
        buildingsData.Add("Glass Blower", new BuildingData("Glass Blower"));
        buildingsData.Add("Brewery", new BuildingData("Brewery"));
        buildingsData.Add("Gold Smelter", new BuildingData("Gold Smelter"));
        buildingsData.Add("Artificer", new BuildingData("Artificer"));
        buildingsData.Add("Jewel Mine", new BuildingData("Jewel Mine"));
        buildingsData.Add("Printing Press", new BuildingData("Printing Press"));
        buildingsData.Add("Manufactory", new BuildingData("Manufactory"));
        buildingsData.Add("Rune Workshop", new BuildingData("Rune Workshop"));
        buildingsData.Add("Deep Mine", new BuildingData("Deep Mine"));
        buildingsData.Add("Volcanic Forge", new BuildingData("Volcanic Forge"));
        buildingsData.Add("Mithril Smith", new BuildingData("Mithril Smith"));

        //fairy
        buildingsData.Add("Garden", new BuildingData("Garden"));
        buildingsData.Add("Fairy Circle", new BuildingData("Fairy Circle"));
        buildingsData.Add("Berry Field", new BuildingData("Berry Field"));
        buildingsData.Add("Dustery", new BuildingData("Dustery"));
        buildingsData.Add("Mana Siphon", new BuildingData("Mana Siphon"));
        buildingsData.Add("Cobbler", new BuildingData("Cobbler"));
        buildingsData.Add("Rainbow", new BuildingData("Rainbow"));
        buildingsData.Add("Illusionist", new BuildingData("Illusionist"));
        buildingsData.Add("Luminous Garden", new BuildingData("Luminous Garden"));
        buildingsData.Add("Tea Farm", new BuildingData("Tea Farm"));
        buildingsData.Add("Worm Forest", new BuildingData("Worm Forest"));
        buildingsData.Add("Fairy Alchemist", new BuildingData("Fairy Alchemist"));
        buildingsData.Add("Spell Weaver", new BuildingData("Spell Weaver"));
        buildingsData.Add("Crystalizer", new BuildingData("Crystalizer"));
        buildingsData.Add("Fairy Seamstress", new BuildingData("Fairy Seamstress"));
        buildingsData.Add("Fairy Jeweler", new BuildingData("Fairy Jeweler"));

        //elf
        buildingsData.Add("Anima Tree", new BuildingData("Anima Tree"));
        buildingsData.Add("Treant Grove", new BuildingData("Treant Grove"));
        buildingsData.Add("Chicken Coup", new BuildingData("Chicken Coup"));
        buildingsData.Add("Cotton Plantation", new BuildingData("Cotton Plantation"));
        buildingsData.Add("Unicorn Stable", new BuildingData("Unicorn Stable"));
        buildingsData.Add("Cotton Weaver", new BuildingData("Cotton Weaver"));
        buildingsData.Add("Hatchery", new BuildingData("Hatchery"));
        buildingsData.Add("Druid Circle", new BuildingData("Druid Circle"));
        buildingsData.Add("Sugarcane Plantation", new BuildingData("Sugarcane Plantation"));
        buildingsData.Add("Cake Bakery", new BuildingData("Cake Bakery"));
        buildingsData.Add("Wand Maker", new BuildingData("Wand Maker"));
        buildingsData.Add("Coffee Farm", new BuildingData("Coffee Farm"));
        buildingsData.Add("Dark Wood Forest", new BuildingData("Dark Wood Forest"));
        buildingsData.Add("Distillery", new BuildingData("Distillery"));
        buildingsData.Add("Life Condenser", new BuildingData("Life Condenser"));
        buildingsData.Add("Elvish Jeweler", new BuildingData("Elvish Jeweler"));

    }

    public BuildingData GetBuildingData(string name)
    {
        return buildingsData[name];
    }
}