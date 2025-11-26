namespace PlayerLevelUp;

public class PlayerCheckLevelUpBehavior : ILevelUpBehavior
{
    public void Execute(Player player)
    {
        if (player.LevelUpBehaviors.ContainsKey("level up"))
        {
            player.LevelUpBehaviors["level up"].Execute(player);
        }
    }
}