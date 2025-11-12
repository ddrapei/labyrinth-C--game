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
            Console.WriteLine("Unknown command: " + command.Pastel("#ff000d") + ".");
            Console.WriteLine("Use: " + "attack ".Pastel("#a3101d") + "- to attack the enemy");
            Console.WriteLine("Or " + "run ".Pastel("#30a310") + "- to try to escape the combat");
        }
    }
}