namespace Commands.MainMenuCommands;

// Command to display game statistics from the main menu
public class ShowGameStatsCommand : PlayerCommand
{
    public ShowGameStatsCommand()
    {
    }

    public void Execute()
    {
        GameStatistics.ShowStats();
    }
}