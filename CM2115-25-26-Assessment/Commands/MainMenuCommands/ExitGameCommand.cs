namespace Commands.MainMenuCommands;

// the command to finish the game
public class ExitGameCommand : PlayerCommand
{
    private Game game;

    public ExitGameCommand(Game game)
    {
        this.game = game;
    }

    public void Execute()
    {
        game.IsRunning = false;
        game.IsFinished = true;
        Console.WriteLine("Exiting game...");
    }
}