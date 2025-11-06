namespace Commands.InventoryCommands;

using Observers;

// Command to open inventory and add InsideInventoryObserver


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