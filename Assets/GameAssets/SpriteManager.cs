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
        }
        return wheat;
    }
}
