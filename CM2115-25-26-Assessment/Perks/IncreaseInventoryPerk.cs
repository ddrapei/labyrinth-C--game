namespace Perks;

using Items.Armour;
using Items.Armour.LeatherArmourSet;

using Pastel;

public class IncreaseInventoryPerk : IPerk
{
    private int size;

    public int Size
    {
        get { return size; }
        set { size = value; }
    }

    public IncreaseInventoryPerk(int size)
    {
        this.size = size;
    }

    public void Apply(Player player)
    {
        player.Inventory.MaxCapacity += size;
        Console.WriteLine("You equipped 3 armour items from the same set".Pastel("#11ff00"));
        Console.WriteLine("The bonus is activated: Incrased inventory " + "+ ".Pastel("#11ff00") + size.ToString().Pastel("#11ff00"));
    }

    public void Remove(Player player)
    {
        player.Inventory.MaxCapacity -= size;
        Console.WriteLine("You no longer have bonus from an armour set");
        Console.WriteLine("The bonus is deactivated: inventory size - " + size.ToString().Pastel("#ff0000"));
    }
}