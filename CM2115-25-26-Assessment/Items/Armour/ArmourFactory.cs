namespace Items.Armour;
public interface ArmourFactory
{
    IHeadArmour CreateHeadArmour();
    ITorsoArmour CreateTorsoArmour();
    ILegsArmour CreateLegsArmour();
}