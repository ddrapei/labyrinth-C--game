namespace Commands.InventoryCommands;

using Items;
using Rooms;

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
            Console.WriteLine("Enter a command to use your inventory");
            Console.WriteLine("drop <item number>, equip <item number>");
            return;
        }

        Room? currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);

        if (currentRoom == null)
        {
            Console.WriteLine("You are not in a valid room.");
            return;
        }

        // currently there can be only one item in the room
        if (currentRoom.Item != null)
        {
            Console.WriteLine("There is already an item in the room.");
            Console.WriteLine("Find another item to drop your item.");
            return;
        }

        Item? item = InventoryChecker.FindItemInInventory(player, itemIdentifier);

        if (item == null)
        {
            Console.WriteLine("Item not found in inventory.");
            return;
        }

        if (player.Inventory.DropItem(item))
        {
            currentRoom.Item = item; // puts item in the room
            Console.WriteLine("You dropped " + item.Name + " in the room.");
        }
    }
}