using Items.Armour;

public class LeatherHelmet : Armour, IHeadArmour
{
    public LeatherHelmet() : base("Leather Helmet", 1, "HeadArmour")
    {
    }

    public int GetDefense()
    {
        return 1;
    }
}