using Items.Armour;

// cocnrete type of leather chest armour

public class LeatherLegsArmour : Armour, ILegsArmour
{
    public LeatherLegsArmour() : base("Leather Pants", 1, "HeadArmour", "Leather")
    {
    }

    public int GetDefense()
    {
        return 1;
    }
}