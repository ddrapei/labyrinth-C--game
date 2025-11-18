namespace Commands.InventoryCommands;

using Items;
using Rooms;

public class PickUpItemCommand : PlayerCommand
{
    public void Execute()
    {
        Player player = Player.GetInstance();
        Room? currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);

        if (currentRoom == null)
        {
            Console.WriteLine("You are not in a valid room.");
            return;
        }
        
        if (currentRoom.Item == null)
        {
            Console.WriteLine("There is no item in the room");
            return;
        }

        Item? item = currentRoom.Item;

        if (player.Inventory.AddItem(item))
        {
            currentRoom.Item = null; // removes item from the room
        }
    }
}