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

    [Theory]
    [InlineData(1, true)]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(4, true)]
    [InlineData(5, true)]
    [InlineData(6, false)]
    [InlineData(7, false)]
    [InlineData(8, false)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    [InlineData(-2, false)]
    [InlineData(-200, false)]
    [InlineData(200, false)]

    public void GetItemByNumber_ReturnsItemByNumberIfThatItemExistsInTheInventory(int itemIndex, bool itemExistInInventory)
    {
        // arrange
        Inventory inventory = new Inventory(10);
        Weapon test_weapon = new Weapon("Weapon", 1);

        inventory.AddItem(test_weapon);
        inventory.AddItem(test_weapon);
        inventory.AddItem(test_weapon);
        inventory.AddItem(test_weapon);
        inventory.AddItem(test_weapon);

        // act
        var result = inventory.GetItemByNumber(itemIndex);

        // assert

        if (itemExistInInventory)
        {
            Assert.Equal(test_weapon, result);
        }
        else
        {
            Assert.Null(result);
        }






    }
}    