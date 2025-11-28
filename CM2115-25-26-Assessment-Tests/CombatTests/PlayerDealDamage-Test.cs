using Enemies;
using Combat;



public class PlayerDealDamageTest
{
    [Theory]
    [InlineData(1, 10, 0, 9)]
    [InlineData(2, 20, 0, 18)]
    [InlineData(0, 20, 0, 20)]
    [InlineData(15, 20, 0, 5)]
    [InlineData(20, 20, 0, 0)]
    [InlineData(20, 20, 5, 5)]
    [InlineData(20, 20, 1, 1)]
    [InlineData(20, 2, 20, 2)]
    [InlineData(20, 2, 25, 2)]
    [InlineData(20, 2, 18, 0)]
    [InlineData(100, 2, 18, 0)]
    public void PlayerDealDamage_ReturnsSubtractedEnemyHealth(int playerAttackPower, int enemyInitialHealth, int enemyDefense, int expectedEnemyHealthAfterAttack)
    {
        // arrange
        Player player = Player.GetInstance();
        player.AttackPower = playerAttackPower;
        Enemy test_enemy = new Enemy("test_enemy", enemyInitialHealth, 0, enemyDefense, 0, 0);
        player.Experience = 0;
        // act
        player.DealDamage(test_enemy);

        // assert
        Assert.Equal(expectedEnemyHealthAfterAttack, test_enemy.Health);

        //reset
        player.Health = 100;
        player.Experience = 0;

    }
}