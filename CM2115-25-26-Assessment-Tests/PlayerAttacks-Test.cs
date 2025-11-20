
using System;
using Enemies;
using Commands.MoveCommands;

namespace CM2115_25_26_Assessment_Tests.CombatTests;


[Collection("Sequential")]
public class EnemyTakeDamageTest
{
    [Fact]

    public void ReturnsSubstractedEnemyHealth()
    {
         // arrange
        Player player = Player.GetInstance();
        CombatSystem combatSystem = CombatSystem.GetInstance();
        player.AttackPower = 5;
        Enemy wild_boar = new Enemy("Wild Boar", 20, 5, 0, 0.1, 10);
        combatSystem.StartCombat(wild_boar);
        
        // act
        combatSystem.PlayerAttack();

        // assert
        Assert.Equal(15, wild_boar.Health);
    }
}

