namespace PlayerMovement;

using Rooms;
using Pastel;

// concrete implementation of move to previous position
public class PlayerMoveToPreviousPosition : IMoveBehavior
{
    public void Move(Player player)
    {
        // check if previous position exists
        if (RoomChecker.GetInstance().doesRoomExist(player.PreviousXcoordinate, player.PreviousYcoordinate))
        {
            // move to previous coordinates
            player.Xcoordinate = player.PreviousXcoordinate;
            player.Ycoordinate = player.PreviousYcoordinate;
            
            RoomChecker.GetInstance().DisplayCurrentRoom(player);
        }
    }
}