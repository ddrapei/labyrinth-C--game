namespace PlayerLevelUp;

using Commands.DisplayCommands;
using Commands;
using Pastel;
using PlayerDisplay;


// Concrete implementation of level up
public class PlayerLevelUpBehavior : ILevelUpBehavior
{

    public PlayerLevelUpBehavior()
    {
    }
    public void LevelUp(Player player)
    {

        
        double diceRollForLevelUp = Random.Shared.NextDouble();
        
        if (player.Experience >= player.ExperienceRequiredForNewLevel)
        {
            
            int experienceCost = player.ExperienceRequiredForNewLevel;
            player.ExperienceRequiredForNewLevel += 50;
            
            if (diceRollForLevelUp < 0.25)
            {
                player.Level += 1;
                player.Health += 5;
                player.Experience -= experienceCost;

                Console.WriteLine($"You reached new level: {(player.Level.ToString().Pastel("#3236a8"))}");
                Console.WriteLine($"You gained " + "+ 5 health".Pastel("#1aff00"));
            } 
            else if (diceRollForLevelUp < 0.5)
            {
                player.Level += 1;
                player.AttackPower += 1;
                player.Experience -= experienceCost;

                Console.WriteLine($"You reached new level: {(player.Level.ToString().Pastel("#3236a8"))}");
                Console.WriteLine($"You gained " + "+ 1 attack power".Pastel("#1aff00"));
            }
            else if (diceRollForLevelUp < 0.75)
            {
                player.Level += 1;
                player.BlockingDamageChance += 0.01;
                player.Experience -= experienceCost;

                Console.WriteLine($"You reached new level: {(player.Level.ToString().Pastel("#3236a8"))}");
                Console.WriteLine($"You gained " + "+ 0.01% blocking damage chance".Pastel("#1aff00"));
            }  
            else
            {
                player.Level += 1;
                player.Inventory.MaxCapacity += 1;
                player.Experience -= experienceCost;

                Console.WriteLine($"You reached new level: {(player.Level.ToString().Pastel("#3236a8"))}");
                Console.WriteLine($"You gained " + "+ 1 inventory size".Pastel("#1aff00"));
            }
        }
    }
}