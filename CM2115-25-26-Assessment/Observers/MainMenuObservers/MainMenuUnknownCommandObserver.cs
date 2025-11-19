namespace Observers.MainMenuObservers;

using Pastel;
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
            Console.WriteLine("Unknown command: "  + command.Pastel("#00eaff") + ".");
            Console.WriteLine("Availible commands:");
            Console.WriteLine("Start".Pastel("#00ff00"));
            Console.WriteLine("Statistics".Pastel("#00eaff"));
            Console.WriteLine("Exit".Pastel("#ff0000"));
        }
    }
}