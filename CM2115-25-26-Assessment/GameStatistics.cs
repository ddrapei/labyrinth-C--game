using System;
using System.IO;

// the code was reused from the previous game
public static class GameStatistics
{
    private static string filePath;

    public static string FilePath
    {
        get { return filePath; }
    }
    
    static GameStatistics()
    {
        // Looking for the statistics file in the project root directory
        filePath = Path.Combine(Directory.GetCurrentDirectory(), "GameStats.txt");
        
        InitializeStatsFile();
    }

    public static int GamesStarted { get; private set; }
    public static int Wins { get; private set; }
    public static int Deaths { get; private set; }

    private static void InitializeStatsFile()
    {
        try
        {
            if (!File.Exists(filePath))
            {
                // Create new file with initial values
                GamesStarted = 0;
                Wins = 0;
                Deaths = 0;
                SaveStats();
                return;
            }

            // Read existing statistics
            string[] lines = File.ReadAllLines(filePath);
            
            if (lines.Length >= 3)
            {
                if (int.TryParse(lines[0].Trim(), out int gamesStarted))
                    GamesStarted = gamesStarted;
                
                if (int.TryParse(lines[1].Trim(), out int wins))
                    Wins = wins;
                
                if (int.TryParse(lines[2].Trim(), out int deaths))
                    Deaths = deaths;
            }
            else
            {
                // If file format is incorrect, reset it
                ResetStats();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error loading statistics: {ex.Message}");
            Console.ResetColor();
            ResetStats();
        }
    }

    public static void AddGamesStarted()
    {
        GamesStarted++;
        SaveStats();
    }

    public static void AddWin()
    {
        Wins++;
        SaveStats();
    }
    
    public static void AddDeath()
    {
        Deaths++;
        SaveStats();
    }

    private static void SaveStats()
    {
        try
        {
            // Save all three statistics, each on a separate line
            string content = $"{GamesStarted}\n{Wins}\n{Deaths}";
            File.WriteAllText(filePath, content);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error saving statistics: {ex.Message}");
            Console.ResetColor();
        }
    }

    // Display game statistics
    public static void ShowStats()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n▒▒▒▒▒ Game Statistics ▒▒▒▒▒");

        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"■ Games Started: {GamesStarted}");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"■ Wins: {Wins}");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"■ Deaths: {Deaths}");
        Console.ResetColor();
        
        
        Console.WriteLine($"\nStatistics file: {filePath}");
        Console.WriteLine();
    }

    public static void ResetStats()
    {
        GamesStarted = 0;
        Wins = 0;
        Deaths = 0;
        SaveStats();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Statistics have been reset.");
        Console.ResetColor();
    }
}