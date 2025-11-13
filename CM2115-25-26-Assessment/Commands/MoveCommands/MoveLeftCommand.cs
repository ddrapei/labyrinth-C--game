using Commands;
using PlayerMovement;

namespace Commands.MoveCommands;

// Concrete command to MoveLeft the player

public class MoveLeftCommand : PlayerCommand
{
    private readonly IMoveBehavior moveBehavior;

    public MoveLeftCommand()
    {
        this.moveBehavior = new PlayerMoveLeft();
    }

    public void Execute()
    {
        moveBehavior.Move(Player.GetInstance());
    }
}