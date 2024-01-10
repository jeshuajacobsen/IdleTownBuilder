using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Sprite tinOre;
    public Sprite vegetables;
    public Sprite wheat;
    public Sprite wood;

    //merfolk
    public Sprite kelp;
    public Sprite coral;
    public Sprite fish;
    public Sprite pearl;
    public Sprite meriteOre;
    public Sprite magmaSlug;
    public Sprite fireSlime;
    public Sprite meriteIngot;

    //dwarf
    public Sprite mushroom;
    public Sprite mana;
    public Sprite mechanicalParts;
    public Sprite artifact;
    public Sprite golem;


    //Buildings
    //human
    public Sprite clayPit;
    public Sprite copperMine;
    public Sprite farm;
    public Sprite forester;
    public Sprite lumberMill;
    public Sprite potter;
    public Sprite vegetableFarm;
    public Sprite bakery;
    public Sprite furnitureFactory;
    public Sprite tinMine;
    public Sprite windMill;

    //merfolk
    public Sprite aquaforge;
    public Sprite fishery;
    public Sprite kelpery;
    public Sprite meriteCave;
    public Sprite oystery;
    public Sprite reef;
    public Sprite slimeMilker;
    public Sprite thermalVents;

    //dwarves
    public Sprite artificer;
    public Sprite gearWorks;
    public Sprite golemManufactory;
    public Sprite manaWell;
    public Sprite mushroomCave;
    

    //Demos
    //Human
    public Sprite peasant;
    public Sprite commoner;
    public Sprite tradesman;
    public Sprite patrician;
    public Sprite wizard;
    public Sprite noble;
    public Sprite royalty;


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
            case "Tin Ore":
                return tinOre;
            case "Vegetables":
                return vegetables;
            case "Wheat":
                return wheat;
            case "Wood":
                return wood;

            //merfolk
            case "Kelp":
                return kelp;
            case "Coral":
                return coral;
            case "Fish":
                return fish;
            case "Pearl":
                return pearl;
            case "Merite Ore":
                return meriteOre;
            case "Magma Slug":
                return magmaSlug;
            case "Fire Slime":
                return fireSlime;
            case "Merite Ingot":
                return meriteIngot;

            //dwarf
            case "Mushroom":
                return mushroom;
            case "Mana":
                return mana;
            case "Mechanical Parts":
                return mechanicalParts;
            case "Artifact":
                return artifact;
            case "Golem":
                return golem;

        }
        return wheat;
    }

    public Sprite GetBuildingSprite(string name)
    {
        switch (name) {
            case "Clay Pit":
                return clayPit;
            case "Farm":
                return farm;
            case "Forester":
                return forester;
            case "Lumber Mill":
                return lumberMill;
            case "Potter":
                return potter;
            case "Vegetable Farm":
                return vegetableFarm;
            case "Bakery":
                return bakery;
            case "Copper Mine":
                return copperMine;
            case "Furniture Factory":
                return furnitureFactory;
            case "Tin Mine":
                return tinMine;
            case "Wind Mill":
                return windMill;


            //merfolk
            case "Aquaforge":
                return aquaforge;
            case "Fishery":
                return fishery;
            case "Kelpery":
                return kelpery;
            case "Merite Cave":
                return meriteCave;
            case "Oystery":
                return oystery;
            case "Reef":
                return reef;
            case "Slime Milker":
                return slimeMilker;
            case "Thermal Vents":
                return thermalVents;

            //dwarves
            case "Artificer":
                return artificer;
            case "Gear Works":
                return gearWorks;
            case "Golem Manufactory":
                return golemManufactory;
            case "Mana Well":
                return manaWell;
            case "Mushroom Cave":
                return mushroomCave;
            
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
        }
        return farm;
    }

}
