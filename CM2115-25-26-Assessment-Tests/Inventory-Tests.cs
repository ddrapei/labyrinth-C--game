using Items;
using Items.Potions;
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

    [Theory]
    [InlineData("Health Potion", "Health Potion", true)]
    [InlineData("Health potion", "Health Potion", true)]
    [InlineData("health potion", "Health Potion", true)]
    [InlineData("health Potion", "Health Potion", true)]
    [InlineData("HEALTH POTION", "Health Potion", true)]
    [InlineData("Big Potion", "Big Potion", true)]
    [InlineData("big Potion", "Big Potion", true)]
    [InlineData("Big potion", "Big Potion", true)]
    [InlineData("big potion", "Big Potion", true)]
    [InlineData("ig potion", "Big Potion", false)]
    [InlineData("health otion", "Health Potion", false)]
    [InlineData("sadkfkas", "Health Potion", false)]
    [InlineData("", "Health Potion", false)]
    [InlineData(" ", "Health Potion", false)]
    [InlineData("escalibur", "Health Potion", false)]

    public void GetItemByName_ReturnsItemByNameIfThatItemExistsInTheInventory(string search, string itemNameInInventory, bool itemExistInInventory)
    {
        // arrange
        Inventory inventory = new Inventory(100);
        Potion potion = new HealingPotion("Health Potion", 5);
        Potion big_potion = new HealingPotion("Big Potion", 10);
        inventory.AddItem(potion);
        inventory.AddItem(big_potion);
        // act
        var result = inventory.GetItemByName(search);

        if (itemExistInInventory)
        {
            Assert.NotNull(result);

            Assert.Equal(itemNameInInventory, result.Name);
        }
        else
        {
            Assert.Null(result);
        }
    }
}