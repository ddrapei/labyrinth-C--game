using Items.Armour;

// cocnrete type of leather head armour

public class LeatherHelmet : Armour, IHeadArmour
{
    public LeatherHelmet() : base("Leather Helmet", 1, "HeadArmour", "Leather")
    {
    }

    public int GetDefense()
    {
        return 1;
    }
}