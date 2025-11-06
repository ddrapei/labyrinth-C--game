namespace Commands.InventoryCommands;

using Items;

public class ShowInventoryCommand : PlayerCommand
{
    public void Execute()
    {
        Player player = Player.GetInstance();
        player.Inventory.DisplayInventory();
    }
}