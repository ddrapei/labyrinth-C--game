using Enemies;
using Combat;


[Collection("Sequential")]

public class EnemyAttacksTest
{
    [Fact]
    public void EnemyAttacks_ReturnsSubstractedPlayerHealth()
    {
        // arrange
        Player player = Player.GetInstance();
        CombatSystem combatSystem = CombatSystem.GetInstance();
        player.Health = 100;
        Enemy wild_boar = new Enemy("Wild Boar", 20, 5, 0, 0.1, 0);
        combatSystem.StartCombat(wild_boar);
        player.Experience = 0;

        
        // act
        combatSystem.EnemyAttack();

        // assert
        Assert.Equal(95, player.Health);

        // finishing combat
        combatSystem.EndCombat();

        //reset
        player.Health = 100;
        player.Experience = 0;


    }
}