using Pastel;

// Singleton used as there is only one combat system in the game
public class CombatSystem
{
    private static CombatSystem instance = null;
    private Enemy currentEnemy;
    private bool isInCombat;
    
    public bool IsInCombat
    {
        get { return isInCombat; }
        set { isInCombat = value; }
    }

    public Enemy CurrentEnemy
    {
        get { return currentEnemy; }
    }

    private CombatSystem()
    {
        this.isInCombat = false;
    }

    public static CombatSystem GetInstance()
    {
        if (instance == null)
        {
            instance = new CombatSystem();
        }
        return instance;
    }

    public CombatResult StartCombat(Enemy enemy)
    {
        currentEnemy = enemy;
        isInCombat = true;

        Console.WriteLine("=== COMBAT STARTED ===".Pastel("#ff0000"));
        Console.WriteLine("You are fighting: " + enemy.Name.Pastel("#ff00ff"));
        enemy.DisplayEnemyInfo();
        Console.WriteLine("");
        Console.WriteLine("Commands:".Pastel("#ffff00"));
        Console.WriteLine("attack".Pastel("#ff0000") + "  Attack the enemy");
        Console.WriteLine("run".Pastel("#00ff00") + "  Try to escape to the previous room");

        return new CombatResult(CombatOutcome.Ongoing);
    }

    public CombatResult PlayerAttack()
    {
        Player player = Player.GetInstance();
        if (!isInCombat || currentEnemy == null)
        {
            return new CombatResult(CombatOutcome.Ongoing, "Not in combat");
        }

        Console.WriteLine("");
        Console.WriteLine("--- Your Turn ---".Pastel("#00ffff"));

        // Player attacks enemy
        currentEnemy.TakeDamage();

        // Check if enemy is dead
        if (currentEnemy.IsDead())
        {
            isInCombat = false;
            Console.WriteLine("");
            Console.WriteLine("=== VICTORY ===".Pastel("#00ff00"));
            Console.WriteLine("");
            return new CombatResult(CombatOutcome.Victory, "Enemy defeated!");
        }

        // Enemy's turn to attack
        Console.WriteLine("");
        Console.WriteLine("--- Enemy's Turn ---".Pastel("#ff00ff"));
        EnemyAttack();

        // Check if player is dead
        if (player.Health <= 0)
        {
            isInCombat = false;
            Console.WriteLine("");
            Console.WriteLine("=== DEFEAT ===".Pastel("#ff0000"));
            Console.WriteLine("You have been slaughtered...".Pastel("#ff0000")); 
            Console.WriteLine("");
            return new CombatResult(CombatOutcome.Defeat, "Player defeated!");
        }

        Console.WriteLine("");
        return new CombatResult(CombatOutcome.Ongoing);
    }

    private void EnemyAttack()

    {
        Player player = Player.GetInstance();
        
        // creates a double between 0.0 and 1.0
        double blockChance = new Random().NextDouble();

        // if created random double is less than player's blocking chance, the attack is blocked
        if (blockChance < player.BlockingDamageChance)
        {
            Console.WriteLine($"You blocked the {currentEnemy.Name}'s attack!".Pastel("#00ffff"));
            return;
        }

        int damage = currentEnemy.AttackPower - player.Defense;

        // prevents healing the player when defense is higher than enemy attack power
        if (damage < 0)
        {
            damage = 0;
        }

        player.Health -= damage;
        Console.WriteLine($"The {currentEnemy.Name} hits you for {damage.ToString().Pastel("#ff0000")} damage! You have {player.Health.ToString().Pastel("#7CFC00")} HP left.");
    }

    public CombatResult TryToRun()
    {
        Player player = Player.GetInstance();

        // checks if the combat has started
        if (!isInCombat)
        {
            return new CombatResult(CombatOutcome.Ongoing, "Not in combat");
        }

        Console.WriteLine("");
        Console.WriteLine("You attempt to flee!".Pastel("#ffaa00"));

        // 50% chance to escape
        // generates a random number between 0 and 1
        if (new Random().Next(0, 2) == 0)
        {
            Console.WriteLine("You successfully flee!".Pastel("#00ff00"));
            isInCombat = false;

            // Move player to previous position
            if (player.MoveToPreviousPosition())
            {
                Console.WriteLine("You fled back to (" + player.Xcoordinate.ToString().Pastel("#00ff00") + ") " + player.Ycoordinate.ToString().Pastel("#00ff00") + ")");
                RoomChecker.GetInstance().DisplayCurrentRoom(player);
            }

            Console.WriteLine("");
            return new CombatResult(CombatOutcome.Escaped, "Escaped successfully");
        }
        else
        {
            Console.WriteLine("You failed to escape!".Pastel("#ff0000"));

            // if failed to escape, enemy attacks
            Console.WriteLine("");
            Console.WriteLine("--- Enemy's Turn ---".Pastel("#ff00ff"));
            EnemyAttack();

            // Check if player is dead
            if (player.Health <= 0)
            {
                isInCombat = false;
                Console.WriteLine("");
                Console.WriteLine("=== DEFEAT ===".Pastel("#ff0000"));
                Console.WriteLine("You have been slaughtered...".Pastel("#ff0000"));
                Console.WriteLine("");
                return new CombatResult(CombatOutcome.Defeat, "Player defeated!");
            }

            Console.WriteLine("");
            return new CombatResult(CombatOutcome.Ongoing, "Failed to escape");
        }
    }

    public void EndCombat()
    {
        isInCombat = false;
        currentEnemy = null;
    }
}


// enum is used since CombatOutcome is not going to be changed and 
// it is more descriptive than array
public enum CombatOutcome
{
    Victory,
    Defeat,
    Escaped,
    Ongoing
}

public class CombatResult
{
    private CombatOutcome combatOutcome;
    private string message;

    public CombatOutcome CombatOutcome
    {
        get { return combatOutcome; }
        set { combatOutcome = value; }
    }

    public string Message
    {
        get { return message; }
        set { message = value; }
    }
    public CombatResult(CombatOutcome combatOutcome, string message = "")
    {
        this.combatOutcome = combatOutcome;
        this.message = message;
    }
}
