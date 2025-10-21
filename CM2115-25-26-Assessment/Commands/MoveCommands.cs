namespace Commands;
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