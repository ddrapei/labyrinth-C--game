namespace Observers;

// this observer is used to register commands before the game has start,
// then it gets removed
public class MainMenuUnknownCommandObserver : IGameObserver
{
    private Game game;
    private HashSet<string> validCommands;

    public MainMenuUnknownCommandObserver(Game game)
    {
        this.game = game;
        this.validCommands = new HashSet<string>();
    }

    public void RegisterValidCommand(string commandString)
    {
        validCommands.Add(commandString);
    }

    public void Update(string command)
    {
        if (!game.IsRunning && !validCommands.Contains(command))
        {
            Console.WriteLine("Unknown command:"  + command + ". Type 'start' to begin the game.");
        }
    }
}