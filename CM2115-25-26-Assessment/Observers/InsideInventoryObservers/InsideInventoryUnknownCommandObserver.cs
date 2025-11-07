namespace Observers.InsideInventoryObservers;


// this observer is used to register commands before the game has start,
// then it gets removed
public class InsideInventoryUnknownCommandObserver : IGameObserver
{
    private Game game;
    private HashSet<string> validCommands;

    public InsideInventoryUnknownCommandObserver(Game game)
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

        }
    }
}