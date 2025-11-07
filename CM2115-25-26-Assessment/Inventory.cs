using System.Globalization;
using Items;

public class Inventory
{
    private List<Item> items;
    private int maxCapacity;

    public List<Item> Items
    {
        get { return items; }
    }

    public int MaxCapacity
    {
        get { return maxCapacity; }
        set { maxCapacity = value; }
    }

    // --- Constructor ---

    public Inventory(int maxCapacity = 10)
    {
        this.items = new List<Item>();
        this.maxCapacity = maxCapacity;
    }

    // function to collect the item from the room, returns true if successful or false if it can't be done
    public bool AddItem(Item item)
    {
        if (items.Count >= maxCapacity)
        {
            Console.WriteLine("Inventory is full. Cannot pick up the item");
            return false;
        }

        items.Add(item);
        Console.WriteLine(item.Name + " has been added to the inventory.");
        return true;
    }

    // function to drop the item from the inventory, returns true if successful or false if it can't be done
    public bool DropItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Console.WriteLine(item.Name + " has been removed from the invenory");
            return true;
        }

        Console.WriteLine("The item can not be removed.");
        return false;
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }

    // returns item by its number in the inventory
    public Item GetItemByNumber(int number)
    {
        // list starts from 0, however the player's interface is going to show 1 as the first item
        int index = number - 1;

        // checks if the index is viable for the current inventory list
        if (index >= 0 && index < Items.Count)
        {
            return items[index];
        }

        // returns null if there is no item with this index
        return null;
    }

    public Item GetItemByName (string name)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Name.ToLower() == name.ToLower())
            {
                return items[i];
            }
        }
        return null;
    }

    public void DisplayInventory()
    {
        if (items.Count == 0)
        {
            Console.WriteLine("The inventory is emoty");
            return;
        }

        Console.WriteLine("===Inventory===");
        Console.WriteLine("===" + items.Count + "/" + maxCapacity + "===");

        for (int i = 0; i < items.Count; i++)
        {
            int number = i + 1;
            Console.WriteLine(number + ". " + items[i].Name);
        }

        Console.WriteLine("===============");
    }

    public int GetInventoryCount()
    {
        return items.Count;
    }

    public bool isFull()
    {
        if (items.Count >= maxCapacity)
        {
            return true;
        }

        return false;
    }
    
    public bool isEmpty()
    {
        if (items.Count == 0)
        {
            return true;
        }

        return false;
    }
}