namespace Observers.InsideInventoryObservers;

using Commands;
using Commands.InventoryCommands;

using Pastel;

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
        // 
        var (command, parameter) = ParseInput(commandString);

        // check for register commands that consist from 1 word
        if (commands.ContainsKey(command))
        {
            commands[command].Execute();
            return;
        }
        
        if (command == "use")
        {
            if (string.IsNullOrEmpty(parameter))
            {
                Console.WriteLine("Use <item number or name>");
                return;
            }
            
            UsePotionCommand useCommand = new UsePotionCommand(parameter);
            useCommand.Execute();
            return;
        }
        
        if (command == "equip")
        {
            if (string.IsNullOrEmpty(parameter))
            {
                Console.WriteLine("Equip <item number or name>");
                return;
            }
            
            EquipItemCommand equipCommand = new EquipItemCommand(parameter);
            equipCommand.Execute();
            return;
        }

        if (command == "drop")
        {
            if (string.IsNullOrEmpty(parameter))
            {
                Console.WriteLine("Drop <item number or name>");
                return;
            }

            DropItemCommand dropCommand = new DropItemCommand(parameter);
            dropCommand.Execute();
            return;
        }    
        
        // Unknown command
        Console.WriteLine("Unknown inventory command: " + commandString + "."); 
        Console.WriteLine();
        Console.WriteLine("Available inventory commands:");
        Console.WriteLine("use ".Pastel("#6f9e00") +  "<item number or name>");
        Console.WriteLine("equip ".Pastel("#6f9e00") +  "<item number or name>");
        Console.WriteLine("drop ".Pastel("#6f9e00") +  "<item number or name>");
        Console.WriteLine("close".Pastel("#ff0318"));
    }


    private (string command, string parameter) ParseInput(string input)
    {
        string trimmedInput = input.Trim();
        
        // saves the number of the first space (the index of the first space)
        int firstSpaceIndex = trimmedInput.IndexOf(' ');

        if (firstSpaceIndex == -1)
        {
            // No space found 
            return (trimmedInput.ToLower(), "");
        }
        

        // splits string into 2 parts using that firstSpaceIndex 
        string command = trimmedInput.Substring(0, firstSpaceIndex).ToLower();
        string parameter = trimmedInput.Substring(firstSpaceIndex + 1).ToLower().Trim();
        
        return (command, parameter);
    }
}