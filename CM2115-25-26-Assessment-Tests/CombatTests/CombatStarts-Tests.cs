using Combat;
using Enemies;


[Collection("Sequential")]

public class CombatStartsTests
{
    [Theory]
    [InlineData(1.00, true)]
    [InlineData(0.00, false)]
    public void EnemyNoticesPlayer_CombatStarts(double perception, bool inCombat)
    {
        // arrange
        Player player = Player.GetInstance();
        CombatSystem combatSystem = CombatSystem.GetInstance();
        var enemy = new Enemy("Tralalelo Tralala", 10, 0, 0, perception, 0);

        // act
        enemy.NoticePlayer();
        enemy.StartAttackingPlayer();

        // assert
        Assert.Equal(inCombat, combatSystem.IsInCombat);

        // reset
        combatSystem.IsInCombat = false;
    }
}
