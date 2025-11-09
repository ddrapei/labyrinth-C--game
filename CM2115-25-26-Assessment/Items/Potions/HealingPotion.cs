namespace Items.Potions;

public class HealingPotion : Potion
{
    private int healingPower;

    public int HealingPower
    {
        get { return healingPower; }
        set { healingPower = value; }
    }

    // --- Constructor ---

    public HealingPotion (string name, int healingPower) : base(name)
    {
        this.healingPower = healingPower;
    }
}