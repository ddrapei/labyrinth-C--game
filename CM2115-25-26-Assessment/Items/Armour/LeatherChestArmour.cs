using Items.Armour;

// cocnrete type of leather chest armour

public class LeatherChestArmour : Armour, ITorsoArmour
{
    public LeatherChestArmour() : base("Leather Chest Armour", 2, "ChestArmour")
    {
    }

    public int GetDefense()
    {
        return 2;
    }
}