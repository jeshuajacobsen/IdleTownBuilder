using System.Numerics;

public class PrestigeResearchData
{
    public string title;
    public string description;
    public BigInteger baseCost;
    public int maxLevel;
    public int tier;
    public string race;
    public PrestigeResearchData(string title)
    {
        this.title = title;
    

        switch(title)
        {
            case "Human Tech":
                description = "Increase the production of all Human buildings by 10%.";
                baseCost = 100;
                maxLevel = 10;
                tier = 3;
                race = "Human";
                break;
            case "Production Speed":
                description = "Increases global production speed by 5%.";
                baseCost = 1000;
                maxLevel = 10;
                tier = 2;
                race = "Human";
                break;
            case "Peasanting":
                description = "Increases peasant base prestige output by 1.";
                baseCost = 100;
                maxLevel = 10;
                tier = 1;
                race = "Human";
                break;
            case "Marketing":
                description = "Increases value of goods in the market by 10%.";
                baseCost = 100;
                maxLevel = 10;
                tier = 3;
                race = "Human";
                break;
            case "Tap Power":
                description = "Increases tapping power by 10%.";
                baseCost = 100;
                maxLevel = 10;
                tier = 2;
                race = "Human";
                break;
            case "Merfolk Tech":
                description = "Increase the production of all Merfolk buildings by 10%";
                baseCost = 10000000;
                maxLevel = 10;
                tier = 3;
                race = "Merfolk";
                break;
            case "Max Away Time":
                description = "Increases max away time by 20 minutes.";
                baseCost = 10000;
                maxLevel = 10;
                tier = 1;
                race = "Merfolk";
                break;
            case "Population Growth":
                description = "Increases the growth rate of all populations by 5%.";
                baseCost = 1000000;
                maxLevel = 10;
                tier = 2;
                race = "Merfolk";
                break;
            case "Dwarf Tech":
                description = "Increase the production of all Dwarf buildings by 10%";
                baseCost = 10000000;
                maxLevel = 10;
                tier = 3;
                race = "Dwarf";
                break;
            case "Population Capacity":
                description = "Increases the maximum population for all demographics by 5%.";
                baseCost = 1000000;
                maxLevel = 10;
                tier = 2;
                race = "Dwarf";
                break;
            case "Fairy Tech":
                description = "Increase the production of all Fairy buildings by 10%";
                baseCost = 1000000;
                maxLevel = 10;
                tier = 3;
                race = "Fairy";
                break;
            case "Elf Tech":
                description = "Increase the production of all Elf buildings by 10%";
                baseCost = 1000000;
                maxLevel = 10;
                tier = 23;
                race = "Elf";
                break;
            case "Human capacity":
                description = "Increases the maximum population of Humans by 1.";
                baseCost = 100;
                maxLevel = 10;
                tier = 1;
                race = "Human";
                break;
            case "Merfolk capacity":
                description = "Increases the maximum population of Merfolk by 1.";
                baseCost = 20000;
                maxLevel = 10;
                tier = 1;
                race = "Merfolk";
                break;
            case "Dwarf capacity":
                description = "Increases the maximum population of Dwarves by 1.";
                baseCost = 20000;
                maxLevel = 10;
                tier = 1;
                race = "Dwarf";
                break;
            case "Fairy capacity":
                description = "Increases the maximum population of Fairies by 1.";
                baseCost = 1000000;
                maxLevel = 10;
                tier = 1;
                race = "Fairy";
                break;
            case "Elf capacity":
                description = "Increases the maximum population of Elves by 1.";
                baseCost = 1000000;
                maxLevel = 10;
                tier = 1;
                race = "Elf";
                break;
            case "Human Growth":
                description = "Increases the growth rate of Humans by 10%.";
                baseCost = 100;
                maxLevel = 10;
                tier = 2;
                race = "Human";
                break;
            case "Merfolk Growth":
                description = "Increases the growth rate of Merfolk by 10%.";
                baseCost = 20000;
                maxLevel = 10;
                tier = 2;
                race = "Merfolk";
                break;
            case "Dwarf Growth":
                description = "Increases the growth rate of Dwarves by 10%.";
                baseCost = 20000;
                maxLevel = 10;
                tier = 2;
                race = "Dwarf";
                break;
            case "Fairy Growth":
                description = "Increases the growth rate of Fairies by 10%.";
                baseCost = 1000000;
                maxLevel = 10;
                tier = 2;
                race = "Fairy";
                break;
            case "Elf Growth":
                description = "Increases the growth rate of Elves by 10%.";
                baseCost = 1000000;
                maxLevel = 10;
                tier = 2;
                race = "Elf";
                break;
            
        }
    }
}