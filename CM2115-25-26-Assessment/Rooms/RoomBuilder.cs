namespace Rooms;

using Items;
using Enemies;
using Puzzles;
public class RoomBuilder
{
    private int xcoordinate;
    private int ycoordinate;
    private string? description;
    private Item? item;
    private Enemy? enemy;
    private IPuzzle? puzzle;

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
    public string? Description
    {
        get { return description; }
        set { description = value; }
    }
    public Item? Item
    {
        get { return item; }
        set { item = value; }
    }
    public Enemy? Enemy
    {
        get { return enemy; }
        set { enemy = value; }
    }

    public IPuzzle? Puzzle
    {
        get { return puzzle; }
        set{ puzzle = value; }
    }

    

    // --- Constructor ---
    public RoomBuilder(int xcoordinate, int ycoordinate)
    {
        this.Xcoordinate = xcoordinate;
        this.Ycoordinate = ycoordinate;
        this.Description = null;
        this.Item = null;
    }

    public RoomBuilder SetDescription(string description)
    {
        this.Description = description;
        return this;
    }
    public RoomBuilder AddEnemy(Enemy enemy)
    {
        this.Enemy = enemy;
        return this;
    }
    public RoomBuilder AddItem(Item item)
    {
        this.Item = item;
        return this;
    }

    public RoomBuilder AddPuzzle(IPuzzle puzzle)
    {
        this.Puzzle = puzzle;
        return this;
    }

    public Room Build()
    {
        return new Room(this);
    }
}