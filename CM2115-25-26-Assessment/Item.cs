// abstract class for items
public abstract class Item
{
    private string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public Item(string name)
    {
        Name = name;
    }
}