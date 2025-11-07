namespace Commands.InventoryCommands;

using Items;
using Items.Armour;

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
            Console.WriteLine("Enter a command to use your inventory");
            Console.WriteLine("drop <item number>, equip <item number>");
            return;
        }

        Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);

        // currently there can be only one item in the room
        if (currentRoom.Item != null)
        {
            Console.WriteLine("There is already an item in the room.");
            Console.WriteLine("Find another item to drop your item.");
            return;
        }

        Item item = null;


        // returns true if input is a number
        bool isNumeric = true;
        foreach (char c in itemIdentifier)
        {
            if (!char.IsDigit(c))
            {
                isNumeric = false;
                break;
            }
        }

        if (isNumeric)
        {
            if (int.TryParse(itemIdentifier, out int itemNumber))
            {
                item = player.Inventory.GetItemByNumber(itemNumber);
            }
        }
        else
        {
            item = player.Inventory.GetItemByName(itemIdentifier);
        }

        if (item == null)
        {
            Console.WriteLine("Could not find item " + itemIdentifier + " in your inventory.");
            return;
        }

        // Equipping different items based on the type
        if (item is Weapon weapon)
        {
            player.EquipWeapon(weapon);
            player.Inventory.DropItem(item);
        }
        else if (item is IHeadArmour headArmour)
        {
            //
        }
        else if (item is ITorsoArmour torsoArmour)
        {
            //
        }
        else if (item is ILegsArmour legsArmour)
        {
            //
        }
        else
        {
            Console.WriteLine("Can not equip the item");
        }
    }
}