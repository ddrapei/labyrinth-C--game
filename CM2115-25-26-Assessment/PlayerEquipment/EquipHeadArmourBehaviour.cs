namespace PlayerEquipment;

using Items;
using Items.Armour;
using Rooms;
using Pastel;

public class EquipHeadArmourBehavior : IEquipBehavior
{
    public bool Equip(Player player, Item item)
    {
        if (item is not IHeadArmour headArmour)
        {
            Console.WriteLine("This item is not head armour.");
            return false;
        }

        // stores old armour or null
        IHeadArmour? oldArmour = player.HeadArmourEquipped;

        if (oldArmour != null)
        {
            Room? currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);

            if (currentRoom != null && currentRoom.Item == null)
            {   
                // places old armour inside of the room
                currentRoom.Item = (Item)oldArmour;
                player.Defense -= ((Armour)oldArmour).Defense;
                Console.WriteLine("You placed your " + ((Item)oldArmour).Name.Pastel("#ff9d00"));
            }
            else
            {
                // already an item inside of the room
                Console.WriteLine("Cannot equip " + ((Item)headArmour).Name.Pastel("#ff9d00") + " - no space to place your current armour " + ((Item)oldArmour).Name.Pastel("#ff9d00"));
                return false;
            }
        }
        else
        {
            Console.WriteLine("You equipped " + ((Item)headArmour).Name.Pastel("#ff9d00"));
        }

        player.HeadArmourEquipped = headArmour;
        player.Defense += ((Armour)headArmour).Defense;
        Console.WriteLine("Your defense now is: " + player.Defense.ToString().Pastel("#1900ff"));
        ArmourSetManager.GetInstance().CheckAndUpdateSetBonuses(player);
        return true;
    }
}