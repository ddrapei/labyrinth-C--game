using System;
using Rooms;
using Commands.MoveCommands;

namespace CM2115_25_26_Aassessment_Tests;

[Collection("Sequential-MoveTests")]
public class MoveLeftTest
{    
    [Fact]
    public void MoveLeft_ReturnsSubstractedXCoordinate()
    {
         // arrange
        Player player = Player.GetInstance();
        RoomChecker roomChecker = RoomChecker.GetInstance();
        MoveLeftCommand moveCommand = new MoveLeftCommand();
        
        // Set up room
        Room room = new RoomBuilder(-1, 0)
            .SetDescription("Test Room")
            .Build(); 
        roomChecker.AddRoom(room);

        // act
        moveCommand.Execute();

        // assert
        Assert.Equal(-1, player.Xcoordinate);
        Assert.Equal(0, player.Ycoordinate);
        Assert.Equal(0, player.PreviousXcoordinate);
        Assert.Equal(0, player.PreviousYcoordinate);

        player.ResetPlayerLocation();
    }
}



