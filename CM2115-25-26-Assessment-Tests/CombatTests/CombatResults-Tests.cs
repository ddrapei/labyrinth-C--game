using Enemies;

[Collection("Sequential")]
public class CombatResultTests
{
    [Fact]
    public void CombatResult_WhenEnemyDies_ReturnsVictory()
    {
        // arrange
        Player player = Player.GetInstance();
        CombatSystem combatSystem = CombatSystem.GetInstance();
        player.AttackPower = 10; // high enough to kill the enemy
        player.Health = 100;
        Enemy enemy = new Enemy("enemy_test", 5, 1, 0, 0.1, 10);
        combatSystem.StartCombat(enemy);

        // act
        CombatResult result = combatSystem.PlayerAttack();

        // assert
        Assert.Equal(CombatOutcome.Victory, result.CombatOutcome);
        Assert.False(combatSystem.IsInCombat);
    }

    [Fact]
    public void CombatResult_WhenEnemyIsAlive_ReturnsOngoing()
    {
        // arrange
        Player player = Player.GetInstance();
        CombatSystem combatSystem = CombatSystem.GetInstance();
        player.Health = 100;
        Enemy enemy = new Enemy("enemy_test", 10, 1, 0, 0.1, 10);
        combatSystem.StartCombat(enemy);

        // act
        CombatResult result = combatSystem.PlayerAttack();

        // assert
        Assert.Equal(CombatOutcome.Ongoing, result.CombatOutcome);
        Assert.True(combatSystem.IsInCombat);
    }

    [Fact]
    public void CombatResult_WhenPlayerDies_ReturnsDefeat()
    {
        // arrange
        Player player = Player.GetInstance();
        CombatSystem combatSystem = CombatSystem.GetInstance();
        player.Health = 1;
        player.AttackPower = 1;
        Enemy enemy = new Enemy("enemy_test", 100, 10, 0, 0.1, 10);
        combatSystem.StartCombat(enemy);

        // act
        CombatResult result = combatSystem.PlayerAttack();

        // assert
        Assert.Equal(CombatOutcome.Defeat, result.CombatOutcome);
        Assert.False(combatSystem.IsInCombat);
    }
}    