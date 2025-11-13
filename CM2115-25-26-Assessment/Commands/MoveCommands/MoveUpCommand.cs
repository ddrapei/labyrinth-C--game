using Commands;
using PlayerMovement;

namespace Commands.MoveCommands;

// Concrete command to MoveUp the player

public class MoveUpCommand : PlayerCommand
{
    private readonly IMoveBehavior moveBehavior;

    public MoveUpCommand()
    {
        this.moveBehavior = new PlayerMoveUp();
    }

    public void Execute()
    {
        moveBehavior.Move(Player.GetInstance());
    }
}