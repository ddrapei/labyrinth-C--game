namespace PlayerEquipment;

using Items;
using Rooms;
using Pastel;

public class EquipWeaponBehavior : IEquipBehavior
{
    public bool Equip(Player player, Item item)
    {
        if (item is not Weapon weapon)
        {
            Console.WriteLine("This item is not a weapon.");
            return false;
        }

        if (player.WeaponEquiped != null)
        {
            Room? currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);
            if (currentRoom != null && currentRoom.Item == null)
            {   
                player.AttackPower -= player.WeaponEquiped.Damage;
                currentRoom.Item = player.WeaponEquiped;
                Console.WriteLine("You placed the item in the room " + player.WeaponEquiped.Name.Pastel("#ff9d00") + " and equipped " + weapon.Name.Pastel("#ff9d00"));
            }
            else
            {
                Console.WriteLine("Cannot equip the weapon");
                return false;
            }
        }
        else
        {
            Console.WriteLine("You equipped " + weapon.Name.Pastel("#ff9d00"));
        }

        player.WeaponEquiped = weapon;
        player.AttackPower += weapon.Damage;
        player.BaseAttackPower = weapon.Damage;
        Console.WriteLine("Your damage now is: " + player.AttackPower.ToString().Pastel("#ff0000"));
        return true;
    }
}