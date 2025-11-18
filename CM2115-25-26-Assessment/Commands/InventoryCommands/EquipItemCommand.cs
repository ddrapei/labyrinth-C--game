namespace Commands.InventoryCommands;

using Items;
using Items.Armour;
using Rooms;

public class EquipItemCommand : PlayerCommand
{
    private string itemIdentifier;

    public EquipItemCommand(string itemIdentifier)
    {
        this.itemIdentifier = itemIdentifier;
    }

    public void Execute()
    {
        Player player = Player.GetInstance();

        if (string.IsNullOrEmpty(itemIdentifier))
        {
            Console.WriteLine("Use, equip or drop <item number or name>");
            Console.WriteLine("Example: equip 1, equip item with index 1");
            return;
        }

        Room? currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);

        if (currentRoom == null)
        {
            Console.WriteLine("You are not in a valid room.");
            return;
        }
        // Check if there's already an item in the room (needed for swapping equipment)
        if (currentRoom.Item != null)
        {
            Console.WriteLine("There is already an item in the room.");
            Console.WriteLine("Find another room to drop your equipment.");
            return;
        }

        Item? item = InventoryChecker.FindItemInInventory(player, itemIdentifier);

        if (item == null)
        {
            Console.WriteLine("Could not find item '" + itemIdentifier + "' in your inventory.");
            return;
        }

        // equips based on what is the item
        EquipItem(item);
    }


    // equips based on what is the item
    private void EquipItem(Item item)
    {
        PlayerCommand? specificCommand = null;

        if (item is Weapon)
        {
            specificCommand = new EquipWeaponCommand(itemIdentifier);
        }
        else if (item is Shield)
        {
            specificCommand = new EquipShieldCommand(itemIdentifier);
        }
        else if (item is IHeadArmour)
        {
            specificCommand = new EquipHeadArmourCommand(itemIdentifier);
        }
        else if (item is ITorsoArmour)
        {
            specificCommand = new EquipTorsoArmourCommand(itemIdentifier);
        }
        else if (item is ILegsArmour)
        {
            specificCommand = new EquipLegsArmourCommand(itemIdentifier);
        }
        else
        {
            Console.WriteLine("Cannot equip '" + item.Name + "' - this is not an equippable item.");
            Console.WriteLine("Try 'use <item>' for consumables like potions.");
            return;
        }

        // Execute the specific command
        specificCommand?.Execute();
    }
}