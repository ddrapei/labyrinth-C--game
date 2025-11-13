using Commands;
using PlayerMovement;

namespace Commands.MoveCommands;

// Concrete command to MoveDown the player
public class MoveDownCommand : PlayerCommand
{
    private readonly IMoveBehavior moveBehavior;

    public MoveDownCommand()
    {
        this.moveBehavior = new PlayerMoveDown();
    }

    public void Execute()
    {
        moveBehavior.Move(Player.GetInstance());
    }
}