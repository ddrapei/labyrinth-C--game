namespace Puzzles;

using Pastel;

public class SisyphusPuzzle : IPuzzle
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

    public SisyphusPuzzle()
    {
        // question
        this.question = "I never was, am always to be, no one ever saw me, nor will ever see. And yet I am the confidence of all to live and breath in this hallowed hall.";
        this.correctAnswer = "tomorrow";
        this.isSolved = false;
        this.damage = 40;
        this.failedAttempts = 0;
    }

    public void StartPuzzle()
    {
        if (isSolved)
        {
            Console.WriteLine("The Sisyphus's puzzle has already been solved.".Pastel("#808080"));
            return;
        }

        // Display the puzzle
        Console.WriteLine("" + "╔═══════════════════════════════════════════════════════════╗".Pastel("#8B4513"));
        Console.WriteLine("║".Pastel("#8B4513") + "                Sisyphus -  blocks your path!              ".Pastel("#DAA520") + "║".Pastel("#8B4513").Pastel("#DAA520"));
        Console.WriteLine("╚═══════════════════════════════════════════════════════════╝".Pastel("#8B4513"));
        Console.WriteLine("Sisyphus speaks:".Pastel("#FFD700"));
        Console.WriteLine($"{question}".Pastel("#FFA500"));
        Console.WriteLine("Type 'answer [your answer]' to respond.".Pastel("#90EE90"));
    }

    public void CheckAnswer(string playerAnswer)
    {
        Player player = Player.GetInstance();

        if (playerAnswer.Trim().ToLower() == correctAnswer.ToLower())
        {
            int experienceGranted = player.Experience += 40;
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗".Pastel("#00FF00"));
            Console.WriteLine("║".Pastel("#00FF00") + "                       Sisyphus nods                       ".Pastel("#32CD32") + "║".Pastel("#00FF00"));
            Console.WriteLine("║".Pastel("#00FF00") + "                  Correct! You may pass.                   ".Pastel("#7FFF00") + "║".Pastel("#00FF00"));
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝".Pastel("#00FF00"));
            Console.WriteLine("Sisyphus disapperars".Pastel("#90EE90"));
            Console.WriteLine($"You gained + {experienceGranted}xp".Pastel("#90EE90"));


            isSolved = true;

            // Exit puzzle
            PuzzleSystem.GetInstance().ExitPuzzle();
        }
        else
        {
            player.Health -= this.damage;
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗".Pastel("#d4020d"));
            Console.WriteLine("║".Pastel("#d4020d") + "                     Sisyphus says No                       ".Pastel("#f72530") + "║".Pastel("#d4020d"));
            Console.WriteLine("║".Pastel("#d4020d") + "                        Incorrect                           ".Pastel("#ab262d") + "║".Pastel("#d4020d"));
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝".Pastel("#d4020d"));
            Console.WriteLine("His vision swam, blood-dark with the focus of an eternal rage.".Pastel("#b30009"));
            Console.WriteLine("With a grunt born of ages, Sisyphus drove the full, wearying weight of eternity against the impertinent mortal.".Pastel("#ff001a"));
            Console.WriteLine($"Sisyphus deals {this.damage} damage".Pastel("#ff001a"));
            Console.WriteLine("Sisyphus repeats the question!".Pastel("#b30009"));
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
        Console.WriteLine("Consider what always lies ahead, giving hope and assurance.".Pastel("#FFA500"));
    }
}