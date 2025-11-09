namespace Items.Armour.CircusAcrobatArmourSet;

// cocnrete type of CircusAcrobat Head Armour

public class CircusAcrobatHelmet : Armour, IHeadArmour
{
    public CircusAcrobatHelmet() : base("Harlequin`s Cap", 0, "HeadArmour", "CircusAcrobat")
    {
    }

    public int GetDefense()
    {
        return 0;
    }
}