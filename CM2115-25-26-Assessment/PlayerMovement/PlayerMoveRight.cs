namespace PlayerMovement;

using Rooms;

using Pastel;

// Concrete implementation of right movement
public class PlayerMoveRight : IMoveBehavior
{
    public void Move(Player player)
    {

        player.StorePreviousPosition.StorePosition(player);
        int newXcoordinate = player.Xcoordinate + 1;

        if (RoomChecker.GetInstance().DoesRoomExist(newXcoordinate, player.Ycoordinate))
        {
            player.Xcoordinate = newXcoordinate;
            Console.WriteLine($"Player moved right. Current position: ({player.Xcoordinate}, {player.Ycoordinate})");
            RoomChecker.GetInstance().DisplayCurrentRoom(player);
        }
        else
        {
            Console.WriteLine("There is no room in that direction.".Pastel("#ff9d00"));
        }
    }
}