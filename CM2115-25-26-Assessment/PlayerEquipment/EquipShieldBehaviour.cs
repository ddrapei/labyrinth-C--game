namespace PlayerEquipment;

using Items;
using Rooms;
using Pastel;

public class EquipShieldBehavior : IEquipBehavior
{
    public bool Equip(Player player, Item item)
    {
        if (item is not Shield shield)
        {
            Console.WriteLine("This item is not a shield.");
            return false;
        }

        if (player.ShieldEquipped != null)
        {
            Room? currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);
            if (currentRoom != null && currentRoom.Item == null)
            {
                currentRoom.Item = player.ShieldEquipped;
                Console.WriteLine("You placed the item in the room " + player.ShieldEquipped.Name.Pastel("#00e5ff") + " and equipped " + shield.Name.Pastel("#00e5ff"));
            }
            else
            {
                Console.WriteLine("Cannot equip the shield");
                return false;
            }
        }
        else
        {
            Console.WriteLine("You equipped " + shield.Name.Pastel("#00e5ff"));
        }

        player.ShieldEquipped = shield;
        player.BlockingDamageChance = shield.BlockingDamageChance;
        Console.WriteLine("Your blocking change now is: " + player.BlockingDamageChance.ToString().Pastel("#00e5ff"));
        return true;
    }
}