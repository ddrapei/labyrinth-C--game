namespace PlayerMovement;
public class StorePreviousPosition : ITrackPosition
{
    public void StorePosition(Player player)
    {
        player.PreviousXcoordinate = player.Xcoordinate;
        player.PreviousYcoordinate = player.Ycoordinate;
    }
}