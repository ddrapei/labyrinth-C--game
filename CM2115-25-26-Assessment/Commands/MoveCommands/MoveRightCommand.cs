using Commands;
using PlayerMovement;

namespace Commands.MoveCommands;

// Concrete command to MoveRight the player

public class MoveRightCommand : PlayerCommand
{
    private readonly IMoveBehavior moveBehavior;

    public MoveRightCommand()
    {
        this.moveBehavior = new PlayerMoveRight();
    }

    public void Execute()
    {
        moveBehavior.Move(Player.GetInstance());
    }
}