namespace PlayerEquipment;

using Items;
using Items.Armour;
using Rooms;
using Pastel;

public class EquipTorsoArmourBehavior : IEquipBehavior
{
    public bool Equip(Player player, Item item)
    {
        if (item is not ITorsoArmour torsoArmour)
        {
            Console.WriteLine("This item is not torso armour.");
            return false;
        }

        ITorsoArmour? oldArmour = player.TorsoArmourEquipped;

        if (oldArmour != null)
        {
            Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);
            if (currentRoom != null && currentRoom.Item == null)
            {
                currentRoom.Item = (Item)oldArmour;
                player.Defense -= ((Armour)oldArmour).Defense;
                Console.WriteLine("You placed your " + ((Item)oldArmour).Name.Pastel("#ff9d00"));
            }
            else
            {
                Console.WriteLine("Cannot equip " + ((Item)torsoArmour).Name.Pastel("#ff9d00") + " - no space to place your current armour " + ((Item)oldArmour).Name.Pastel("#ff9d00"));
                return false;
            }
        }
        else
        {
            Console.WriteLine("You equipped " + ((Item)torsoArmour).Name.Pastel("#ff9d00"));
        }

        player.TorsoArmourEquipped = torsoArmour;
        player.Defense += ((Armour)torsoArmour).Defense;
        Console.WriteLine("Your defense now is: " + player.Defense.ToString().Pastel("#1900ff"));
        ArmourSetManager.GetInstance().CheckAndUpdateSetBonuses(player);
        return true;
    }
}