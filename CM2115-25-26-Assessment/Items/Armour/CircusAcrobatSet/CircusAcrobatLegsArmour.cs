namespace Items.Armour.CircusAcrobatArmourSet;

// cocnrete type of Crude Knight's Chest Armour

public class CircusAcrobatLegsArmour : Armour, ILegsArmour
{
    public CircusAcrobatLegsArmour() : base("Harlequin`s Breeches", 0, "LegsArmour", "CircusAcrobat")
    {
    }

    public int GetDefense()
    {
        return 0;
    }
}