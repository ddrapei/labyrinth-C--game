using PlayerLevelUp;
using Combat;
using Enemies;

[Collection("Sequential")]
public class LevelUpTests
{
    [Theory]
    [InlineData(110, 3, 0)]
    [InlineData(120, 3, 10)]
    [InlineData(130, 3, 20)]
    [InlineData(10, 1, 10)]
    [InlineData(50, 2, 0)]
    [InlineData(60, 2, 10)]
    [InlineData(70, 2, 20)]
    [InlineData(80, 2, 30)]
    [InlineData(90, 2, 40)]
    [InlineData(0, 1, 0)]
    [InlineData(20, 1, 20)]
    [InlineData(30, 1, 30)]
    [InlineData(180, 4, 0)]
    [InlineData(260, 5, 0)]
    [InlineData(265, 5, 5)]
    [InlineData(261, 5, 1)]
    [InlineData(269, 5, 9)]

    public void LevelUpBehavior_RightLevelIsReturnedWithTheRightExperienceLeft(int experience, int expectedLevel, int experienceLeftAfterLevelUp)
    {
        // arrange
        Player player = Player.GetInstance();
        player.Experience = 0;
        player.ExperienceRequiredForNewLevel = 50;
        player.Level = 1;
        CombatSystem combatSystem = CombatSystem.GetInstance();
        player.AttackPower = 100000000; // high enough to kill the enemy
        player.Health = 100;
        Enemy enemy = new Enemy("enemy_test", 5, 1, 0, 0.1, experience);
        combatSystem.StartCombat(enemy);


        player.RegisterLevelUpBehavior("level up", new PlayerLevelUpBehavior());
        player.RegisterLevelUpBehavior("check level up", new PlayerCheckLevelUpBehavior());

        // act
        combatSystem.PlayerAttack();

        // assert
        Assert.Equal(expectedLevel, player.Level);
        Assert.Equal(experienceLeftAfterLevelUp, player.Experience);

        //reset
        player.Experience = 0;
        player.Level = 1;
        player.ExperienceRequiredForNewLevel = 50;

    }
}