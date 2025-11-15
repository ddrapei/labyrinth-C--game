using Observers;
using Enemies;
using Rooms;
using Pastel;

// Singleton used as there is only one combat system in the game
public class CombatSystem
{
    public static CombatSystem GetInstance()
    {
        if (instance == null)
        {
            instance = new CombatSystem();
        }
        return instance;
    }

    private static CombatSystem? instance = null;
    private Enemy? currentEnemy;
    private bool isInCombat;
    private InputManager? inputManager;
    private IGameObserver? combatObserver;
    private IGameObserver? combatUnknownCommandObserver;
    private IGameObserver? gameCommandMoveObserver;
    private IGameObserver? gameHandlerObserver;
    private IGameObserver? inventoryObserver;
    private IGameObserver? unknownCommandObserver;

    public bool IsInCombat
    {
        get { return isInCombat; }
        set { isInCombat = value; }
    }

    public Enemy? CurrentEnemy
    {
        get { return currentEnemy; }
    }

    private CombatSystem()
    {
        this.isInCombat = false;
    }

    public CombatResult StartCombat(Enemy enemy)
    {
        Player player = Player.GetInstance();

        currentEnemy = enemy;
        isInCombat = true;

        // swapping observers for the combat
        SwitchToCombatMode();

        // Display combat UI
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

        Console.WriteLine("--- Your Turn ---".Pastel("#00ffff"));

        // Player attacks enemy
        currentEnemy.TakeDamage();

        // Check if enemy is dead
        if (currentEnemy.IsDead())
        {
            Console.WriteLine("");
            Console.WriteLine("=== VICTORY ===".Pastel("#00ff00"));
            Console.WriteLine("");

            // Add experience reward
            player.Experience += currentEnemy.ExperienceReward;
            Console.WriteLine($"You gained {currentEnemy.ExperienceReward.ToString().Pastel("#ffff00")} experience!");
            Console.WriteLine("");

            if (player.LevelUpBehaviors.ContainsKey("check level up"))
            {
                player.LevelUpBehaviors["check level up"].LevelUp(player);
            }
            
            EndCombat();
            return new CombatResult(CombatOutcome.Victory, "Enemy defeated!");
        }

        // Enemy's turn to attack
        Console.WriteLine("");
        Console.WriteLine("--- Enemy's Turn ---".Pastel("#ff00ff"));
        EnemyAttack();

        // Check if player is dead
        if (player.Health <= 0)
        {
            EndCombat();
            return new CombatResult(CombatOutcome.Defeat, "Player defeated!");
        }

        Console.WriteLine("");
        return new CombatResult(CombatOutcome.Ongoing);
    }

    private void EnemyAttack()
    {
        Player player = Player.GetInstance();

        if (currentEnemy == null) return;

        // creates a double between 0.0 and 1.0
        // if created random double is less than player's blocking chance, the attack is blocked
        if (new Random().NextDouble() < player.BlockingDamageChance)
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

            // Move player to previous position
            if (player.MoveToPreviousPosition())
            {
                Console.WriteLine("You fled back to (" + player.Xcoordinate.ToString().Pastel("#00ff00") + ", " + player.Ycoordinate.ToString().Pastel("#00ff00") + ")");
                RoomChecker.GetInstance().DisplayCurrentRoom(player);
            }

            EndCombat();
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
                EndCombat();
                return new CombatResult(CombatOutcome.Defeat, "Player defeated!");
            }

            Console.WriteLine("");
            return new CombatResult(CombatOutcome.Ongoing, "Failed to escape");
        }
    }

    // Switch from normal game mode to combat mode
    private void SwitchToCombatMode()
    {
        if (inputManager == null) return;

        // Remove observers from the main game
        if (gameCommandMoveObserver != null)
            inputManager.RemoveObserver(gameCommandMoveObserver);
        if (gameHandlerObserver != null)
            inputManager.RemoveObserver(gameHandlerObserver);
        if (inventoryObserver != null)
            inputManager.RemoveObserver(inventoryObserver);
        if (unknownCommandObserver != null)
            inputManager.RemoveObserver(unknownCommandObserver);

        // Add combat observers 
        if (combatObserver != null)
            inputManager.AddObserver(combatObserver);
        if (combatUnknownCommandObserver != null)
            inputManager.AddObserver(combatUnknownCommandObserver);
    }

    // Switch from combat mode back to normal game mode
    private void SwitchToNormalMode()
    {
        if (inputManager == null) return;

        // Remove combat observers
        if (combatObserver != null)
            inputManager.RemoveObserver(combatObserver);
        if (combatUnknownCommandObserver != null)
            inputManager.RemoveObserver(combatUnknownCommandObserver);

        // Add back main game observers
        if (gameCommandMoveObserver != null)
            inputManager.AddObserver(gameCommandMoveObserver);
        if (gameHandlerObserver != null)
            inputManager.AddObserver(gameHandlerObserver);
        if (inventoryObserver != null)
            inputManager.AddObserver(inventoryObserver);
        if (unknownCommandObserver != null)
            inputManager.AddObserver(unknownCommandObserver);
    }

    public void EndCombat()
    {
        isInCombat = false;

        // Remove enemy from room if defeated
        if (currentEnemy != null && currentEnemy.IsDead())
        {
            Player player = Player.GetInstance();
            Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);
            if (currentRoom != null && currentRoom.Enemy == currentEnemy)
            {
                currentRoom.Enemy = null;
            }
        }

        currentEnemy = null;

        // Switch back to normal mode
        SwitchToNormalMode();
    }

    public void Initialize(InputManager inputManager, IGameObserver combatObserver, IGameObserver combatUnknownCommandObserver, IGameObserver gameCommandMoveObserver, IGameObserver gameHandlerObserver, IGameObserver inventoryObserver, IGameObserver unknownCommandObserver)
    {
        this.inputManager = inputManager;
        this.combatObserver = combatObserver;
        this.combatUnknownCommandObserver = combatUnknownCommandObserver;
        this.gameCommandMoveObserver = gameCommandMoveObserver;
        this.gameHandlerObserver = gameHandlerObserver;
        this.inventoryObserver = inventoryObserver;
        this.unknownCommandObserver = unknownCommandObserver;
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