using Puzzles;

namespace Commands.PuzzleCommands;

public class AnswerRiddleCommand : PlayerCommand
{
    private IPuzzle puzzle;
    private string? answer;

    public AnswerRiddleCommand(IPuzzle puzzle)
    {
        this.puzzle = puzzle;
    }

    public void SetAnswer(string answer)
    {
        this.answer = answer;
    }

    public void Execute()
    {
        if (string.IsNullOrWhiteSpace(answer))
        {
            Console.WriteLine("Usage: answer [your answer]");
            return;
        }

        puzzle.CheckAnswer(answer);
    }
}