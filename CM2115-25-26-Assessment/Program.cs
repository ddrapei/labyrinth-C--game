// creates a player
Player player = Player.GetInstance();

// creates a game
var game = new Game();

// crates an input manager
var InputManager = new InputManager();

// creates an observer for the game
var gameHandlerObserver = new GameHandlerObserver(game);

// adds observer to the game
InputManager.AddObserver(gameHandlerObserver);

while (!game.IsFinished)
{
    InputManager.ProcessInput();
} 