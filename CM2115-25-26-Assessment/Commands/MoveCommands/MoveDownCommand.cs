using Observers;

namespace Commands.MoveCommands;

// Concrete command to MoveDown the player
public class MoveDownCommand : PlayerCommand
{
    private Player player;

    public MoveDownCommand(Player player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.MoveDown();
    }
}