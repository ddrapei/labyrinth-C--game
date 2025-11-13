// for extendability testing

/*

namespace PlayerMovement;

using Rooms;
using Pastel;


// Concrete implementation of down movement
public class PlayerMoveDiagonallyUpAndRight : IMoveBehavior
{
    public void Move(Player player)
    {

        player.StorePreviousPosition.StorePosition(player);
        int newYcoordinate = player.Ycoordinate + 1;
        int newXcoordinate = player.Xcoordinate + 1;

        if (RoomChecker.GetInstance().doesRoomExist(newXcoordinate, newYcoordinate))
        {
            player.Ycoordinate = newYcoordinate;
            player.Xcoordinate = newXcoordinate;

            Console.WriteLine($"Player moved diagonally up and right. Current position: ({player.Xcoordinate}, {player.Ycoordinate})");
            RoomChecker.GetInstance().DisplayCurrentRoom(player);
        }
        else
        {
            Console.WriteLine("There is no room in that direction.".Pastel("#ff9d00"));
        }
    }
}
*/
