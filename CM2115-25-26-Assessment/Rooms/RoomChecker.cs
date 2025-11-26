namespace Rooms;

using Items;
using Items.Armour;
using Items.Armour.LeatherArmourSet;
using Enemies;
using Puzzles;
using Pastel;

// Singleton pattern, since it will always be the only one room checker in the game
public class RoomChecker
{
    private static RoomChecker? instance = null;
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
    public Room? GetRoom(int xcoordinate, int ycoordinate)
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
    public Room? GetCurrentRoom(Player player)
    {
        return GetRoom(player.Xcoordinate, player.Ycoordinate);
    }

    // method to check if a room exists at given coordinates
    public bool DoesRoomExist(int xcoordinate, int ycoordinate)
    {
        return GetRoom(xcoordinate, ycoordinate) != null;
    }

    // method to display current room's description, enemyes and puzzles in the future
    public void DisplayCurrentRoom(Player player)
    {
        Room? currentRoom = GetCurrentRoom(player);

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

            // Check for puzzle first - puzzles take priority
            bool hasUnsolvedPuzzle = false;
            if (currentRoom.Puzzle != null)
            {
                // Check if puzzle is solved using reflection to access IsSolved property
                var puzzleType = currentRoom.Puzzle.GetType();
                var isSolvedProperty = puzzleType.GetProperty("IsSolved");

                if (isSolvedProperty != null)
                {
                    // FIXED: Safe cast to bool? and null check
                    // If GetValue returns null, it defaults to false
                    bool isSolved = (bool?)isSolvedProperty.GetValue(currentRoom.Puzzle) ?? false;

                    if (!isSolved)
                    {
                        hasUnsolvedPuzzle = true;
                        PuzzleSystem puzzleSystem = PuzzleSystem.GetInstance();
                        puzzleSystem.EnterPuzzle(currentRoom.Puzzle);
                    }
                }
            }

            // Only check for enemy if there's no unsolved puzzle
            if (!hasUnsolvedPuzzle && currentRoom.Enemy != null && !currentRoom.Enemy.IsDead())
            {
                if (currentRoom.Enemy.Name == "Broodmother Gargoyle")
                {
                    Console.WriteLine(@"                            __,                  ,__");
                    Console.WriteLine(@"                           //                      \\");
                    Console.WriteLine(@"                          / L/|                  |\J |");
                    Console.WriteLine(@"                          |  /                    \  |");
                    Console.WriteLine(@"                         ,/^|       ,~-_,--`,      |^\");
                    Console.WriteLine(@"                        / /\|      /         \,    ||\\~,");
                    Console.WriteLine(@"                       / //\\     / ,\,./';.  `,   || \\ ~,");
                    Console.WriteLine(@"                      / //  \\    | (~Q'Q~';   ; ,//   \\  ~.");
                    Console.WriteLine(@"                    ,/ //    \\   (, \ ^_,    / //'     \\   ~-.");
                    Console.WriteLine(@"                   / //'      \`,.__\_`--' \,/,'L        `\\    \.");
                    Console.WriteLine(@"                 ,/ //         `~---/ //         \.        `\\    \,");
                    Console.WriteLine(@"                / ,/'               | |\,          \.        `:,    \");
                    Console.WriteLine(@"               |  ;;                !/   `-_  ,`-_   \_,      \`,    :,");
                    Console.WriteLine(@"               |  ||                (  ,:    )/\. `.  /        ||     |");
                    Console.WriteLine(@"               |  |!               / `-- `--'/  \,/  /         !|     :,");
                    Console.WriteLine(@"              ,; ,;'              ;  /\     <    / /'          ':,     |");
                    Console.WriteLine(@"              |  ||              /  ;/ )     \,-' /\            ||     |");
                    Console.WriteLine(@"              |  ||             / ./; /_ ,  _{=_.'  | __        ||     :,");
                    Console.WriteLine(@"              |  ||        __  ; /_/ |--:==:--""\    |/  \       ||      |");
                    Console.WriteLine(@"              |  ||       /  \| (   / \       / |        \      ||      |");
                    Console.WriteLine(@"              |  |!      /    | ,] |   |     /  \         \     |!      |");
                    Console.WriteLine(@"             ,; ,;' __  |      \L, |    \   |    \         |   ,;;      |");
                    Console.WriteLine(@"             |  || /  \ |          \    |  /      |        | __|!       |");
                    Console.WriteLine(@"             |  ||/    \|           \    \ |      |        |/ ,;;       |");
                    Console.WriteLine(@"             |  ||                   \    \|      |           !;        ;");
                    Console.WriteLine(@"             |  ::                   <\   (""\|  /\(          ,:'       /");
                    Console.WriteLine(@"             :  \\                    \    ` \.:  i\,        ||       /");
                    Console.WriteLine(@"              \  \\                   |    |  `,   | `-_     ;!      /");
                    Console.WriteLine(@"               \ ||                   |  ,/    |`  |_,  `-_  '.\    /");
                    Console.WriteLine(@"                \|!                  /  /       \  |  `-_  `-__\\  /");
                    Console.WriteLine(@"                 :;                 /  /_       _) !     `-_____\\|-,");
                    Console.WriteLine(@"                 /                 /  ,--~      \.  \            \|-,\");
                    Console.WriteLine(@"                               ,-_/  /            ;  \            \  \\");
                    Console.WriteLine(@"                              _--,  |             |   \_              |:");
                    Console.WriteLine(@"                             (---, /             :,; `,_)            /-'");
                    Console.WriteLine(@"                              (--'               ' \,] '           ");
                }

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