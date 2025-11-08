namespace Items.Armour.CrudeKnightsArmourSet;

// cocnrete type of Crude Knight's Chest Armour

public class CrudeKnightsHelmet : Armour, IHeadArmour
{
    public CrudeKnightsHelmet() : base("Crude Knight's Helmet", 5, "HeadArmour", "CrudeKnights")
    {
    }

    public int GetDefense()
    {
        return 5;
    }
}