namespace Items.Armour;

// abstract class for armour, that inherits from item class
public abstract class Armour : Item
{
    private int defense;
    private string armourType;
    private string setName;

    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    public string ArmourType
    {
        get { return armourType; }
        set { armourType = value; }
    }
    public string SetName
    {
        get { return setName; }
        set { setName = value; }
    }

    // --- Constructor ---
    public Armour(string name, int defense, string armourType, string setName) : base(name)
    {
        this.defense = defense;
        this.armourType = armourType;
        this.setName = setName;
    }
}