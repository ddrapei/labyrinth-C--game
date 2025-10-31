using Items;
public class Room
{
    private int xcoordinate;
    private int ycoordinate;
    private string description;
    private Item item;
    public int Xcoordinate
    {
        get { return xcoordinate; }
        set { xcoordinate = value; }
    }
    public int Ycoordinate
    {
        get { return ycoordinate; }
        set { ycoordinate = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    public Item Item
    {
        get { return item; }
        set { item = value; }
    }

    // --- Constructor --- 
    public Room(RoomBuilder builder)
    {
        this.xcoordinate = builder.Xcoordinate;
        this.ycoordinate = builder.Ycoordinate;
        this.description = builder.Description;
        this.item = builder.Item;
    }
}