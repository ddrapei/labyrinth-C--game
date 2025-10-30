// Singleton pattern, since it will always be the only one room checker in the game
public class RoomChecker
{
    private static RoomChecker instance = null;
    private List<Room> rooms;

    public static RoomChecker GetInstance()
    {
        if (instance == null)
        {
            instance = new RoomChecker();
        }
        return instance;
    }

    // --- Constructor --- 
    private RoomChecker()
    {
        rooms = new List<Room>();
    }

    // method to add a room to the List of the RoomChecker
    public void AddRoom(Room room)
    {
        rooms.Add(room);
    }

    // method to get a room by its coordinates
    public Room GetRoom(int xcoordinate, int ycoordinate)
    {
        foreach (Room room in rooms)
        {
            if (room.Xcoordinate == xcoordinate && room.Ycoordinate == ycoordinate)
            {
                return room;
            }
        }
        return null; // returns null if the room wasn't found at the given coordinates
    }

    // method get current room from player's coordinates
    public Room GetCurrentRoom(Player player)
    {
        return GetRoom(player.Xcoordinate, player.Ycoordinate);
    }

    // method to check if a room exists at given coordinates
    public bool doesRoomExist(int xcoordinate, int ycoordinate)
    {
        return GetRoom(xcoordinate, ycoordinate) != null;
    } 

    // method to display current room's description and items (for now)
    public void DisplayCurrentRoom(Player player)
    {
        Room currentRoom = GetCurrentRoom(player);
        if (currentRoom != null)
        {
            Console.WriteLine($"You are in room at ({currentRoom.Xcoordinate}, {currentRoom.Ycoordinate}): {currentRoom.Description}");
            if (currentRoom.Item != null)
            {
                Console.WriteLine($"You see an item here: {currentRoom.Item.Name}");
            }
        }
        else
        {
            Console.WriteLine("There is no room at your current location.");
        }
    }
}