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



public class ShowInventoryCommand : PlayerCommand
{
    public void Execute()
    {
        Player player = Player.GetInstance();
        player.Inventory.DisplayInventory();
    }
}