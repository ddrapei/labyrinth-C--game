using Items;

public class InventoryCheckerTests
{
    [Theory]
    [InlineData("1", 1, true)]
    [InlineData("3", 3, true)]
    [InlineData("mavpa", 0, false)]
    [InlineData("1000", 1000, true)]
    [InlineData("1sdsfd", 0, false)]
    [InlineData("13 e", 0, false)]
    [InlineData("1 2", 0, false)]
    [InlineData("1 asd 2", 0, false)]
    [InlineData("-1", 0, false)]
    [InlineData("-100", 0, false)]
    [InlineData("0", 0, true)]

    public void IsNumericInput_ReturnsTrueIfResultIsNumeric(string input, int expectedIndex, bool expectedOutcome)
    {
        // act
        var result = InventoryChecker.IsNumericInput(input, out int actualIndex);

        // assert
        Assert.Equal(result, expectedOutcome);
        Assert.Equal(expectedIndex, actualIndex);
    }
}