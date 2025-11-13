namespace Enemies;
using Pastel;

public class Enemy : IPerceptible
{
    private string name;
    private int health;
    private int attackPower;
    private int defense;
    private double perceptionChance;
    private bool hasNoticedPlayer;
    private int experienceReward;

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

    public double PerceptionChance
    {
        get { return perceptionChance; }
        set { perceptionChance = value; }
    }

    public int ExperienceReward
    {
        get { return experienceReward; }
        set { experienceReward = value; }
    }

    public bool HasNoticedPlayer
    {
        get { return hasNoticedPlayer; }
        set { hasNoticedPlayer = value; }
    }

    // --- Constructor ---
    public Enemy(string name, int health, int attackPower, int defense, double perceptionChance, int experienceReward)
    {
        this.name = name;
        this.health = health;
        this.attackPower = attackPower;
        this.defense = defense;
        this.perceptionChance = perceptionChance;
        this.experienceReward = experienceReward;
        this.hasNoticedPlayer = false;
    }

    // method to check if enemy is dead
    public bool IsDead()
    {
        return health <= 0;
    }

    // method for enemy to take damage from player
    public void TakeDamage()
    {
        Player player = Player.GetInstance();
        int damage = player.AttackPower - this.defense;

        // prevents healing the enemy when player attack is lower than enemy defense
        if (damage < 0)
        {
            damage = 0;
        }

        this.health -= damage;

        // prevents enemie's health to go below 0
        if (this.health < 0)
        {
            this.health = 0;
        }

        Console.WriteLine($"You hit {this.name} for {damage.ToString().Pastel("#ff0000")} damage! Enemy has {this.health.ToString().Pastel("#ff9d00")} HP");
    }

    public void DisplayEnemyInfo()
    {
        Console.WriteLine("Enemy: " + this.name.Pastel("#ff00ff"));
        Console.WriteLine("Health: " + this.health.ToString().Pastel("#ff9d00"));
        Console.WriteLine("Attack: " + this.attackPower.ToString().Pastel("#ff0000"));
        Console.WriteLine("Defense: " + this.defense.ToString().Pastel("#1900ff"));
    }


    // Perception and Player Notice implementation
    // If the enemy notices the player the combat starts automatically


    // Dice roll for perception - returns a value between 0.0 and 1.0
    public double DicePerception()
    {
        return new Random().NextDouble();
    }

    // Check if enemy notices the player based on perception chance
    public bool NoticePlayer()
    {
        // If enemy already noticed player, return true
        if (hasNoticedPlayer)
        {
            return true;
        }

        if (DicePerception() < perceptionChance)
        {
            hasNoticedPlayer = true;
            Console.WriteLine("");
            Console.WriteLine($"{this.name.Pastel("#ff00ff")} has noticed you!".Pastel("#ff9d00"));
            Console.WriteLine("");
            return true;
        }

        return false;
    }

    // Start attacking the player - triggers combat
    public void StartAttackingPlayer()
    {
        if (hasNoticedPlayer)
        {
            CombatSystem combatSystem = CombatSystem.GetInstance();
            combatSystem.StartCombat(this);
        }
    }
}