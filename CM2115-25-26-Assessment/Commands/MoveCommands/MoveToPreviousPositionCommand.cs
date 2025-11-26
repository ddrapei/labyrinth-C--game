using Commands;
using PlayerMovement;

namespace Commands.MoveCommands;

// concrete command to move player back to previous position
public class MoveToPreviousPositionCommand : PlayerCommand
{
    private readonly IMoveBehavior moveBehavior;

    public MoveToPreviousPositionCommand()
    {
        this.moveBehavior = new PlayerMoveToPreviousPosition();
    }

    public void Execute()
    {
        moveBehavior.Move(Player.GetInstance());
    }
}