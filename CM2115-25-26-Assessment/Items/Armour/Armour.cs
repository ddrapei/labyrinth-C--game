namespace Items.Armour;

public abstract class Armour : Item
{
    private int defense;
    private string armourType;

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

    // --- Constructor ---
    public Armour(string name, int defense, string armourType) : base(name)
    {
        this.defense = defense;
        this.armourType = armourType;
    }
}