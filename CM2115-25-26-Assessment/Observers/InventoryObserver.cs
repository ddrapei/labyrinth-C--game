namespace Observers;

using Commands;

// this observer handles commands related to an inventory when the inventory is closed
public class InventoryObserver : IGameObserver
{
    private Game game;
    private Dictionary<string, PlayerCommand> commands;

    public InventoryObserver(Game game)
    {
        this.game = game;
        this.commands = new Dictionary<string, PlayerCommand>();
    }

    public void AddCommand(string commandString, PlayerCommand command)
    {
        commands[commandString] = command;
    }

    public void Update(string commandString)
    {

        if (commands.ContainsKey(commandString))
        {
            this.commands[commandString].Execute();
        }
    }
}