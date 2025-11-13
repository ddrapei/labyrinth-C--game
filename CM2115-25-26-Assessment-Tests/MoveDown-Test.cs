
using System;
using Rooms;

namespace CM2115_25_26_Assessment_Tests;

public class MoveDownTest
{
    [Fact]

    public void MoveDown_ReturnsSubstractedYCoordinate()
    {
         // arrange
        Player player = Player.GetInstance();
        RoomChecker roomChecker = RoomChecker.GetInstance();
        
        // Set up room
        Room room = new RoomBuilder(0, -1)
            .SetDescription("Test Room")
            .Build(); 
        roomChecker.AddRoom(room);

        // act
        player.MoveDown();

        // assert
        Assert.Equal(0, player.Xcoordinate);
        Assert.Equal(-1, player.Ycoordinate);
        Assert.Equal(0, player.PreviousXcoordinate);
        Assert.Equal(0, player.PreviousYcoordinate);

        player.ResetPlayerLocation();
    }
}

