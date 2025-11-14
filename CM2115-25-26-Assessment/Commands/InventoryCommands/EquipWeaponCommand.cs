using Commands;
using PlayerEquipment;
using Items;
using Pastel;

namespace Commands.InventoryCommands;

// Concrete command to EquipWeapon
public class EquipWeaponCommand : PlayerCommand
{
    private string itemIdentifier;

    public EquipWeaponCommand(string itemIdentifier)
    {
        this.itemIdentifier = itemIdentifier;
    }

    public void Execute()
    {
        Player player = Player.GetInstance();

        if (string.IsNullOrEmpty(itemIdentifier))
        {
            Console.WriteLine("Equip weapon <item number or name>");
            Console.WriteLine("Example: equip weapon 1");
            return;
        }

        // Find the item in inventory
        Item? item = InventoryChecker.FindItemInInventory(player, itemIdentifier);

        if (item == null)
        {
            Console.WriteLine("Could not find item '" + itemIdentifier + "' in your inventory.");
            return;
        }

        // Check if the item is a weapon
        if (item is not Weapon weapon)
        {
            Console.WriteLine(item.Name.Pastel("#ba6d00") + " is not a weapon.");
            return;
        }
        
        // IMPORTANT:
        // Registering logic with the command
        if (player.EquipmentBehaviors.TryGetValue("weapon", out var behavior))
        {
            if (behavior.Equip(player, item))
            {
                player.Inventory.RemoveItem(item);
                player.Inventory.DisplayInventory();
            }
        }
        else
        {
            Console.WriteLine("Cannot equip " + item.Name.Pastel("#ba6d00") + " - no behavior found for weapons.");
        }
    }
}