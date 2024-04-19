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
                tier = 4;
                race = "Human";
                break;
            case "Production Speed":
                description = "Increases global production speed by 5%.";
                baseCost = 100;
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
                tier = 1;
                race = "Human";
                break;
            case "Merfolk Tech":
                description = "Increase the production of all Merfolk buildings by 10%";
                baseCost = 100;
                maxLevel = 10;
                tier = 4;
                race = "Merfolk";
                break;
            case "Max Away Time":
                description = "Increases max away time by 20 minutes.";
                baseCost = 100;
                maxLevel = 10;
                tier = 1;
                race = "Merfolk";
                break;

        }
    }
}