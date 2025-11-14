namespace Commands.InventoryCommands;

using Items;
using Items.Potions;
using Pastel;

public class UsePotionCommand : PlayerCommand
{
    private string itemIdentifier;

    public UsePotionCommand(string itemIdentifier)
    {
        this.itemIdentifier = itemIdentifier;
    }

    public void Execute()
    {
        Player player = Player.GetInstance();

        if (string.IsNullOrEmpty(itemIdentifier))
        {
            Console.WriteLine("Use <item number or name>");
            Console.WriteLine("Example: use 1, use name of the item");
            return;
        }

        // find the item
        Item? item = InventoryChecker.FindItemInInventory(player, itemIdentifier);

        if (item == null)
        {
            Console.WriteLine("Cam not find the item " + itemIdentifier.Pastel("#ba0013") + " in your inventory.");
            return;
        }

        // check if the item is a potion
        if (item is not Potion)
        {
            Console.WriteLine(item.Name.Pastel("#ba6d00") + " cannot be used");
            Console.WriteLine("Try " + "'equip <item>'".Pastel("#ba6d00") + " for equipment.");
            return;
        }

        // IMPORTANT:
        // Registering logic with the command
        if (player.EquipmentBehaviors.TryGetValue("potion", out var behavior))
        {
            if (behavior.Equip(player, item))
            {
                player.Inventory.RemoveItem(item);
                player.Inventory.DisplayInventory();
            }
        }
        else
        {
            Console.WriteLine("Cannot use " + item.Name.Pastel("#ba6d00"));
        }
    }
}