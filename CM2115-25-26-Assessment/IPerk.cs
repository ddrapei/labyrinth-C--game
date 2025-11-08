// strategy pattern to add and remove perks on the fly
// depending on the condition
public interface IPerk
{
    void Apply(Player player);
    void Remove(Player player);
}