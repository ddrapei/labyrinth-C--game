namespace PlayerEquipment;

using Items;
using Items.Potions;
using Pastel;

public class UseHealingPotionBehavior : IEquipBehavior
{
    public bool Equip(Player player, Item item)
    {
        if (item is not HealingPotion healingPotion)
        {
            Console.WriteLine("This item is not a healing potion.");
            return false;
        }

        player.Health = player.Health + healingPotion.HealingPower;
        
        if (player.Health <= 0)
        {
            Console.WriteLine("");
            Console.WriteLine("You have used " + healingPotion.Name.Pastel("#7CFC00") + " and it was a " + "DEADLY POISON".Pastel("#fc0303") + "!");
            Console.WriteLine("Your heart stopped beating");
            Console.WriteLine("");
        }
        else
        {
            Console.WriteLine("");
            Console.WriteLine("You have used " + healingPotion.Name.Pastel("#7CFC00") + ", your health is increased by " + healingPotion.HealingPower.ToString().Pastel("#7CFC00"));
            Console.WriteLine("Your health is " + player.Health.ToString().Pastel("#7CFC00"));
            Console.WriteLine("");
        }
        return true;
    }
}