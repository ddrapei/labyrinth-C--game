using Observers;

namespace Commands.MoveCommands;

// Concrete command to MoveDown the player
public class MoveDownCommand : PlayerCommand
{
    private Player player;
    private InputManager inputManager;

    public MoveDownCommand(Player player, InputManager inputManager)
    {
        this.player = player;
        this.inputManager = inputManager;
    }

    public void Execute()
    {
        player.MoveDown();
    }
}