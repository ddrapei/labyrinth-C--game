namespace Items.Armour.CircusAcrobatArmourSet;

// cocnrete type of Crude Knight's Chest Armour

public class CircusAcrobatChestArmour : Armour, ITorsoArmour
{
    public CircusAcrobatChestArmour() : base("Harlequin`s Doublet", 0, "ChestArmour", "CircusAcrobat")
    {
    }

    public int GetDefense()
    {
        return 0;
    }
}