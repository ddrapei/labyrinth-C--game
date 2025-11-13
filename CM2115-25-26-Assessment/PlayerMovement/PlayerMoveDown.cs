namespace PlayerMovement;

using Rooms;
using Pastel;


// Concrete implementation of down movement
public class PlayerMoveDown : IMoveBehavior
{
    public void Move(Player player)
    {

        player.StorePreviousPosition.StorePosition(player);
        int newYcoordinate = player.Ycoordinate - 1;

        if (RoomChecker.GetInstance().doesRoomExist(player.Xcoordinate, newYcoordinate))
        {
            player.Ycoordinate = newYcoordinate;
            Console.WriteLine($"Player moved down. Current position: ({player.Xcoordinate}, {player.Ycoordinate})");
            RoomChecker.GetInstance().DisplayCurrentRoom(player);
        }
        else
        {
            Console.WriteLine("There is no room in that direction.".Pastel("#ff9d00"));
        }
    }
}