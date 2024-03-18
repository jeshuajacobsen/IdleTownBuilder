using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Globalization;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager instance;

    public Sprite bread;
    public Sprite bronzeIngot;
    public Sprite clay;
    public Sprite copperOre;
    public Sprite flour;
    public Sprite furniture;
    public Sprite lumber;
    public Sprite pottery;
    public Sprite stone;
    public Sprite tinOre;
    public Sprite vegetables;
    public Sprite wheat;
    public Sprite wood;
    public Sprite hemp;
    public Sprite clothes;
    public Sprite fruit;
    public Sprite grapes;
    public Sprite cow;
    public Sprite milk;
    public Sprite barrel;
    public Sprite wine;
    public Sprite beef;
    public Sprite leather;
    public Sprite paper;
    public Sprite leatherClothes;
    public Sprite humanJewelry;
    public Sprite highArcana;

    //merfolk
    public Sprite kelp;
    public Sprite coral;
    public Sprite fish;
    public Sprite reed;
    public Sprite pearl;
    public Sprite basket;
    public Sprite sand;
    public Sprite manatee;
    public Sprite meriteOre;
    public Sprite eyeOfNewt;
    public Sprite ink;
    public Sprite curse;
    public Sprite crab;
    public Sprite magmaSlug;
    public Sprite rice;
    public Sprite fireSlime;
    public Sprite meriteIngot;
    public Sprite merJewelry;
    public Sprite mermail;
    public Sprite trident;

    //dwarf
    public Sprite mushroom;
    public Sprite mana;
    public Sprite coal;
    public Sprite ironOre;
    public Sprite honey;
    public Sprite ironIngot;
    public Sprite mead;
    public Sprite mechanicalParts;
    public Sprite tools;
    public Sprite hops;
    public Sprite goldOre;
    public Sprite glass;
    public Sprite beer;
    public Sprite goldIngot;
    public Sprite artifact;
    public Sprite jewel;
    public Sprite book;
    public Sprite golem;
    public Sprite rune;
    public Sprite mithrilOre;
    public Sprite mithrilIngot;
    public Sprite mithrilArmor;

    //Fairies
    public Sprite flowers;
    public Sprite magicMushroom;
    public Sprite berry;
    public Sprite fairyDust;
    public Sprite shoes;
    public Sprite luckCharm;
    public Sprite beautyCharm;
    public Sprite lightBulb;
    public Sprite tea;
    public Sprite silk;
    public Sprite fairyelium;
    public Sprite whisperSilk;
    public Sprite fairyCrystal;
    public Sprite invisibilityCloak;
    public Sprite fairyJewelry;

    public Sprite animaFruit;
    public Sprite livingWood;
    public Sprite egg;
    public Sprite cotton;
    public Sprite unicornHair;
    public Sprite cottonClothes;
    public Sprite chicken;
    public Sprite enchantment;
    public Sprite sugar;
    public Sprite cake;
    public Sprite wand;
    public Sprite coffee;
    public Sprite darkWood;
    public Sprite liqour;
    public Sprite lifeGem;
    public Sprite elvishJewelry;


    //Buildings

    public Sprite farm;
    public Sprite forester;
    public Sprite clayPit;
    public Sprite lumberMill;
    public Sprite potter;
    public Sprite stoneQuarry;
    public Sprite vegetableFarm;
    public Sprite hempFarm;
    public Sprite weaver;
    public Sprite copperMine;
    public Sprite orchard;
    public Sprite tinMine;
    public Sprite smelter;
    public Sprite windMill;
    public Sprite bakery;
    public Sprite vineyard;
    public Sprite furnitureFactory;
    public Sprite pasture;
    public Sprite dairy;
    public Sprite barrelMaker;
    public Sprite winery;
    public Sprite butcher;
    public Sprite tannery;
    public Sprite paperMill;
    public Sprite leatherShop;
    public Sprite humanJeweler;
    public Sprite wizardUniversity;

            //merfolk
            
            
    public Sprite kelpery;
    public Sprite reef;
    public Sprite fishery;
    public Sprite reeds;
    public Sprite oystery;
    public Sprite basketWeaver;
    public Sprite sandBanks;
    public Sprite manateePasture;
    public Sprite meriteCave;
    public Sprite manateeDairy;
    public Sprite newtSwamp;
    public Sprite squidTraps;
    public Sprite witchHut;
    public Sprite crabPots;
    public Sprite thermalVents;
    public Sprite ricePatties;
    public Sprite slimeMilker;
    public Sprite aquaforge;
    public Sprite aquaJeweler;
    public Sprite aquaArmorer;
    public Sprite aquaSmith;

            //dwarves
    public Sprite mushroomCave;
    public Sprite manaWell;
    public Sprite coalMine;
    public Sprite ironMine;
    public Sprite apiary;
    public Sprite ironSmelter;
    public Sprite meadery;
    public Sprite gearWorks;
    public Sprite blacksmith;
    public Sprite hopsFarm;
    public Sprite goldMine;
    public Sprite glassBlower;
    public Sprite brewery;
    public Sprite goldSmelter;
    public Sprite artificer;
    public Sprite jewelMine;
    public Sprite printingPress;
    public Sprite manufactory;
    public Sprite runeWorkshop;
    public Sprite deepMine;
    public Sprite volcanicForge;
    public Sprite mithrilSmith;

    //Fairy
    public Sprite garden;
    public Sprite fairyCircle;
    public Sprite berryField;
    public Sprite dustery;
    public Sprite manaSiphon;
    public Sprite cobbler;
    public Sprite rainbow;
    public Sprite illusionist;
    public Sprite luminousGarden;
    public Sprite teaFarm;
    public Sprite wormForest;
    public Sprite fairyAlchemist;
    public Sprite spellWeaver;
    public Sprite crystalizer;
    public Sprite fairySeamstress;
    public Sprite fairyJeweler;

    //elf 
    public Sprite animaTree;
    public Sprite treantGrove;
    public Sprite chickenCoup;
    public Sprite cottonPlantation;
    public Sprite unicornStable;
    public Sprite cottonWeaver;
    public Sprite hatchery;
    public Sprite druidCircle;
    public Sprite sugarcanePlantation;
    public Sprite cakeBakery;
    public Sprite wandMaker;
    public Sprite coffeeFarm;
    public Sprite darkWoodForest;
    public Sprite distillery;
    public Sprite lifeCondenser;
    public Sprite elvishJeweler;

    //Demos
    //Human
    public Sprite peasant;
    public Sprite commoner;
    public Sprite tradesman;
    public Sprite patrician;
    public Sprite wizard;
    public Sprite noble;
    public Sprite royalty;

    //Merfolk
    public Sprite surfs;
    public Sprite middleMer;
    public Sprite seaWitches;
    public Sprite merchants;
    public Sprite highMer;
    public Sprite tritons;

    //dwarf
    public Sprite miners;
    public Sprite workers;
    public Sprite mages;
    public Sprite artificers;
    public Sprite dwarfLord;

    //Fairy
    public Sprite changelings;
    public Sprite brownies;
    public Sprite leprechauns;
    public Sprite selkies;
    public Sprite clurichauns;
    public Sprite aosSi;

    //Elf
    public Sprite workerElves;
    public Sprite houseElves;
    public Sprite druids;
    public Sprite highElves;
    public Sprite perfects;



    //interface
    public Sprite fillBarGreen;
    public Sprite fillBarYellow;
    public Sprite fillBarRed;

    public Sprite humanCrest;
    public Sprite merfolkCrest;
    public Sprite dwarfCrest;
    public Sprite fairyCrest;
    public Sprite elfCrest;

    public Sprite managerButton;

    public Sprite managerBackgroundCommon;
    public Sprite managerBackgroundUncommon;
    public Sprite managerBackgroundRare;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite GetInterfaceSprite(string name)
    {
        switch (name) {
            case "FillBarGreen":
                return fillBarGreen;
            case "FillBarYellow":
                return fillBarYellow;
            case "FillBarRed":
                return fillBarRed;
            case "HumanCrest":
                return humanCrest;
            case "MerfolkCrest":
                return merfolkCrest;
            case "DwarfCrest":
                return dwarfCrest;
            case "FairyCrest":
                return fairyCrest;
            case "ElfCrest":
                return elfCrest;
            case "ManagerButton":
                return managerButton;
            case "ManagerBackgroundCommon":
                return managerBackgroundCommon;
            case "ManagerBackgroundUncommon":
                return managerBackgroundUncommon;
            case "ManagerBackgroundRare":
                return managerBackgroundRare;
        }
        return fillBarGreen;
    }

    public Sprite GetResourceSprite(string name)
    {
        switch (name) {
            case "Bread":
                return bread;
            case "Bronze Ingot":
                return bronzeIngot;
            case "Clay":
                return clay;
            case "Copper Ore":
                return copperOre;
            case "Flour":
                return flour;
            case "Furniture":
                return furniture;
            case "Lumber":
                return lumber;
            case "Pottery":
                return pottery;
            case "Stone":
                return stone;
            case "Tin Ore":
                return tinOre;
            case "Vegetables":
                return vegetables;
            case "Wheat":
                return wheat;
            case "Wood":
                return wood;
            case "Hemp":
                return hemp;
            case "Clothes":
                return clothes;
            case "Fruit":
                return fruit;
            case "Grapes":  
                return grapes;
            case "Cow":
                return cow;
            case "Milk":
                return milk;
            case "Barrel":
                return barrel;
            case "Wine":
                return wine;
            case "Beef":    
                return beef;
            case "Leather":
                return leather;
            case "Paper":
                return paper;
            case "Leather Clothes":
                return leatherClothes;
            case "Human Jewelry":
                return humanJewelry;
            case "High Arcana":
                return highArcana;

            //merfolk
            case "Kelp":
                return kelp;
            case "Coral":
                return coral;
            case "Fish":
                return fish;
            case "Reed":
                return reed;
            case "Pearl":
                return pearl;
            case "Basket":
                return basket;
            case "Sand":
                return sand;
            case "Manatee":
                return manatee;
            case "Merite Ore":
                return meriteOre;
            case "Eye of Newt":
                return eyeOfNewt;
            case "Ink":
                return ink;
            case "Curse":
                return curse;
            case "Crab":
                return crab;
            case "Magma Slug":
                return magmaSlug;
            case "Rice":
                return rice;
            case "Fire Slime":
                return fireSlime;
            case "Merite Ingot":
                return meriteIngot;
            case "Mer Jewelry":
                return merJewelry;
            case "Mermail":
                return mermail;
            case "Trident":
                return trident;

            //dwarf
            case "Mushroom":
                return mushroom;
            case "Mana":
                return mana;
            case "Coal":
                return coal;
            case "Iron Ore":
                return ironOre;
            case "Honey":
                return honey;
            case "Iron Ingot":
                return ironIngot;
            case "Mead":
                return mead;
            case "Mechanical Parts":
                return mechanicalParts;
            case "Tools":
                return tools;
            case "Hops":
                return hops;
            case "Gold Ore":
                return goldOre;
            case "Glass":
                return glass;
            case "Beer":
                return beer;
            case "Gold Ingot":
                return goldIngot;
            case "Artifact":
                return artifact;
            case "Golem":
                return golem;
            case "Rune":
                return rune;
            case "Mithril Ore":
                return mithrilOre;
            case "Mithril Ingot":
                return mithrilIngot;
            case "Mithril Armor":
                return mithrilArmor;

            //Fairy
            case "Flowers":
                return flowers;
            case "Fairy Circle":    
                return magicMushroom;
            case "Berry":
                return berry;
            case "Fairy Dust":
                return fairyDust;
            case "Shoes":
                return shoes;
            case "Luck Charm":  
                return luckCharm;
            case "Beauty Charm":
                return beautyCharm;
            case "Light Bulb":
                return lightBulb;
            case "Tea":
                return tea;
            case "Silk":
                return silk;
            case "Fairyelium":
                return fairyelium;
            case "Whisper Silk":
                return whisperSilk;
            case "Fairy Crystal":
                return fairyCrystal;
            case "Invisibility Cloak":
                return invisibilityCloak;
            case "Fairy Jewelry":
                return fairyJewelry;

            //elf
            case "Anima Fruit":
                return animaFruit;
            case "Living Wood":
                return livingWood;
            case "Egg":
                return egg;
            case "Cotton":
                return cotton;
            case "Unicorn Hair":
                return unicornHair;
            case "Cotton Clothes":
                return cottonClothes;
            case "Chicken":
                return chicken;
            case "Enchantment":
                return enchantment;
            case "Sugar":
                return sugar;
            case "Cake":
                return cake;
            case "Wand":
                return wand;
            case "Coffee":
                return coffee;
            case "Dark Wood":
                return darkWood;
            case "Liqour":
                return liqour;
            case "Life Gem":
                return lifeGem;
            case "Elvish Jewelry":
                return elvishJewelry;
            

        }
        return wheat;
    }

    public Sprite GetBuildingSprite(string name)
    {
        switch (name) {
            
            case "Farm":
                return farm;
            case "Forester":
                return forester;
            case "Clay Pit":
                return clayPit;
            case "Lumber Mill":
                return lumberMill;
            case "Potter":
                return potter;
            case "Stone Quarry":
                return stoneQuarry;
            case "Vegetable Farm":
                return vegetableFarm;
            case "Hemp Farm":
                return hempFarm;
            case "Weaver":
                return weaver;
            case "Copper Mine":
                return copperMine;
            case "Orchard":
                return orchard;
            case "Tin Mine":
                return tinMine;
            case "Smelter":
                return smelter;
            case "Wind Mill":
                return windMill;
            case "Bakery":
                return bakery;
            case "Vineyard":
                return vineyard;
            case "Furniture Factory":
                return furnitureFactory;
            case "Pasture":
                return pasture;
            case "Dairy":
                return dairy;
            case "Barrel Maker":
                return barrelMaker;
            case "Winery":
                return winery;
            case "Butcher":
                return butcher;
            case "Tannery":
                return tannery;
            case "Paper Mill":
                return paperMill;
            case "Leather Shop":
                return leatherShop;
            case "Human Jeweler":
                return humanJeweler;
            case "Wizard University": 
                return wizardUniversity;

            //merfolk
            
            
            case "Kelpery":
                return kelpery;
            case "Reef":
                return reef;
            case "Fishery":
                return fishery;
            case "Reeds":
                return reeds;
            case "Oystery":
                return oystery;
            case "Basket Weaver":
                return basketWeaver;
            case "Sand Banks":
                return sandBanks;
            case "Manatee Pasture":
                return manateePasture;
            case "Merite Cave":
                return meriteCave;
            case "Manatee Dairy":
                return manateeDairy;
            case "Newt Swamp":
                return newtSwamp;
            case "Squid Traps":
                return squidTraps;
            case "Witch Hut":
                return witchHut;
            case "Crab Pots":
                return crabPots;
            case "Thermal Vents":
                return thermalVents;
            case "Rice Patties":
                return ricePatties;
            case "Slime Milker":
                return slimeMilker;
            case "Aqua Forge":
                return aquaforge;
            case "Aqua Jeweler":
                return aquaJeweler;
            case "Aqua Armorer":
                return aquaArmorer;
            case "Aqua Smith":
                return aquaSmith;

            //dwarves
            case "Mushroom Cave":
                return mushroomCave;
            case "Mana Well":
                return manaWell;
            case "Coal Mine":
                return coalMine;
            case "Iron Mine":
                return ironMine;
            case "Apiary":
                return apiary;
            case "Iron Smelter":
                return ironSmelter;
            case "Meadery":
                return meadery;
            case "Gear Works":
                return gearWorks;
            case "Blacksmith":
                return blacksmith;
            case "Hops Farm":
                return hopsFarm;
            case "Gold Mine":
                return goldMine;
            case "Glass Blower":
                return glassBlower;
            case "Brewery":
                return brewery;
            case "Gold Smelter":
                return goldSmelter;
            case "Artificer":
                return artificer;
            case "Jewel Mine":
                return jewelMine;
            case "Printing Press":
                return printingPress;
            case "Manufactory":
                return manufactory;
            case "Rune Workshop":
                return runeWorkshop;
            case "Deep Mine":
                return deepMine;
            case "Volcanic Forge":
                return volcanicForge;
            case "Mithril Smith":
                return mithrilSmith;
            
            //Fairy
            case "Garden":
                return garden;
            case "Fairy Circle":
                return fairyCircle;
            case "Berry Field":
                return berryField;
            case "Dustery":
                return dustery;
            case "Mana Siphon":
                return manaSiphon;
            case "Cobbler":
                return cobbler;
            case "Rainbow":
                return rainbow;
            case "Illusionist":
                return illusionist;
            case "Luminous Garden":
                return luminousGarden;
            case "Tea Farm":
                return teaFarm;
            case "Worm Forest":
                return wormForest;
            case "Fairy Alchemist":
                return fairyAlchemist;
            case "Spell Weaver":
                return spellWeaver;
            case "Crystalizer":
                return crystalizer;
            case "Fairy Seamstress":
                return fairySeamstress;
            case "Fairy Jeweler":
                return fairyJeweler;

            //elf
            case "Anima Tree":
                return animaTree;
            case "Treant Grove":
                return treantGrove;
            case "Chicken Coup":
                return chickenCoup;
            case "Cotton Plantation":
                return cottonPlantation;
            case "Unicorn Stable":
                return unicornStable;
            case "Cotton Weaver":
                return cottonWeaver;
            case "Hatchery":
                return hatchery;
            case "Druid Circle":
                return druidCircle;
            case "Sugarcane Plantation":
                return sugarcanePlantation;
            case "Cake Bakery":
                return cakeBakery;
            case "Wand Maker":
                return wandMaker;
            case "Coffee Farm":
                return coffeeFarm;
            case "Dark Wood Forest":
                return darkWoodForest;
            case "Distillery":
                return distillery;
            case "Life Condenser":
                return lifeCondenser;
            case "Elvish Jeweler":
                return elvishJeweler;
            
        }
        return farm;
    }

    public Sprite GetDemoSprite(string name)
    {
        switch (name) {
            case "Peasants":
                return peasant;
            case "Commoners":
                return commoner;
            case "Tradesmen":
                return tradesman;
            case "Patricians":
                return patrician;
            case "Wizards":
                return wizard;
            case "Nobles":
                return noble;
            case "Royalty":
                return royalty;
            
            //Merfolk
            case "Surfs":
                return surfs;
            case "Middle Mer":
                return middleMer;
            case "Sea Witches":
                return seaWitches;
            case "Mer-chants":
                return merchants;
            case "High Mer":
                return highMer;
            case "Tritons":
                return tritons;

            //Dwarf
            case "Miners":
                return miners;
            case "Workers":
                return workers;
            case "Mages":
                return mages;
            case "Artificers":
                return artificers;
            case "Dwarf Lords":
                return dwarfLord;

            //Fairy
            case "Changelings":
                return changelings;
            case "Brownies":
                return brownies;
            case "Leprechauns":
                return leprechauns;
            case "Selkies":
                return selkies;
            case "Clurichaun":
                return clurichauns;
            case "Aos Si":
                return aosSi;

            //elf
            case "Worker Elves":
                return workerElves;
            case "House Elves":
                return houseElves;
            case "Druids":
                return druids;
            case "High Elves":
                return highElves;
            case "Perfects":
                return perfects;
        }
        return farm;
    }

    public Sprite GetManagerSprite(string name)
    {
        switch (name) {
            case "Wedge":
                return commoner;
            case "Biggs":
                return workerElves;
        }
        return commoner;
    }

    public static string ToCamelCase(string titleCaseStr)
    {
        if (string.IsNullOrEmpty(titleCaseStr))
            return titleCaseStr;

        // Split the string into words
        string[] words = titleCaseStr.Split(' ');

        // Process the first word: make it lowercase
        words[0] = words[0].ToLower(CultureInfo.InvariantCulture);

        // Process the subsequent words: capitalize the first letter and make the rest lowercase
        for (int i = 1; i < words.Length; i++)
        {
            if (words[i].Length > 1)
            {
                words[i] = char.ToUpper(words[i][0], CultureInfo.InvariantCulture) + words[i].Substring(1).ToLower(CultureInfo.InvariantCulture);
            }
        }

        // Combine the words back into a single string
        return string.Concat(words);
    }
}
