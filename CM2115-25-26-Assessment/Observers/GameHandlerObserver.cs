namespace Observers;
public class GameHandlerObserver : IGameObserver
{
    private Game game;

    public GameHandlerObserver(Game game)
    {
        this.game = game;
    }

    public void Update(string command)
    {
        if (command == "start" && !game.IsRunning)
        {
            game.IsRunning = true;
            Console.WriteLine("Game started!");
        }
        else if (command == "exit" || command == "quit" || command == "finish")
        {
            game.IsRunning = false;
            game.IsFinished = true;
            Console.WriteLine("Exiting game...");
        }
    }
}