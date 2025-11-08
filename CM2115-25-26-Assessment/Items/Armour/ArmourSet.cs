namespace Items.Armour;

using Perks;
// strategy pattern is used
public class ArmourSet
{
    private string name;
    private List<IPerk> perks;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public List<IPerk> Perks
    {
        get { return perks; }
        set { perks = value; }
    }

    // --- Constructor --- 
    public ArmourSet(string name)
    {
        this.name = name;
        this.perks = new List<IPerk>();
    }

    public void AddPerk(IPerk perk)
    {
        // makes sure that we don't add null to the list of perks
        if (perk != null)
        {
            this.perks.Add(perk);
        }
    }

    // activates all the perks in the list to the player
    public void Activate(Player player)
    {
        // checks each perk in the list
        foreach (IPerk perk in perks)
        {
            perk.Apply(player);
        }
    }

    public void Deactivate(Player player)
    {
        // checks each perk in the list
        foreach (IPerk perk in perks)
        {
            perk.Remove(player);
        }
    }
}