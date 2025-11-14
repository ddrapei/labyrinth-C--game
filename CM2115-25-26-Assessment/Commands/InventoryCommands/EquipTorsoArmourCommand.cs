using Commands;
using PlayerEquipment;
using Items;
using Items.Armour;
using Pastel;

namespace Commands.InventoryCommands;

// Concrete command to EquipTorsoArmour
public class EquipTorsoArmourCommand : PlayerCommand
{
    private string itemIdentifier;

    public EquipTorsoArmourCommand(string itemIdentifier)
    {
        this.itemIdentifier = itemIdentifier;
    }

    public void Execute()
    {
        Player player = Player.GetInstance();

        if (string.IsNullOrEmpty(itemIdentifier))
        {
            Console.WriteLine("Equip torso <item number or name>");
            Console.WriteLine("Example: equip torso 1");
            return;
        }

        // Find the item in inventory
        Item? item = InventoryChecker.FindItemInInventory(player, itemIdentifier);

        if (item == null)
        {
            Console.WriteLine("Could not find item '" + itemIdentifier + "' in your inventory.");
            return;
        }

        // Check if the item is torso armour
        if (item is not ITorsoArmour torsoArmour)
        {
            Console.WriteLine(item.Name.Pastel("#ba6d00") + " is not torso armour.");
            return;
        }
        
        // IMPORTANT:
        // Registering logic with the command
        if (player.EquipmentBehaviors.TryGetValue("torso", out var behavior))
        {
            if (behavior.Equip(player, item))
            {
                player.Inventory.RemoveItem(item);
                player.Inventory.DisplayInventory();
            }
        }
        else
        {
            Console.WriteLine("Cannot equip " + item.Name.Pastel("#ba6d00") + " - no behavior found for torso armour.");
        }
    }
}