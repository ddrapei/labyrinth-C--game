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

    private static void InitializeStatsFile()
    {
        try
        {
            if (!File.Exists(filePath))
            {
                // Create new file with initial value of 0
                File.WriteAllText(filePath, "0");
                GamesStarted = 0;
                return;
            }

            // Read existing statistics
            string content = File.ReadAllText(filePath);
            if (int.TryParse(content.Trim(), out int gamesStarted))
            {
                GamesStarted = gamesStarted;
            }
            else
            {
                // If file is corrupted, reset it
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

    private static void SaveStats()
    {
        try
        {
            File.WriteAllText(filePath, GamesStarted.ToString());
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

        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Games Started: {GamesStarted}");
        Console.ResetColor();
        
        Console.WriteLine($"Statistics file: {filePath}");
        Console.WriteLine();
    }

    public static void ResetStats()
    {
        GamesStarted = 0;
        SaveStats();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Statistics have been reset.");
        Console.ResetColor();
    }
}