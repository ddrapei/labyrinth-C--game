namespace PlayerDisplay;


using Pastel;

// method to display stats of the player
public class PlayerStatsDisplay : IPlayerDisplay
{
    public void Display()
    {
        Player player = Player.GetInstance();

        Console.WriteLine();
        Console.WriteLine("=== Your Stats ===".Pastel("#00ff00"));
        Console.WriteLine("Health: " + player.Health.ToString().Pastel("#00ff00"));
        Console.WriteLine("Defense: " + player.Defense.ToString().Pastel("#1900ff"));
        Console.WriteLine("Attack Power: " + player.AttackPower.ToString().Pastel("#fc030f"));
        Console.WriteLine("Blocking Chance: " + (player.BlockingDamageChance * 100).ToString().Pastel("#00e5ff") + "%".Pastel("#00e5ff"));
        Console.WriteLine("Experience: " + player.Experience.ToString().Pastel("#00fc08") + " Exp".Pastel("#00fc08"));
        Console.WriteLine("Position: " + player.Xcoordinate + " : " + player.Ycoordinate);
        Console.WriteLine();
    }
}