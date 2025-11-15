using Pastel;

namespace Puzzles;

public class SphynxPuzzle : IPuzzle
{
    private string question;
    private string correctAnswer;
    private bool isSolved;

    public bool IsSolved
    {
        get { return isSolved; }
        set { isSolved = value; }
    }

    public string CorrectAnswer
    {
        get { return correctAnswer; }
    }

    public SphynxPuzzle()
    {
        // question
        this.question = "I speak without a mouth and hear without ears. I have no body, but I come alive with wind. What am I?";
        this.correctAnswer = "echo";
        this.isSolved = false;
    }

    public void StartPuzzle()
    {
        if (isSolved)
        {
            Console.WriteLine("The Sphynx's puzzle has already been solved.".Pastel("#808080"));
            return;
        }
        
        // Display the puzzle
        Console.WriteLine("" + "╔═══════════════════════════════════════════════════════════╗".Pastel("#8B4513"));
        Console.WriteLine("║".Pastel("#8B4513")+"           A primeval Sphynx blocks your path!             ".Pastel("#DAA520") + "║".Pastel("#8B4513").Pastel("#DAA520"));
        Console.WriteLine("╚═══════════════════════════════════════════════════════════╝".Pastel("#8B4513"));
        Console.WriteLine("The Sphynx speaks:".Pastel("#FFD700"));
        Console.WriteLine($"{question}".Pastel("#FFA500"));
        Console.WriteLine("Type 'answer [your answer]' to respond.".Pastel("#FFFFFF"));
    }

    public void CheckAnswer(string playerAnswer)
    {
        if (playerAnswer.Trim().ToLower() == correctAnswer.ToLower())
        {
            Console.WriteLine("═══════════════════════════════════════════════════════════".Pastel("#00FF00"));
            Console.WriteLine("The Sphynx nods".Pastel("#32CD32"));
            Console.WriteLine("Correct! You may pass.".Pastel("#7FFF00"));
            Console.WriteLine("═══════════════════════════════════════════════════════════".Pastel("#00FF00"));
            Console.WriteLine("The Sphynx disapperars".Pastel("#90EE90"));
            
            isSolved = true;
            
            // Exit puzzle
            PuzzleSystem.GetInstance().ExitPuzzle();
        }
        else
        {
            Console.WriteLine("═══════════════════════════════════════════════════════════".Pastel("#FF0000"));
            Console.WriteLine("The Sphynx says no".Pastel("#DC143C"));
            Console.WriteLine("Incorrect.".Pastel("#FF4500"));
            Console.WriteLine("═══════════════════════════════════════════════════════════".Pastel("#FF0000"));
            Console.WriteLine("Try again. Type 'answer [your answer]' to respond.".Pastel("#FFFFFF"));
        }
    }

    public string GetHint()
    {
        return "Think about sound and how it travels...";
    }
}