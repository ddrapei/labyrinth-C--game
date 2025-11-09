namespace PlayerDisplay;

using Items;
using Items.Armour;

using Pastel;


// method to display equipped items of the player
public class PlayerEquippedDisplay : IPlayerDisplay
{
    public void Display()
    {
        Player player = Player.GetInstance();

        Console.WriteLine();
        Console.WriteLine("=== Equipped ===".Pastel("#00ff00"));
        Console.WriteLine("Weapon: " + (player.WeaponEquiped?.Name ?? "None").Pastel("#fc030f"));
        Console.WriteLine("Shield: : " + (player.ShieldEquipped?.Name ?? "None").Pastel("#1900ff"));
        Console.WriteLine("Head Armour: " + (((Item)player.HeadArmourEquipped)?.Name ?? "None").Pastel("#693a01"));
        Console.WriteLine("Torso Armour: " + (((Item)player.TorsoArmourEquipped)?.Name ?? "None").Pastel("#693a01"));
        Console.WriteLine("Legs Armour: " + (((Item)player.LegsArmourEquipped)?.Name ?? "None").Pastel("#693a01"));
    }
}