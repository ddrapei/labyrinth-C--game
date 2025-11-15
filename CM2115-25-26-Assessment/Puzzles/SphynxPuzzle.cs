namespace Puzzles;

using Pastel;

public class SphynxPuzzle : IPuzzle
{
    private string question;
    private string correctAnswer;
    private bool isSolved;
    private int damage;

    public bool IsSolved
    {
        get { return isSolved; }
        set { isSolved = value; }
    }

    public string CorrectAnswer
    {
        get { return correctAnswer; }
    }
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public SphynxPuzzle()
    {
        // question
        this.question = "I speak without a mouth and hear without ears. I have no body, but I come alive with wind. What am I?";
        this.correctAnswer = "echo";
        this.isSolved = false;
        this.damage = 25;
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
        Console.WriteLine("║".Pastel("#8B4513") + "           A primeval Sphynx blocks your path!             ".Pastel("#DAA520") + "║".Pastel("#8B4513").Pastel("#DAA520"));
        Console.WriteLine("╚═══════════════════════════════════════════════════════════╝".Pastel("#8B4513"));
        Console.WriteLine("The Sphynx speaks:".Pastel("#FFD700"));
        Console.WriteLine($"{question}".Pastel("#FFA500"));
        Console.WriteLine("Type 'answer [your answer]' to respond.".Pastel("#FFFFFF"));
    }

    public void CheckAnswer(string playerAnswer)
    {
        Player player = Player.GetInstance();

        if (playerAnswer.Trim().ToLower() == correctAnswer.ToLower())
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗".Pastel("#00FF00"));
            Console.WriteLine("║".Pastel("#00FF00") + "                 The Sphynx nods                           ".Pastel("#32CD32") + "║".Pastel("#00FF00"));
            Console.WriteLine("║".Pastel("#00FF00") + "              Correct! You may pass.                       ".Pastel("#7FFF00") + "║".Pastel("#00FF00"));
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝".Pastel("#00FF00"));
            Console.WriteLine("The Sphynx disapperars".Pastel("#90EE90"));

            isSolved = true;

            // Exit puzzle
            PuzzleSystem.GetInstance().ExitPuzzle();
        }
        else
        {

            if (player.Health <= 0)
            {
                Console.WriteLine("");
                Console.WriteLine("=== DEAD ===".Pastel("#ff0000"));
                Console.WriteLine("You have been slaughtered...".Pastel("#ff0000"));
                Console.WriteLine("");
                PuzzleSystem.GetInstance().ExitPuzzle();
                return;
            }

            player.Health -= this.damage;
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗".Pastel("#d4020d"));
            Console.WriteLine("║".Pastel("#d4020d") + "                   The Sphynx says no                      ".Pastel("#f72530") + "║".Pastel("#d4020d"));
            Console.WriteLine("║".Pastel("#d4020d") + "                        Incorrect                          ".Pastel("#ab262d") + "║".Pastel("#d4020d"));
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝".Pastel("#d4020d"));
            Console.WriteLine("The Sphynx is angry!".Pastel("#b30009"));
            Console.WriteLine("The Sphynx pecks you");
            Console.WriteLine($"The Sphynx deals {this.damage}");
            Console.WriteLine("The Sphynx repeats the question!".Pastel("#b30009"));
            Console.WriteLine("Try again. Type 'answer [your answer]' to respond.".Pastel("#FFFFFF"));
        }
    }

    public string GetHint()
    {
        return "Think about sound and how it travels...";
    }
}