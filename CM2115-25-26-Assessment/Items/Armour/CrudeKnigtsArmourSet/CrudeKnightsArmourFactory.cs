namespace Items.Armour.CrudeKnightsArmourSet;

// concrete factory for Crude Knight's armour
public class CrudeKnightsArmourFactory : ArmourFactory
{
    public IHeadArmour CreateHeadArmour()
    {
        return new CrudeKnightsHelmet();
    }

    public ITorsoArmour CreateTorsoArmour()
    {
        return new CrudeKnightsChestArmour();
    }

    public ILegsArmour CreateLegsArmour()
    {
        return new CrudeKnightsLegsArmour();
    }
}