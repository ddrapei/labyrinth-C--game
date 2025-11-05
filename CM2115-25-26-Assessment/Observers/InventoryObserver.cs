namespace Observers;

using Commands;

public class InventoryObserver : IGameObserver
{
    private Game game;
    private Dictionary<string, PlayerCommand> commands;
    private bool waitingForDropInput;

    public InventoryObserver(Game game)
    {
        this.game = game;
        this.commands = new Dictionary<string, PlayerCommand>();
        this.waitingForDropInput = false;
    }

    public void AddCommand(string commandString, PlayerCommand command)
    {
        commands[commandString] = command;
    }

    public void Update(string commandString)
    {
        // the game has to run
        if (!game.IsRunning)
        {
            return;
        }

        if (waitingForDropInput)
        {
            waitingForDropInput = false;
            DropItemCommand dropCommand = new DropItemCommand(commandString);
            dropCommand.Execute();
            return;
        }

        if (commands.ContainsKey(commandString))
        {
            this.commands[commandString].Execute();
        }

        if (commandString == "drop")
        {
            Player player = Player.GetInstance();
            if (player.Inventory.isEmpty())
            {
                Console.WriteLine("The inventory is empty");
            }
            else
            {
                Console.WriteLine("Enter the item number or name to drop");
                player.Inventory.DisplayInventory();
                waitingForDropInput = true;
            }
        }


    }
}
