namespace Commands.MoveCommands;
// Concrete command to MoveLeft the player
public class MoveLeftCommand : PlayerCommand
{
    private Player player;

    public MoveLeftCommand(Player player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.MoveLeft();
    }
}