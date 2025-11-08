namespace Items.Armour.LeatherArmourSet;

// cocnrete type of leather chest armour

public class LeatherChestArmour : Armour, ITorsoArmour
{
    public LeatherChestArmour() : base("Leather Chest Armour", 2, "ChestArmour", "Leather")
    {
    }

    public int GetDefense()
    {
        return 2;
    }
}