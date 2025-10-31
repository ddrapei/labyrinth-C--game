namespace Items.Armour;

// abstract factory interface
public interface ArmourFactory
{
    IHeadArmour CreateHeadArmour();
    ITorsoArmour CreateTorsoArmour();
    ILegsArmour CreateLegsArmour();
}