namespace Observers.CombatObservers;

using Commands;
using Pastel;

public class CombatUnknownCommandObserver : IGameObserver
{
    private Game game;
    private HashSet<string> validCommands;

    public CombatUnknownCommandObserver(Game game)
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
        if (!validCommands.Contains(command))
        {
             Console.WriteLine("Unknown command: "  + command + ".");
        }
    }
}