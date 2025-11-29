namespace Puzzles;
public interface IPuzzle
{
    bool IsSolved { get; }
    public void StartPuzzle();
    public void CheckAnswer(string answer);
}