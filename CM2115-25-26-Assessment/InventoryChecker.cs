using Items;

public static class InventoryChecker
{
    public static Item FindItemInInventory(Player player, string itemIdentifier)
    {
        // two options to select the item, via its number, or via its name
        if (IsNumericInput(itemIdentifier, out int itemNumber))
        {
            return player.Inventory.GetItemByNumber(itemNumber);
        }
        else
        {
            return player.Inventory.GetItemByName(itemIdentifier);
        }
    }


    // checks what user has typed, a number or has characters in it 
    public static bool IsNumericInput(string input, out int number)
    {

        // has to be assigned first, or compiler returns an error
        number = 0;

        if (string.IsNullOrEmpty(input))
        {
            return false;
        }

        // Check if all characters are numbers
        foreach (char c in input)
        {
            if (!char.IsDigit(c))
            {
                return false;
            }
        }

        // Parse the number
        return int.TryParse(input, out number);
    }
}