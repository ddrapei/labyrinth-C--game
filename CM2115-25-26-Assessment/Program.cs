// creates a player
Player player = Player.GetInstance();

// creates a game
var game = new Game();

// crates an input manager
var InputManager = new InputManager();

// creates observers for the game
var gameHandlerObserver = new GameHandlerObserver(game);
var gameCommandMoveObserver = new GameCommandMoveObserver(game);

// adds observer to the game
InputManager.AddObserver(gameHandlerObserver);
InputManager.AddObserver(gameCommandMoveObserver);

// main game loop
while (!game.IsFinished)
{
    InputManager.ProcessInput();
} 