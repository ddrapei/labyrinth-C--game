namespace Perks;

using Items.Armour;
using Items.Armour.LeatherArmourSet;

using Pastel;

public class IncreaseDefensePerk : IPerk
{
    private int defense;

    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    public IncreaseDefensePerk(int defense)
    {
        this.defense = defense;
    }

    public void Apply(Player player)
    {
        player.Defense += defense;
        Console.WriteLine("You equipped 3 armour items from the same set".Pastel("#11ff00"));
        Console.WriteLine("The bonus is activated: Incrased defense " + "+ ".Pastel("#11ff00") + defense.ToString().Pastel("#11ff00"));
    }

    public void Remove(Player player)
    {
        player.Defense -= defense;
        Console.WriteLine("You no longer have bonus from an armour set");
        Console.WriteLine("The bonus is deactivated: defense - " + defense.ToString().Pastel("#ff0000"));
    }
}