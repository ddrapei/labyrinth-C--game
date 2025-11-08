namespace Items.Armour.CrudeKnightsArmourSet;

// cocnrete type of Crude Knight's Chest Armour

public class CrudeKnightsLegsArmour : Armour, ILegsArmour
{
    public CrudeKnightsLegsArmour() : base("Crude Knight's Legs Armour", 5, "LegsArmour", "CrudeKnights")
    {
    }

    public int GetDefense()
    {
        return 5;
    }
}