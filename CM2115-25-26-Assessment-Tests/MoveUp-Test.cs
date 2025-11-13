using System;

namespace CM2115_25_26_Aassessment_Tests;

public class MoveUpTest
{
    [Theory]
    [InlineData(0, 1)]
    public void MoveUp_ReturnsAddedYCoordinate(int ydefault, int yestimated)
    {
        // arrange
        Player player = Player.GetInstance();

        int y = player.Ycoordinate;

        // act
        player.MoveUp();

        //assert
        Assert.Equal(ydefault, y);
    }
}

