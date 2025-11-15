using Puzzles;

namespace Commands.PuzzleCommands;

public class EnterPuzzleCommand : PlayerCommand
{
    private IPuzzle puzzle;

    public EnterPuzzleCommand(IPuzzle puzzle)
    {
        this.puzzle = puzzle;
    }

    public void Execute()
    {
        PuzzleSystem.GetInstance().EnterPuzzle(puzzle);
    }
}