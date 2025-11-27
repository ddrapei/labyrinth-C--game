using System;
using Rooms;
using Commands.MoveCommands;

namespace CM2115_25_26_Assessment_Tests.MoveTests;

// the idea how to handle Singleton pollution was taken from here:
// https://medium.com/selectstarfromweb/xunit-tests-in-parallel-ad32788ce1bd
[Collection("Sequential")]
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

        // reset
        player.Xcoordinate = 0;
        player.Ycoordinate = 0;
        player.PreviousXcoordinate = 0;
        player.PreviousYcoordinate = 0;

    }
}



