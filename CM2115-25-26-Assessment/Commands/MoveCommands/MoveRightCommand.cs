namespace Commands.MoveCommands;

// Concrete command to MoveLeft the player
public class MoveRightCommand : PlayerCommand
{
    private Player player;

    public MoveRightCommand(Player player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.MoveRight();
    }
}