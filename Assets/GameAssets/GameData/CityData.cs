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
                    new TaskData("PopulationGoal", 2, "Peasants"),
                    new TaskData("PopulationGoal", 2, "Commoners"), 
                    new TaskData("BuildingGoal", 20, "Farm")
                };
                break;
            case "Aquias":
                unlockCost = 300;
                races = new List<string> {"Human", "Merfolk"};
                demographics = new List<string> {"Peasants", "Commoners", "Surfs"};
                tasks = new TaskData[] {
                    new TaskData("PopulationGoal", 10, "Peasants"),
                    new TaskData("PopulationGoal", 5, "Commoners"), 
                    new TaskData("BuildingGoal", 20, "Farm")
                };
                break;
            case "Dwarvary":
                unlockCost = 300;
                races = new List<string> {"Human", "Dwarf"};
                demographics = new List<string> {"Peasants", "Commoners", "Miners"};
                tasks = new TaskData[] {
                    new TaskData("PopulationGoal", 10, "Peasants"),
                    new TaskData("PopulationGoal", 5, "Commoners"), 
                    new TaskData("BuildingGoal", 20, "Farm")
                };
                break;
            case "Mountain Port":
                unlockCost = 300;
                races = new List<string> {"Human", "Merfolk", "Dwarf"};
                demographics = new List<string> {"Peasants", "Commoners", "Tradesmen", "Surfs", "Middle Mer", "Miners", "Workers"};
                tasks = new TaskData[] {
                    new TaskData("PopulationGoal", 10, "Peasants"),
                    new TaskData("PopulationGoal", 5, "Commoners"), 
                    new TaskData("BuildingGoal", 20, "Farm")
                };
                break;
            case "Fairia":
                unlockCost = 1000;
                races = new List<string> {"Human", "Merfolk", "Dwarf", "Fairy"};
                demographics = new List<string> {"Peasants", "Commoners", "Tradesmen", "Patricians", "Wizards", "Surfs", "Middle Mer", "Sea Witches", "Mer-chants", "Miners", "Workers", "Mages", "Artificers", "Changelings", "Brownies", "Leprechauns"};
                tasks = new TaskData[] {
                    new TaskData("PopulationGoal", 10, "Peasants"),
                    new TaskData("PopulationGoal", 5, "Commoners"), 
                    new TaskData("BuildingGoal", 20, "Farm")
                };
                break;
            case "Elveryn":
                unlockCost = 1000;
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
