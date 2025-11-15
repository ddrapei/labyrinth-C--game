namespace PlayerLevelUp;

public class PlayerCheckLevelUpBehavior : ILevelUpBehavior
{
    public void LevelUp(Player player)
    {
        if (player.LevelUpBehaviors.ContainsKey("level up"))
        {
            player.LevelUpBehaviors["level up"].LevelUp(player);
        }
    }
}