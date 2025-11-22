using Items;
public class InventoryTests
{

    [Theory]
    [InlineData(2, 1, false)]
    [InlineData(2, 2, true)]
    [InlineData(2, 3, true)]
    [InlineData(2, 100, true)]
    [InlineData(100, 100, true)]
    [InlineData(3, 3, true)]  
    [InlineData(5, 2, false)]
    public void InventoryIsFull_ReturnsExpectedFullness(int capacity, int amountToAdd, bool IsFull)
    {
        // arrange
        var inventory = new Inventory(capacity);

        // act
        for (int i = 0; i < amountToAdd; i++)
        {
            inventory.AddItem(new Weapon($"Weapon {i}", 1));
        }

        // assert
        Assert.Equal(IsFull, inventory.isFull());
    }
}    