namespace Items.Armour.CrudeKnightsArmourSet;

// cocnrete type of Crude Knight's Chest Armour

public class CrudeKnightsChestArmour : Armour, ITorsoArmour
{
    public CrudeKnightsChestArmour() : base("Crude Knight's Chest Armour", 10, "ChestArmour", "CrudeKnights")
    {
    }

    public int GetDefense()
    {
        return 10;
    }
}