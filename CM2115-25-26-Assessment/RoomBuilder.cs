public class RoomBuilder
{
    private int xcoordinate;
    private int ycoordinate;
    private string description;

    public int Xcoordinate
    {
        set { xcoordinate = value; }
    }

    public int Ycoordinate
    {
        set { ycoordinate = value; }
    }
    public string Description
    {
        set { description = value; }
    }

    public RoomBuilder(int xcoordinate, int ycoordinate, string description)
    {
        this.xcoordinate = xcoordinate;
        this.ycoordinate = ycoordinate;
        this.description = description;
    }

    public RoomBuilder AddEnemy()
    {
        // logic to add enemy to the room
        return this;
    }
    public RoomBuilder AddItem()
    {
        // logic to add item to the room
        return this;
    }

    public RoomBuilder AddPuzzle()
    {
        // logic to add puzzle to the room
        return this;
    }

    public Room Build()
    {
        return new Room(xcoordinate, ycoordinate, description);
    }
}