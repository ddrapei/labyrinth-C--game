using System;

namespace CM2115_25_26_Aassessment_Test;

public class MoveDownTest
{
    [Theory]
    [InlineData(0, -1)]
    public void MoveDown_ReturnsSubstractedYCoordinate(int ydefault, int yestimated)
    {
        // arrange
        Player player = Player.GetInstance();

        int y = player.Ycoordinate;

        // act
        player.MoveDown();

        //assert
        Assert.Equal(ydefault, y);
    }
}

