namespace Commands.InventoryCommands;

using Observers;

// Command to open inventory and add InsideInventoryObserver


// Command to close inventory and remove InsideInventoryObserver
public class CloseInventoryCommand : PlayerCommand
{
    private InputManager inputManager;
    private IGameObserver insideInventoryObserver;
    private IGameObserver insideInventoryUnknownCommandObserver;
    private IGameObserver gameCommandMoveObserver;
    private IGameObserver gameHandlerObserver;
    private IGameObserver inventoryObserver;
    private IGameObserver unknownCommandObserver;


    public CloseInventoryCommand(InputManager inputManager, InsideInventoryObserver insideInventoryObserver, InsideInventoryUnknownCommandObserver insideInventoryUnknownCommandObserver, IGameObserver gameCommandMoveObserver, IGameObserver gameHandlerObserver, IGameObserver inventoryObserver, IGameObserver unknownCommandObserver)
    {
        this.inputManager = inputManager;
        this.insideInventoryObserver = insideInventoryObserver;
        this.insideInventoryUnknownCommandObserver = insideInventoryUnknownCommandObserver;
        this.gameCommandMoveObserver = gameCommandMoveObserver;
        this.gameHandlerObserver = gameHandlerObserver;
        this.inventoryObserver = inventoryObserver;
        this.unknownCommandObserver = unknownCommandObserver;
    }

    public void Execute()
    {
        // adds back observers for the main game
        inputManager.AddObserver(gameCommandMoveObserver);
        inputManager.AddObserver(gameHandlerObserver);
        inputManager.AddObserver(inventoryObserver);
        inputManager.AddObserver(unknownCommandObserver);


        // removes inventory menu observers
        inputManager.RemoveObserver(insideInventoryObserver);
        inputManager.RemoveObserver(insideInventoryUnknownCommandObserver);

        Console.WriteLine("Inventory closed.");
    }
}