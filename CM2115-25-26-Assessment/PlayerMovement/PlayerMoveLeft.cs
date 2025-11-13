namespace PlayerMovement;

using Rooms;

using Pastel;

// Concrete implementation of left movement
public class PlayerMoveLeft : IMoveBehavior
{
    public void Move(Player player)
    {
        player.StorePreviousPosition.StorePosition(player);
        int newXcoordinate = player.Xcoordinate - 1;

        if (RoomChecker.GetInstance().doesRoomExist(newXcoordinate, player.Ycoordinate))
        {
            player.Xcoordinate = newXcoordinate;
            Console.WriteLine($"Player moved left. Current position: ({player.Xcoordinate}, {player.Ycoordinate})");
            RoomChecker.GetInstance().DisplayCurrentRoom(player);
        }
        else
        {
            Console.WriteLine("There is no room in that direction.".Pastel("#ff9d00"));
        }
    }
}