namespace Commands;

using Items;

public class PickUpItemCommand : PlayerCommand
{
    public void Execute()
    {
        Player player = Player.GetInstance();
        Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);

        if (currentRoom == null)
        {
            Console.WriteLine("You fell out of the map");
            return;
        }

        if (currentRoom.Item == null)
        {
            Console.WriteLine("There is no item in the room");
        }

        Item item = currentRoom.Item;

        if (player.Inventory.AddItem(item))
        {
            currentRoom.Item = null; // removes item from the room
        }
    }
}

public class DropItemCommand : PlayerCommand
{
    private string itemIdentifier;

    public DropItemCommand(string itemIdentifier)
    {
        this.itemIdentifier = itemIdentifier;
    }

    public void Execute()
    {
        Player player = Player.GetInstance();

        if (string.IsNullOrEmpty(itemIdentifier))
        {
            Console.WriteLine("Provide the number of the item to drop");
            return;
        }

        Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);

        if (currentRoom == null)
        {
            Console.WriteLine("You fell out of the map");
            return;
        }
        
        // currently there can be only one item in the room
        if (currentRoom.Item != null)
        {
            Console.WriteLine("There is already an item in the room.");
            Console.WriteLine("Find another item to drop your item.");
            return;
        }

        Item item = null;


        // returns true if input is a number
        bool isNumeric = true;
        foreach (char c in itemIdentifier)
        {
            if (!char.IsDigit(c))
            {
                isNumeric = false;
                break;
            }
        }

        // checks input for the number first
        if (isNumeric && itemIdentifier.Length > 0)
        {
            int itemNumber = int.Parse(itemIdentifier);
            item = player.Inventory.GetItemByNumber(itemNumber);

            if (item == null)
            {
                Console.WriteLine("You don't have the item with that number");
                return;
            }
        }
        else
        {
            // checks input for the item name after
            item = player.Inventory.GetItemByName(itemIdentifier);

            if (item == null)
            {
                Console.WriteLine("You don't have " + itemIdentifier + " in your invenotory");
                return;
            }
        }

        if (player.Inventory.DropItem(item))
        {
            currentRoom.Item = item; // puts item in the room
            Console.WriteLine("You dropped " + item.Name + " in the room.");
        }
    }
}

public class ShowInventoryCommand : PlayerCommand
{
    public void Execute()
    {
        Player player = Player.GetInstance();
        player.Inventory.DisplayInventory();
    }
}