public class Room
{
    private int xcoordinate;
    private int ycoordinate;
    private string description;
    public int Xcoordinate
    {
        get { return xcoordinate; }
    }
    public int Ycoordinate
    {
        get { return ycoordinate; }
    }
    public string Description
    {
        get { return description; }
    }

    public Room(int xcoordinate, int ycoordinate, string description)
    {
        this.xcoordinate = xcoordinate;
        this.ycoordinate = ycoordinate;
        this.description = description;
    }
}