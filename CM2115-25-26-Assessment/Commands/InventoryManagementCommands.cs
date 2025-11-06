namespace Commands;

using Observers;

// Command to open inventory and add InsideInventoryObserver
public class OpenInventoryCommand : PlayerCommand
{
    private InputManager inputManager;
    private InsideInventoryObserver insideInventoryObserver;

    public OpenInventoryCommand(InputManager inputManager, InsideInventoryObserver insideInventoryObserver)
    {
        this.inputManager = inputManager;
        this.insideInventoryObserver = insideInventoryObserver;
    }

    public void Execute()
    {
        Player player = Player.GetInstance();
        player.Inventory.DisplayInventory();
        Console.WriteLine("Commands: drop <number>, close");
        
        // Add the inside inventory observer
        inputManager.AddObserver(insideInventoryObserver);
    }
}

// Command to close inventory and remove InsideInventoryObserver
public class CloseInventoryCommand : PlayerCommand
{
    private InputManager inputManager;
    private InsideInventoryObserver insideInventoryObserver;

    public CloseInventoryCommand(InputManager inputManager, InsideInventoryObserver insideInventoryObserver)
    {
        this.inputManager = inputManager;
        this.insideInventoryObserver = insideInventoryObserver;
    }

    public void Execute()
    {
        // once the inventory is closed the observer for inside inventory commands is removed
        inputManager.RemoveObserver(insideInventoryObserver);
        Console.WriteLine("Inventory closed.");
    }
}