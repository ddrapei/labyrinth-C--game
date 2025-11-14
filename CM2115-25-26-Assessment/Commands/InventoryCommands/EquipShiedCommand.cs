using Commands;
using PlayerEquipment;
using Items;
using Pastel;

namespace Commands.InventoryCommands;

// Concrete command to EquipShield
public class EquipShieldCommand : PlayerCommand
{
    private string itemIdentifier;

    public EquipShieldCommand(string itemIdentifier)
    {
        this.itemIdentifier = itemIdentifier;
    }

    public void Execute()
    {
        Player player = Player.GetInstance();

        if (string.IsNullOrEmpty(itemIdentifier))
        {
            Console.WriteLine("Equip shield <item number or name>");
            Console.WriteLine("Example: equip shield 1");
            return;
        }

        // Find the item in inventory
        Item? item = InventoryChecker.FindItemInInventory(player, itemIdentifier);

        if (item == null)
        {
            Console.WriteLine("Could not find item '" + itemIdentifier + "' in your inventory.");
            return;
        }

        // Check if the item is a shield
        if (item is not Shield shield)
        {
            Console.WriteLine(item.Name.Pastel("#ba6d00") + " is not a shield.");
            return;
        }
        
        // IMPORTANT:
        // Registering logic with the command
        if (player.EquipmentBehaviors.TryGetValue("shield", out var behavior))
        {
            if (behavior.Equip(player, item))
            {
                player.Inventory.RemoveItem(item);
                player.Inventory.DisplayInventory();
            }
        }
        else
        {
            Console.WriteLine("Cannot equip " + item.Name.Pastel("#ba6d00") + " - no behavior found for shields.");
        }
    }
}