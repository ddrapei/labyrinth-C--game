namespace PlayerLevelUp;

using Rooms;
using Pastel;


// Concrete implementation of down movement
public class PlayerLevelUpBehavior : ILevelUpBehavior
{
    public void LevelUp(Player player)  
    {
        

        if (player.Experience >= 100)
        {
            player.Level += 1;
            player.Health += 100;  
            player.Experience -= 100;
        }                
    }
}