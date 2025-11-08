namespace Items;

public class Shield : Item
{
    private double blockingDamageChance;

    public double BlockingDamageChance
    {
        get { return blockingDamageChance; }
        set { blockingDamageChance = value; }
    }

    public Shield (string name, double blockingDamageChance) : base(name)
    {
        this.blockingDamageChance = blockingDamageChance;
    }
}