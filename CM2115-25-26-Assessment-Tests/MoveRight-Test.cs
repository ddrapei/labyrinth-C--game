using System;

namespace CM2115_25_26_Aassessment_Test;

public class MoveRightTest
{
    [Theory]
    [InlineData(0, 1)]
    public void MoveUp_ReturnsAddedXCoordinate(int xdefault, int xestimated)
    {
        // arrange
        Player player = Player.GetInstance();

        int x = player.Xcoordinate;

        // act
        player.MoveRight();

        //assert
        Assert.Equal(xdefault, x);
    }
}

