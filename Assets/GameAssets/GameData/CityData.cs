using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;

public class CityData
{
    public string cityName;
    public List<string> races;
    public int unlockCost;
    public List<string> demographics;
    public TaskData[] tasks;

    public CityData(string name)
    {
        cityName = name;
        switch(name)
        {
            case "Peasantry":
                unlockCost = 1;
                races = new List<string> {"Human"};
                demographics = new List<string> {"Peasants", "Commoners"};
                tasks = new TaskData[] {
                    new TaskData("PopulationGoal", 10, "Peasants"),
                    new TaskData("PopulationGoal", 2, "Commoners"), 
                    new TaskData("BuildingGoal", 12, "Farm")
                };
                break;
            case "Aquias":
                unlockCost = 2;
                races = new List<string> {"Human", "Merfolk"};
                demographics = new List<string> {"Peasants", "Commoners", "Surfs"};
                tasks = new TaskData[] {
                    new TaskData("PopulationGoal", 10, "Surfs"),
                    new TaskData("PopulationGoal", 10, "Commoners"), 
                    new TaskData("BuildingGoal", 10, "Reef")
                };
                break;
            case "Dwarvary":
                unlockCost = 2;
                races = new List<string> {"Human", "Dwarf"};
                demographics = new List<string> {"Peasants", "Commoners", "Miners"};
                tasks = new TaskData[] {
                    new TaskData("PopulationGoal", 10, "Miners"),
                    new TaskData("BuildingGoal", 10, "Mushroom Cave"), 
                    new TaskData("BuildingGoal", 5, "Mana Well")
                };
                break;
            case "Mountain Port":
                unlockCost = 5;
                races = new List<string> {"Human", "Merfolk", "Dwarf"};
                demographics = new List<string> {"Peasants", "Commoners", "Tradesmen", "Surfs", "Middle Mer", "Miners", "Workers"};
                tasks = new TaskData[] {
                    new TaskData("PopulationGoal", 10, "Middle Mer"),
                    new TaskData("PopulationGoal", 10, "Workers"), 
                    new TaskData("BuildingGoal", 10, "Iron Mine")
                };
                break;
            case "Fairia":
                unlockCost = 7;
                races = new List<string> {"Human", "Merfolk", "Dwarf", "Fairy"};
                demographics = new List<string> {"Peasants", "Commoners", "Tradesmen", "Patricians", "Wizards", "Surfs", "Middle Mer", "Sea Witches", "Mer-chants", "Miners", "Workers", "Mages", "Artificers", "Changelings", "Brownies", "Leprechauns"};
                tasks = new TaskData[] {
                    new TaskData("PopulationGoal", 10, "Peasants"),
                    new TaskData("PopulationGoal", 5, "Commoners"), 
                    new TaskData("BuildingGoal", 20, "Farm")
                };
                break;
            case "Elveryn":
                unlockCost = 10;
                races = new List<string> {"Human", "Merfolk", "Dwarf", "Elf"};
                demographics = new List<string> {"Peasants", "Commoners", "Tradesmen", "Patricians", "Wizards", "Surfs", "Middle Mer", "Sea Witches", "Mer-chants", "Miners", "Workers", "Mages", "Worker Elves", "House Elves", "Druids"};
                tasks = new TaskData[] {
                    new TaskData("PopulationGoal", 10, "Peasants"),
                    new TaskData("PopulationGoal", 5, "Commoners"), 
                    new TaskData("BuildingGoal", 20, "Farm")
                };
                break;

        }
    }

    public class TaskData
    {
        public string name;
        public BigInteger quantity;
        public string target;


        public TaskData(string name, BigInteger quantity, string target)
        {
            this.name = name;
            this.quantity = quantity;
            this.target = target;
        }
    }
}
