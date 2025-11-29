namespace PlayerEquipment;

using Items;
using Items.Potions;
using Pastel;
using System.Threading;

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
        

        // 2 scenarios: deadly potion and healing potion
        if (player.Health <= 0)
        {   

            // nice animation when player has used deadly potion
            for (int i = 0; i <= 3; i++)
            {
                if (i == 0)
                {
                    Console.Write("\r.".Pastel("#fc0303"));
                    
                }
                else if (i == 1)
                {
                    Console.Write("\r..".Pastel("#fc0303"));
                }
                else
                {
                    Console.Write("\r...".Pastel("#fc0303"));
                }
                Thread.Sleep(1000);
            }

            Thread.Sleep(3000);
            Console.WriteLine("");
            Console.WriteLine("You have used " + healingPotion.Name.Pastel("#7CFC00") + " and it was a " + "DEADLY POISON".Pastel("#fc0303") + "!");
            Console.WriteLine("Your heart stopped beating".Pastel("#fc0303"));
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