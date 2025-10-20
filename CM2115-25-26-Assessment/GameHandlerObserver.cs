// Handles input to start and finish the game

public class GameHandlerObserver : IGameObserver
{
    private Game game;

    public GameHandlerObserver(Game game)
    {
        this.game = game;
    }

    public void Update(string command)
    {
        if (!game.IsRunning)
        {
            if (command == "start" && !game.IsRunning)
            {
                game.IsRunning = true;
                Console.WriteLine("Game started!");
                return;
            }
            else
            {
                    Console.WriteLine("Invalid command or game state.");
                    return;
            }
        }
        else
        {
            if (command == "exit" || command == "finish")
            {
                game.IsRunning = false;
                game.IsFinished = true;
                Console.WriteLine("Exiting game...");
                return;
            }
        }
    }
}