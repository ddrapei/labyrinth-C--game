namespace Commands.InventoryCommands;

using Items;
using Items.Potions;

using Pastel;

public class UsePotionCommand : PlayerCommand
{
    private string itemIdentifier;

    public UsePotionCommand(string itemIdentifier)
    {
        this.itemIdentifier = itemIdentifier;
    }

    public void Execute()
    {
        Player player = Player.GetInstance();

        if (string.IsNullOrEmpty(itemIdentifier))
        {
            Console.WriteLine("Use <item number or name>");
            Console.WriteLine("Example: use 1, use name of the item");
            return;
        }

        // find the item
        Item item = InventoryChecker.FindItemInInventory(player, itemIdentifier);

        if (item == null)
        {
            Console.WriteLine("Cam not find the item " + itemIdentifier.Pastel("#ba0013") + " in your inventory.");
            return;
        }

        // check if the item is a potion
        if (item is not Potion)
        {
            Console.WriteLine(item.Name.Pastel("#ba6d00") + " cannot be used");
            Console.WriteLine("Try " + "'equip <item>'".Pastel("#ba6d00") + " for equipment.");
            return;
        }

        // Use potion
        UsePotion(player, (Potion)item);
    }

    private void UsePotion(Player player, Potion potion)
    {
        // Handle different potion types
        if (potion is HealingPotion healingPotion)
        {
            player.UseHealingPotion(healingPotion);
        }

        // for more potions in the future
        // else if (potion is RagePotion ragePotion)
        // {
        //     wasUsed = player.UseRagePotion(ragePotion);
        // }
        else
        {
            Console.WriteLine("Cannot use " + potion.Name.Pastel("#ba6d00") + " - this potion type is not yet implemented.");
            return;
        }

        // Remove the potion from inventory after successful use
            player.Inventory.RemoveItem(potion);
            player.Inventory.DisplayInventory();
    }
}