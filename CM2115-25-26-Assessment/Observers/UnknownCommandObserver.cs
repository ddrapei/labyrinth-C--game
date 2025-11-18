// this observer was added to check for unknown commands,
// without it, both observers were trying to handle unknown commands
// and it resulted in always printing unknown command message
using Observers;

using Pastel;
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
            Console.WriteLine("Unknown command: " +  command.Pastel("#00eaff"));
            Console.WriteLine("☰☰☰☰Availible commands: ☰☰☰☰");
            Console.WriteLine("");
            Console.WriteLine("Move up".Pastel("#de8100"));
            Console.WriteLine("Move down".Pastel("#de8100"));
            Console.WriteLine("Move left".Pastel("#de8100"));
            Console.WriteLine("Move right".Pastel("#de8100"));
            Console.WriteLine("Inventory".Pastel("#0800ff"));
            Console.WriteLine("Pick up".Pastel("#ff00fb"));
            Console.WriteLine("Stats".Pastel("#0800ff"));
            Console.WriteLine("Attack".Pastel("#ff000d"));
            Console.WriteLine("Look around".Pastel("#9900ff"));
            Console.WriteLine("Equipped".Pastel("#00ff1e"));
            Console.WriteLine("");
        }
    }
}