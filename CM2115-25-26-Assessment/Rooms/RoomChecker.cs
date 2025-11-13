namespace Rooms;

using Items;
using Items.Armour;
using Items.Armour.LeatherArmourSet;
using Enemies;


using Pastel;

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
        return null; // returns null if there is no room with those coordinates
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

    // method to display current room's description, enemyes and puzzles in the future
    public void DisplayCurrentRoom(Player player)
    {
        Room currentRoom = GetCurrentRoom(player);
        if (currentRoom != null)
        {
            Console.WriteLine($"You are in room at ({currentRoom.Xcoordinate}, {currentRoom.Ycoordinate}): {currentRoom.Description}");
            if (currentRoom.Item != null)
            {
                if (currentRoom.Item is Weapon weapon)
                {
                    Console.WriteLine("You see an weapon here: " + currentRoom.Item.Name.Pastel("#ff0000"));
                }
                else if (currentRoom.Item is Shield shield)
                {
                    Console.WriteLine("You see a shield here: " + currentRoom.Item.Name.Pastel("#00a2ff"));
                }
                else if (currentRoom.Item is Armour armour)
                {
                    Console.WriteLine("You see an armour here: " + currentRoom.Item.Name.Pastel("#00a2ff"));
                }
                else
                {
                    Console.WriteLine("You see an item here: " + currentRoom.Item.Name.Pastel("#ffa200"));
                }
            }

            if (currentRoom.Enemy != null && !currentRoom.Enemy.IsDead())
            {
                Console.WriteLine("The enemy is here: " + currentRoom.Enemy.Name.Pastel("#ff00ff"));
                Console.WriteLine("Fight - to fight the enemy");

                if (currentRoom.Enemy.NoticePlayer())
                {
                    currentRoom.Enemy.StartAttackingPlayer();
                }
            }
        }
    }
}