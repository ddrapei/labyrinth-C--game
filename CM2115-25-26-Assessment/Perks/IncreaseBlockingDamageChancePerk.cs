namespace Perks;

using Items.Armour;
using Items.Armour.CircusAcrobatArmourSet;

using Pastel;

public class IncreaseBlockingDamageChancePerk : IPerk
{
    private double increaseBlockingDamageChance;

    public double IncreaseBlockingDamageChance
    {
        get { return increaseBlockingDamageChance; }
        set { increaseBlockingDamageChance = value; }
    }

    public IncreaseBlockingDamageChancePerk(double increaseBlockingDamageChance)
    {
        this.increaseBlockingDamageChance = increaseBlockingDamageChance;
    }

    public void Apply(Player player)
    {
        player.BlockingDamageChance += increaseBlockingDamageChance;
        Console.WriteLine("You equipped 3 armour items from the same set".Pastel("#11ff00"));
        Console.WriteLine("The bonus is activated: chance to block attack is increased " + "+ ".Pastel("#11ff00") + player.BlockingDamageChance.ToString().Pastel("#11ff00"));
    }

    public void Remove(Player player)
    {
        player.BlockingDamageChance -= increaseBlockingDamageChance;
        Console.WriteLine("You no longer have bonus from an armour set");
        Console.WriteLine("The bonus is deactivated: chance to block attack is increased - " + player.BlockingDamageChance.ToString().Pastel("#ff0000"));
    }
}