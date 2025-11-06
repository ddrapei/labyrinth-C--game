namespace Commands;

using Observers;

// Command to start the game
public class StartGameCommand : PlayerCommand
{
    private Game game;
    private InputManager inputManager;
    private IGameObserver startGameObserver;
    private IGameObserver mainMenuUnknownCommandObserver;
    private IGameObserver gameCommandMoveObserver;
    private IGameObserver gameHandlerObserver;
    private IGameObserver inventoryObserver;
    private IGameObserver unknownCommandObserver;

    public StartGameCommand(Game game, InputManager inputManager, IGameObserver startGameObserver, IGameObserver mainMenuUnknownCommandObserver, IGameObserver gameCommandMoveObserver, IGameObserver gameHandlerObserver, IGameObserver inventoryObserver, IGameObserver unknownCommandObserver)
    {
        this.game = game;
        this.inputManager = inputManager;
        this.startGameObserver = startGameObserver;
        this.startGameObserver = mainMenuUnknownCommandObserver;
        this.gameCommandMoveObserver = gameCommandMoveObserver;
        this.gameHandlerObserver = gameHandlerObserver;
        this.inventoryObserver = inventoryObserver;
        this.unknownCommandObserver = unknownCommandObserver;
    }

    public void Execute()
    {
        if (!game.IsRunning)
        {
            game.IsRunning = true;
            Console.WriteLine("Game started!");
            
            // Remove the start game observer since game has started
            inputManager.RemoveObserver(mainMenuUnknownCommandObserver);
            inputManager.RemoveObserver(startGameObserver);
            inputManager.AddObserver(gameCommandMoveObserver);
            inputManager.AddObserver(gameHandlerObserver);
            inputManager.AddObserver(inventoryObserver);
            inputManager.AddObserver(unknownCommandObserver);
        }
    }
}
