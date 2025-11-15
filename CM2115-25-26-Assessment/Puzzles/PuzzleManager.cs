namespace Puzzles;

public class PuzzleManager
{
    private static PuzzleManager? instance = null;

    public static PuzzleManager GetInstance()
    {
        {
            if (instance == null)
            {
                instance = new PuzzleManager();
            }
            return instance;
        }
    }
    private Dictionary<string, IPuzzle> puzzles;

    public Dictionary<string, IPuzzle> Puzzles
    {
        get { return puzzles; }
        set { puzzles = value; }
    }

    private PuzzleManager()
    {
        this.puzzles = new Dictionary<string, IPuzzle>();

    }

    public void RegisterPuzzle(string puzzlename, IPuzzle puzzle)
    {
        if (!puzzles.ContainsKey(puzzlename))
        {
            puzzles.Add(puzzlename, puzzle);
        }
    }

}