namespace Puzzles;

using Pastel;

public class OedipusPuzzle : IPuzzle
{
    private string question;
    private string correctAnswer;
    private bool isSolved;
    private int damage;
    private int failedAttempts;

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
    public int FailedAttempts
    {
        get { return failedAttempts; }
        set { failedAttempts = value; }
    }

    public OedipusPuzzle()
    {
        // question
        this.question = "What walks on four feet in the morning, two in the afternoon, and three at night?";
        this.correctAnswer = "man";
        this.isSolved = false;
        this.damage = 30;
        this.failedAttempts = 0;
    }

    public void StartPuzzle()
    {
        if (isSolved)
        {
            Console.WriteLine("The Oedipus puzzle has already been solved.".Pastel("#808080"));
            return;
        }

        // Display the puzzle
        Console.WriteLine("" + "╔═══════════════════════════════════════════════════════════╗".Pastel("#8B4513"));
        Console.WriteLine("║".Pastel("#8B4513") + "           Oedipus - King of Thebes blocks your path!    ".Pastel("#DAA520") + "║".Pastel("#8B4513").Pastel("#DAA520"));
        Console.WriteLine("╚═══════════════════════════════════════════════════════════╝".Pastel("#8B4513"));
        Console.WriteLine("Oedipus speaks:".Pastel("#FFD700"));
        Console.WriteLine($"{question}".Pastel("#FFA500"));
        Console.WriteLine("Type 'answer [your answer]' to respond.".Pastel("#90EE90"));
    }

    public void CheckAnswer(string playerAnswer)
    {
        Player player = Player.GetInstance();

        if (playerAnswer.Trim().ToLower() == correctAnswer.ToLower())
        {
            int experienceGranted = player.Experience += 50;
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗".Pastel("#00FF00"));
            Console.WriteLine("║".Pastel("#00FF00") + "                 The Sphynx nods                           ".Pastel("#32CD32") + "║".Pastel("#00FF00"));
            Console.WriteLine("║".Pastel("#00FF00") + "              Correct! You may pass.                       ".Pastel("#7FFF00") + "║".Pastel("#00FF00"));
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝".Pastel("#00FF00"));
            Console.WriteLine("Oedipus disapperars".Pastel("#90EE90"));
            Console.WriteLine($"You gained + {experienceGranted}xp".Pastel("#90EE90"));


            isSolved = true;

            // Exit puzzle
            PuzzleSystem.GetInstance().ExitPuzzle();
        }
        else
        {
            player.Health -= this.damage;
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗".Pastel("#d4020d"));
            Console.WriteLine("║".Pastel("#d4020d") + "                   Oedipus says no                      ".Pastel("#f72530") + "║".Pastel("#d4020d"));
            Console.WriteLine("║".Pastel("#d4020d") + "                        Incorrect                          ".Pastel("#ab262d") + "║".Pastel("#d4020d"));
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝".Pastel("#d4020d"));
            Console.WriteLine("The Sphynx is angry!".Pastel("#b30009"));
            Console.WriteLine("The Sphynx pecks you".Pastel("#ff001a"));
            Console.WriteLine($"The Sphynx deals {this.damage} damage".Pastel("#ff001a"));
            Console.WriteLine("The Sphynx repeats the question!".Pastel("#b30009"));
            Console.WriteLine($"{question}".Pastel("#FFA500"));
            Console.WriteLine("Try again. Type 'answer [your answer]' to respond.".Pastel("#6ba832"));
            failedAttempts++;

            if (failedAttempts >= 3)
            {
                this.GetHint();
            }
        }
    }

    public void GetHint()
    {
        Console.WriteLine("Think about sound and how it travels...".Pastel("#FFA500"));
    }
}