namespace Items.Armour;

// concrete factory for leather armour
public class LeatherArmourFactory : ArmourFactory
{
    public IHeadArmour CreateHeadArmour()
    {
        return new LeatherHelmet();
    }

    public ITorsoArmour CreateTorsoArmour()
    {
        return new LeatherChestArmour();
    }

    public ILegsArmour CreateLegsArmour()
    {
        return new LeatherLegsArmour();
    }
}