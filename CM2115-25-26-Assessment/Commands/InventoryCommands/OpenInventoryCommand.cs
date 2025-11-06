namespace Commands.InventoryCommands;

using Observers;

// Command to open inventory and add InsideInventoryObserver
public class OpenInventoryCommand : PlayerCommand
{
    private InputManager inputManager;
    private InsideInventoryObserver insideInventoryObserver;
    private InsideInventoryUnknownCommandObserver insideInventoryUnknownCommandObserver;
    private IGameObserver gameCommandMoveObserver;
    private IGameObserver gameHandlerObserver;
    private IGameObserver inventoryObserver;
    private IGameObserver unknownCommandObserver;

    public OpenInventoryCommand(InputManager inputManager, InsideInventoryObserver insideInventoryObserver, InsideInventoryUnknownCommandObserver insideInventoryUnknownCommandObserver, IGameObserver gameCommandMoveObserver, IGameObserver gameHandlerObserver, IGameObserver inventoryObserver, IGameObserver unknownCommandObserver)
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
        Player player = Player.GetInstance();

        // removes the observers from the main game
        inputManager.RemoveObserver(gameCommandMoveObserver);
        inputManager.RemoveObserver(gameHandlerObserver);
        inputManager.RemoveObserver(inventoryObserver);
        inputManager.RemoveObserver(unknownCommandObserver);


        // adds the inside inventory observers
        inputManager.AddObserver(insideInventoryObserver);
        inputManager.AddObserver(insideInventoryUnknownCommandObserver);

        // displays inventory
        player.Inventory.DisplayInventory();
        Console.WriteLine("Commands: drop <number>, close");
        

    }
}