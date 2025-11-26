namespace PlayerMovement;

using Rooms;

using Pastel;

// Concrete implementation of up movement
public class PlayerMoveUp : IMoveBehavior
{
    public void Move(Player player)
    {
        
        // Store current position before moving
        player.StorePreviousPosition.StorePosition(player);

        // Calculate new coordinate
        int newYcoordinate = player.Ycoordinate + 1;

        if (RoomChecker.GetInstance().DoesRoomExist(player.Xcoordinate, newYcoordinate))
        {
            // Only if a room exists, assign the new coordinate
            player.Ycoordinate = newYcoordinate;
            Console.WriteLine($"Player moved up. Current position: ({player.Xcoordinate}, {player.Ycoordinate})");
            RoomChecker.GetInstance().DisplayCurrentRoom(player);
        }
        else
        {
            // If a room in that direction doesn't exist, player stays at the same place
            Console.WriteLine("There is no room in that direction.".Pastel("#ff9d00"));
        }
    }
}