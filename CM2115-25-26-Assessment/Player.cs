// Singleton player class that represents the only one player in the game

public class Player
{
    private static Player instance = null;

    public static Player GetInstance()
    {
        {
            if (instance == null)
            {
                instance = new Player(100, 0, 0);
            }
            return instance;
        }
    }

    private Player() { }

    private int health;
    private int xcoordinate;
    private int ycoordinate;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int Xcoordinate
    {
        get { return xcoordinate; }
        set { xcoordinate = value; }
    }

    public int Ycoordinate
    {
        get { return ycoordinate; }
        set { ycoordinate = value; }
    }

    public Player(int health, int xcoordinate, int ycoordinate)
    {
        this.health = health;
        this.xcoordinate = xcoordinate;
        this.ycoordinate = ycoordinate;
    }

    // methods for player movement
    public void MoveUp()
    {
        ycoordinate++;
        Console.WriteLine($"Player moved up. Current position: ({xcoordinate}, {ycoordinate})");
    }

    public void MoveDown()
    {
        ycoordinate--;
        Console.WriteLine($"Player moved down. Current position: ({xcoordinate}, {ycoordinate})");
    }

    public void MoveLeft()
    {
        xcoordinate--;
        Console.WriteLine($"Player moved left. Current position: ({xcoordinate}, {ycoordinate})");
    }
    public void MoveRight()
    {
        xcoordinate++;
        Console.WriteLine($"Player moved right. Current position: ({xcoordinate}, {ycoordinate})");
    }
}