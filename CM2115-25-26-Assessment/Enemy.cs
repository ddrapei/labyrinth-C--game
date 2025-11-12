using Pastel;

public class Enemy : IPerceptible
{
    private string name;
    private int health;
    private int attackPower;
    private int defense;
    private double perception;
    private int expReward;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    public int AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }
    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }
    public double Perception
    {
        get { return perception; }
        set { perception = value; }
    }
    public int ExpReward
    {
        get { return expReward; }
        set { expReward = value; }
    }

    public Enemy(string name, int health, int attackPower, int defense, double perception, int expReward)
    {
        this.name = name;
        this.health = health;
        this.attackPower = attackPower;
        this.defense = defense;
        this.perception = perception;
        this.expReward = expReward;
    }

    public void TakeDamage()
    {
        Player player = Player.GetInstance();

        int damage = player.AttackPower - this.Defense;

        // if the defense of the enemy is higher than the player's attack power, the damage ithat is taken is 0
        // it prevents healing the enemy when attacked

        if (damage < 0)
        {
            damage = 0;
        }

        this.Health -= damage;
        Console.WriteLine($"You hit the {this.Name} for {damage} damage. It has {this.Health} HP left.");

        if (this.IsDead())
        {
            Console.WriteLine($"{this.Name} has been defeated!");
            player.Experience += this.ExpReward;
            Console.WriteLine($"You gained {this.ExpReward} experience!");
        }
    }

    public void NoticePlayer()
    {
        Console.WriteLine($"{this.Name} has noticed you!");
    }

    public void AttackPlayer()
    {
        Player player = Player.GetInstance();

        int damage = this.AttackPower - player.Defense;
        if (damage < 0)
        {
            damage = 0;
        }

        player.Health -= damage;
        Console.WriteLine($"{this.Name} deals {damage.ToString().Pastel("#990000")} damage! You have {player.Health.ToString().Pastel("#126b00")} HP left.");
    }

    public double DicePerception()
    {
        return new Random().NextDouble();
    }

    public bool IsDead()
    {
        return this.Health <= 0;
    }

    public void DisplayEnemyInfo()
    {
        Console.WriteLine("Enemy: " + this.Name);
        Console.WriteLine("Health: " + this.Health);
        Console.WriteLine("Attack Power " + this.AttackPower);
        Console.WriteLine("Defense: " + this.Defense);
    }
}