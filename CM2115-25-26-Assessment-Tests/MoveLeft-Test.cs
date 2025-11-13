using System;

namespace CM2115_25_26_Aassessment_Test;

public class MoveLeftTest
{
    [Theory]
    [InlineData(0, -1)]
    public void MoveLeft_ReturnsSubstractedXCoordinate(int xdefault, int xestimated)
    {
        // arrange
        Player player = Player.GetInstance();

        int x = player.Xcoordinate;

        // act
        player.MoveLeft();

        //assert
        Assert.Equal(xdefault, x);
    }
}

