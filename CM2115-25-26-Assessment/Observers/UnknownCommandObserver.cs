// this observer was added to check for unknown commands,
// without it, both observers were trying to handle unknown commands
// and it resulted in always printing unknown command message
using Observers;
public class UnknownCommandObserver : IGameObserver
{
    private Game game;
    // hashset was used because of storing unique values
    private HashSet<string> validCommands;

    public UnknownCommandObserver(Game game)
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
        // Only check for unknown commands when the game is running
        if (game.IsRunning && !validCommands.Contains(command))
        {
            Console.WriteLine($"Unknown command: '{command}'. Type a command.");
        }
        // if the game is not running, checks only for start and exit commands
        else if (!game.IsRunning && command != "start" && command != "exit")
        {
            Console.WriteLine($"Unknown command: '{command}'. Type 'start' to begin the game.");
        }
    }
}