namespace Commands.InventoryCommands;

using Observers;
using Observers.InsideInventoryObservers;

using Pastel;

// Command to open inventory and add InsideInventoryObserver
public class OpenInventoryCommand : PlayerCommand
{
    private InputManager inputManager;
    private IGameObserver insideInventoryObserver;
    private IGameObserver gameCommandMoveObserver;
    private IGameObserver gameHandlerObserver;
    private IGameObserver inventoryObserver;
    private IGameObserver unknownCommandObserver;

    public OpenInventoryCommand(InputManager inputManager, InsideInventoryObserver insideInventoryObserver, IGameObserver gameCommandMoveObserver, IGameObserver gameHandlerObserver, IGameObserver inventoryObserver, IGameObserver unknownCommandObserver)
    {
        this.inputManager = inputManager;
        this.insideInventoryObserver = insideInventoryObserver;
        this.gameCommandMoveObserver = gameCommandMoveObserver;
        this.gameHandlerObserver = gameHandlerObserver;
        this.inventoryObserver = inventoryObserver;
        this.unknownCommandObserver = unknownCommandObserver;
    }

    public void Execute()
    {
        Player player = Player.GetInstance();

        // removes the observers from the main game
        inputManager.RemoveObserver(gameCommandMoveObserver);
        inputManager.RemoveObserver(gameHandlerObserver);
        inputManager.RemoveObserver(inventoryObserver);
        inputManager.RemoveObserver(unknownCommandObserver);


        // adds the inside inventory observers
        inputManager.AddObserver(insideInventoryObserver);

        // displays inventory
        player.Inventory.DisplayInventory();
        Console.WriteLine("Available inventory commands:");
        Console.WriteLine("use ".Pastel("#6f9e00") +  "<item number or name>");
        Console.WriteLine("equip ".Pastel("#6f9e00") +  "<item number or name>");
        Console.WriteLine("drop ".Pastel("#6f9e00") +  "<item number or name>");
        Console.WriteLine("close".Pastel("#ff0318"));
    }
}