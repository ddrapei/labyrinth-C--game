using Items;
using PlayerEquipment;

[Collection("Sequential")]
public class EquipmentTests
{
    [Fact]
    public void EquipWeapon_DamageIncreasedWeaponEquipped()
    {
        // arrange
        Player player = Player.GetInstance();
        player.WeaponEquiped = null;
        player.BaseAttackPower = 1;
        player.AttackPower = 1;
        player.Defense = 0;
        
        IEquipBehavior weaponBehavior = new EquipWeaponBehavior();
        player.RegisterEquipBehavior("weapon", weaponBehavior);
        
        Weapon rusty_sword = new Weapon("Rusty Sword", 3);
        player.Inventory.AddItem(rusty_sword);
        
        int initialInventoryCount = player.Inventory.GetInventoryCount();

        // act
        bool result = weaponBehavior.Equip(player, rusty_sword);

        // assert
        Assert.True(result);
        Assert.Equal(4, player.AttackPower);
        Assert.Equal(rusty_sword, player.WeaponEquiped);
        Assert.Equal(0, player.Defense);
        Assert.Equal(player.Inventory.GetInventoryCount(), initialInventoryCount);
    }

    [Fact]
    public void EquipShield_DefenseIncreasedShieldEquipped()
    {
        // arrange
        Player player = Player.GetInstance();
        player.ShieldEquipped = null;
        player.BlockingDamageChance = 0.0;
        
        IEquipBehavior shieldBehavior = new EquipShieldBehavior();
        player.RegisterEquipBehavior("shield", shieldBehavior);
        
        Shield leather_shield = new Shield("Leather Shield", 0.15);
        player.Inventory.AddItem(leather_shield);
        
        int initialInventoryCount = player.Inventory.GetInventoryCount();

        // act
        bool result = shieldBehavior.Equip(player, leather_shield);

        // assert
        Assert.True(result);
        Assert.Equal(0.15, player.BlockingDamageChance);
        Assert.Equal(leather_shield, player.ShieldEquipped);
        Assert.Equal(player.Inventory.GetInventoryCount(), initialInventoryCount);
    }

    [Fact]
    public void AddItem_ItemAddedToInventory()
    {
        // arrange
        Player player = Player.GetInstance();
        Inventory inventory = player.Inventory;
        int initialCount = inventory.GetInventoryCount();
        
        Weapon rusty_sword = new Weapon("Rusty Sword", 3);

        // act
        bool result = inventory.AddItem(rusty_sword);

        // assert
        Assert.True(result);
        Assert.Equal(initialCount + 1, inventory.GetInventoryCount());
        Assert.True(inventory.HasItem(rusty_sword));
        Assert.Equal(rusty_sword.Name, inventory.GetItemByName("Rusty Sword")?.Name);
    }


}