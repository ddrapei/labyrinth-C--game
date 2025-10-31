using Items.Armour;

public class LeatherLegsArmour : Armour, ILegsArmour
{
    public LeatherLegsArmour() : base("Leather Pants", 1, "HeadArmour")
    {
    }

    public int GetDefense()
    {
        return 1;
    }
}