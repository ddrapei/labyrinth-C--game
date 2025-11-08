using Items.Armour;

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
        Console.WriteLine("You equipped 3 armour items from the same set");
        Console.WriteLine("The bonus is activated: Incrased inventory + " + size);
    }

    public void Remove(Player player)
    {
        player.Inventory.MaxCapacity -= size;
        Console.WriteLine("You no longer have bonus from an armour set");
        Console.WriteLine("The bonus is deactivated: - " + size);
    }
}