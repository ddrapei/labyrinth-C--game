namespace Commands.InventoryCommands;

using Observers;
using Observers.InsideInventoryObservers;

// Command to open inventory and add InsideInventoryObserver


// Command to close inventory and remove InsideInventoryObserver
public class CloseInventoryCommand : PlayerCommand
{
    private InputManager inputManager;
    private IGameObserver insideInventoryObserver;
    private IGameObserver gameCommandMoveObserver;
    private IGameObserver gameHandlerObserver;
    private IGameObserver inventoryObserver;
    private IGameObserver unknownCommandObserver;


    public CloseInventoryCommand(InputManager inputManager, InsideInventoryObserver insideInventoryObserver, IGameObserver gameCommandMoveObserver, IGameObserver gameHandlerObserver, IGameObserver inventoryObserver, IGameObserver unknownCommandObserver)
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
        // adds back observers for the main game
        inputManager.AddObserver(gameCommandMoveObserver);
        inputManager.AddObserver(gameHandlerObserver);
        inputManager.AddObserver(inventoryObserver);
        inputManager.AddObserver(unknownCommandObserver);


        // removes inventory menu observers
        inputManager.RemoveObserver(insideInventoryObserver);

        Console.WriteLine("Inventory closed.");
    }
}