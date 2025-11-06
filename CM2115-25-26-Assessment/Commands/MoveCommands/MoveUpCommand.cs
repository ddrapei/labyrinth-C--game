namespace Commands.MoveCommands;
// Concrete command to MoveUp the player
public class MoveUpCommand : PlayerCommand
{
    private Player player;

    public MoveUpCommand(Player player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.MoveUp();
    }
}