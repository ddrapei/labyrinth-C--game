// creates a player
Player player = Player.GetInstance();

// creates a game
var game = new Game();

// crates an input manager
var InputManager = new InputManager();

// creates observers for the game
// handles start and finish the game
var gameHandlerObserver = new GameHandlerObserver(game);

// handles player movement commands
var gameCommandMoveObserver = new GameCommandMoveObserver(game);

// handles unknown commands
var unknownCommandObserver = new UnknownCommandObserver(game);

// creates commands for player movement
var moveUp = new MoveUpCommand(player);
var moveDown = new MoveDownCommand(player);
var moveLeft = new MoveLeftCommand(player);
var moveRight = new MoveRightCommand(player);

// registers commands with the command observer
gameCommandMoveObserver.AddCommand("move up", moveUp);
gameCommandMoveObserver.AddCommand("move down", moveDown);
gameCommandMoveObserver.AddCommand("move left", moveLeft);
gameCommandMoveObserver.AddCommand("move right", moveRight);

// registers valid commands with the unknown command observer
unknownCommandObserver.RegisterValidCommand("start");
unknownCommandObserver.RegisterValidCommand("exit");
unknownCommandObserver.RegisterValidCommand("move up");
unknownCommandObserver.RegisterValidCommand("move down");
unknownCommandObserver.RegisterValidCommand("move left");
unknownCommandObserver.RegisterValidCommand("move right");

// adds observers to the game
InputManager.AddObserver(gameHandlerObserver);
InputManager.AddObserver(gameCommandMoveObserver);
InputManager.AddObserver(unknownCommandObserver);

// main game loop
while (!game.IsFinished)
{
    InputManager.ProcessInput();
} 