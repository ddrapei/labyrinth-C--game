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

    private Inventory inventory;

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

    public Inventory Inventory
    {
        get { return inventory; }
    }

    // --- Constructor ---
    private Player(int health, int xcoordinate, int ycoordinate)
    {
        this.health = health;
        this.xcoordinate = xcoordinate;
        this.ycoordinate = ycoordinate;
        this.inventory = new Inventory(10);
    }

    // methods for player movement
    public void MoveUp()
    {
        int newYcoordinate = ycoordinate + 1;

        if (RoomChecker.GetInstance().doesRoomExist(xcoordinate, newYcoordinate))
        {
            ycoordinate = newYcoordinate;
            Console.WriteLine($"Player moved up. Current position: ({xcoordinate}, {ycoordinate})");
            RoomChecker.GetInstance().DisplayCurrentRoom(this);
        }
        else
        {
            Console.WriteLine("There is no room in that direction.");
        }
    }

    public void MoveDown()
    {
        int newYcoordinate = ycoordinate - 1;

        if (RoomChecker.GetInstance().doesRoomExist(xcoordinate, newYcoordinate))
        {
            ycoordinate = newYcoordinate;
            Console.WriteLine($"Player moved up. Current position: ({xcoordinate}, {ycoordinate})");
            RoomChecker.GetInstance().DisplayCurrentRoom(this);
        }
        else
        {
            Console.WriteLine("There is no room in that direction.");
        }
    }

    public void MoveLeft()
    {
        int newXcoordinate = xcoordinate - 1;

        if (RoomChecker.GetInstance().doesRoomExist(newXcoordinate, ycoordinate))
        {
            xcoordinate = newXcoordinate;
            Console.WriteLine($"Player moved up. Current position: ({xcoordinate}, {ycoordinate})");
            RoomChecker.GetInstance().DisplayCurrentRoom(this);
        }
        else
        {
            Console.WriteLine("There is no room in that direction.");
        }
    }
    public void MoveRight()
    {
       int newXcoordinate = xcoordinate + 1;

        if (RoomChecker.GetInstance().doesRoomExist(newXcoordinate, ycoordinate))
        {
            xcoordinate = newXcoordinate;
            Console.WriteLine($"Player moved up. Current position: ({xcoordinate}, {ycoordinate})");
            RoomChecker.GetInstance().DisplayCurrentRoom(this);
        }
        else
        {
            Console.WriteLine("There is no room in that direction.");
        }
    }
}