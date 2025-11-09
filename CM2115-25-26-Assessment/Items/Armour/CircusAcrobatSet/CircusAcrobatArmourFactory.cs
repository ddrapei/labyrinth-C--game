namespace Items.Armour.CircusAcrobatArmourSet;

// concrete factory for Crude Knight's armour
public class CircusAcrobatArmourFactory : ArmourFactory
{
    public IHeadArmour CreateHeadArmour()
    {
        return new CircusAcrobatHelmet();
    }

    public ITorsoArmour CreateTorsoArmour()
    {
        return new CircusAcrobatChestArmour();
    }

    public ILegsArmour CreateLegsArmour()
    {
        return new CircusAcrobatLegsArmour();
    }
}