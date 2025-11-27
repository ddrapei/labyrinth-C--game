namespace Commands.MainMenuCommands;

using Observers;

using Pastel;

// Command to start the game
public class StartGameCommand : PlayerCommand
{
    private Game game;
    private InputManager inputManager;
    private IGameObserver mainMenuObserver;
    private IGameObserver mainMenuUnknownCommandObserver;
    private IGameObserver gameCommandMoveObserver;
    private IGameObserver gameHandlerObserver;
    private IGameObserver inventoryObserver;
    private IGameObserver unknownCommandObserver;

    public StartGameCommand(Game game, InputManager inputManager, IGameObserver mainMenuObserver, IGameObserver mainMenuUnknownCommandObserver, IGameObserver gameCommandMoveObserver, IGameObserver gameHandlerObserver, IGameObserver inventoryObserver, IGameObserver unknownCommandObserver)
    {
        this.game = game;
        this.inputManager = inputManager;
        this.mainMenuObserver = mainMenuObserver;
        this.mainMenuUnknownCommandObserver = mainMenuUnknownCommandObserver;
        this.gameCommandMoveObserver = gameCommandMoveObserver;
        this.gameHandlerObserver = gameHandlerObserver;
        this.inventoryObserver = inventoryObserver;
        this.unknownCommandObserver = unknownCommandObserver;
    }

    public void Execute()
    {
        if (!game.IsRunning)
        {
            Player player = Player.GetInstance();
            game.IsRunning = true;

            // that's for statistics
            GameStatistics.AddGamesStarted();


            Console.WriteLine("∘˙○˚.•Game started!∘˙○˚.•".Pastel("#00ff00"));
            Console.WriteLine("");
            Console.WriteLine("✶ ✶ ✶ ✶  Availible commands  ✶ ✶ ✶ ✶");
            Console.WriteLine("◆  Move up".Pastel("#de8100"));
            Console.WriteLine("◆  Move down".Pastel("#de8100"));
            Console.WriteLine("◆  Move left".Pastel("#de8100"));
            Console.WriteLine("◆  Move right".Pastel("#de8100"));
            Console.WriteLine("◆  Inventory".Pastel("#0800ff"));
            Console.WriteLine("◆  Pick up".Pastel("#ff00fb"));
            Console.WriteLine("◆  Stats".Pastel("#0800ff"));
            Console.WriteLine("◆  Attack".Pastel("#ff000d"));
            Console.WriteLine("◆  Look around".Pastel("#9900ff"));
            Console.WriteLine("◆  Equipped".Pastel("#00ff1e"));
            Console.WriteLine("");
        

            // remove the start game observer since game has started
            inputManager.RemoveObserver(mainMenuUnknownCommandObserver);
            inputManager.RemoveObserver(mainMenuObserver);

            // adds observers for the main game
            inputManager.AddObserver(gameCommandMoveObserver);
            inputManager.AddObserver(gameHandlerObserver);
            inputManager.AddObserver(inventoryObserver);
            inputManager.AddObserver(unknownCommandObserver);

            // displaying the first room
            player.LookAround();
        }
    }
}
