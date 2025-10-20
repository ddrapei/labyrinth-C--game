public class GameCommandMoveObserver : IGameObserver
{
    private Game game;

    public GameCommandMoveObserver(Game game)
    {
        this.game = game;
    }

    public void Update(string command)
    {
        if (game.IsRunning)
        {
            if (command == "move up")
            {
                Console.WriteLine("Player moves up");
            }
            else if (command == "move down")
            {
                Console.WriteLine("Player moves down");
            }
            else if (command == "move left")
            {
                Console.WriteLine("Player moves left");
            }
            else if (command == "move right")
            {
                Console.WriteLine("Player moves right");
            }
        }
    }

}