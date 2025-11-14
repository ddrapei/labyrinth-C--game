using Commands;
using PlayerEquipment;
using Items;
using Items.Armour;
using Pastel;

namespace Commands.InventoryCommands;

// Concrete command to EquipHeadArmour
public class EquipHeadArmourCommand : PlayerCommand
{
    private string itemIdentifier;

    public EquipHeadArmourCommand(string itemIdentifier)
    {
        this.itemIdentifier = itemIdentifier;
    }

    public void Execute()
    {
        Player player = Player.GetInstance();

        if (string.IsNullOrEmpty(itemIdentifier))
        {
            Console.WriteLine("Equip head <item number or name>");
            Console.WriteLine("Example: equip head 1");
            return;
        }

        // Find the item in inventory
        Item? item = InventoryChecker.FindItemInInventory(player, itemIdentifier);

        if (item == null)
        {
            Console.WriteLine("Could not find item '" + itemIdentifier + "' in your inventory.");
            return;
        }

        // Check if the item is head armour
        if (item is not IHeadArmour headArmour)
        {
            Console.WriteLine(item.Name.Pastel("#ba6d00") + " is not head armour.");
            return;
        }
        
        // IMPORTANT:
        // Registering logic with the command
        if (player.EquipmentBehaviors.TryGetValue("head", out var behavior))
        {
            if (behavior.Equip(player, item))
            {
                player.Inventory.RemoveItem(item);
                player.Inventory.DisplayInventory();
            }
        }
        else
        {
            Console.WriteLine("Cannot equip " + item.Name.Pastel("#ba6d00") + " - no behavior found for head armour.");
        }
    }
}