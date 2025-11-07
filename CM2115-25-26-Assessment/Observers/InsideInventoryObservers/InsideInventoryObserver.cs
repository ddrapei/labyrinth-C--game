namespace Observers.InsideInventoryObservers;

using Commands;
using Commands.InventoryCommands;

// This observer is active only when inventory is open
public class InsideInventoryObserver : IGameObserver
{
    private Game game;
    private Dictionary<string, PlayerCommand> commands;

    public InsideInventoryObserver(Game game)
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
        // Check if it's a registered command
        if (commands.ContainsKey(commandString))
        {
            commands[commandString].Execute();
            return;
        }
        
        // Handle equipping from the inventory
        if (commandString.StartsWith("equip "))
        {
            string itemIdentifier = commandString.Substring(6).Trim();
            EquipItemCommand equipCommand = new EquipItemCommand(itemIdentifier);
            equipCommand.Execute();
            return;
        }

        // Handle "drop number" or "drop item name"
        // Example: drop 1 or drop Lether Helmet
        if (commandString.StartsWith("drop "))
        {
            // Substring(5) - skips drop and space after it
            // itemIdentifier stores either only number of the item or the item name
            string itemIdentifier = commandString.Substring(5).Trim();
            DropItemCommand dropCommand = new DropItemCommand(itemIdentifier);
            dropCommand.Execute();
            return;
        }
        
        // Unknown command (those are shown now in the case of not valid input)
        Console.WriteLine("Unknown inventory command: " + commandString + "."); 
        Console.WriteLine("Inventory commands:");
        Console.WriteLine("drop <number of the item> or drop <the name of the item>");
        Console.WriteLine("close");

    }
}