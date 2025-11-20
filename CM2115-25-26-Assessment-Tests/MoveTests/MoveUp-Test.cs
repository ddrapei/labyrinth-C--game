using System;
using Rooms;
using Commands.MoveCommands;

namespace CM2115_25_26_Assessment_Tests.MoveTests;

// the idea how to handle Singleton pollution was taken from here:
// https://medium.com/selectstarfromweb/xunit-tests-in-parallel-ad32788ce1bd
[Collection("Sequential")]
public class MoveUpTest
{
    [Fact]

    public void MoveUp_ReturnsAddedYCoordinate()
    {
         // arrange
        Player player = Player.GetInstance();
        RoomChecker roomChecker = RoomChecker.GetInstance();
        MoveUpCommand moveCommand = new MoveUpCommand();

        
        // Set up room
        Room room = new RoomBuilder(0, 1)
            .SetDescription("Test Room")
            .Build(); 
        roomChecker.AddRoom(room);

        // act
        moveCommand.Execute();

        // assert
        Assert.Equal(0, player.Xcoordinate);
        Assert.Equal(1, player.Ycoordinate);
        Assert.Equal(0, player.PreviousXcoordinate);
        Assert.Equal(0, player.PreviousYcoordinate);

        player.ResetPlayerLocation();
    }
}