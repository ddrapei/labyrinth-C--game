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

        Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);

        // 
        if (currentRoom.Item != null)
        {
            Console.WriteLine("There is already an item in the room.");
            Console.WriteLine("Find another room to drpop your equipment.");
            return;
        }


        Item item = InventoryChecker.FindItemInInventory(player, itemIdentifier);

        if (item == null)
        {
            Console.WriteLine("Could not find item '" + itemIdentifier + "' in your inventory.");
            return;
        }

        // Equip the item based on its type
        bool equipped = EquipItem(player, item);

        // remove from inventory and display updated inventory if equipped successfully
        if (equipped)
        {
            player.Inventory.RemoveItem(item);
            player.Inventory.DisplayInventory();
        }
    }

    private bool EquipItem(Player player, Item item)
    {
        // different items to equip
        if (item is Weapon weapon)
        {
            return player.EquipWeapon(weapon);
        }
        else if (item is Shield shield)
        {
            return player.EquipShield(shield);
        }
        else if (item is IHeadArmour headArmour)
        {
            return player.EquipHeadArmour(headArmour);
        }
        else if (item is ITorsoArmour torsoArmour)
        {
            return player.EquipTorsoArmour(torsoArmour);
        }
        else if (item is ILegsArmour legsArmour)
        {
            return player.EquipLegsArmour(legsArmour);
        }
        else
        {
            Console.WriteLine("Cannot equip '" + item.Name + "' - this is not an equippable item.");
            Console.WriteLine("Try 'use <item>' for consumables like potions.");
            return false;
        }
    }
}