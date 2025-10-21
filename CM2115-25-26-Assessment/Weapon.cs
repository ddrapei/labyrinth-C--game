// added weapon class that inherits from abstract class item
public class Weapon : Item
{
    private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public Weapon(string name, int damage) : base(name)
    {
        this.damage = damage;
    }
}