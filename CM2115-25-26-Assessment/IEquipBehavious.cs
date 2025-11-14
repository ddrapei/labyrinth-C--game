namespace PlayerEquipment;

using Items;

public interface IEquipBehavior
{
    bool Equip(Player player, Item item);
}