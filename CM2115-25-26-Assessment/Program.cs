Player player = Player.GetInstance();

var game = new Game();
var InputManager = new InputManager();
var gameHandlerObserver = new GameHandlerObserver(game);

InputManager.AddObserver(gameHandlerObserver);

while (!game.IsFinished)
{
    InputManager.ProcessInput();
} 