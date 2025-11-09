namespace Commands;

// concrete command to display stats of the player
public class DisplayStatsCommand : PlayerCommand
{
    private IPlayerStatsDisplay statsDisplay;

    public DisplayStatsCommand(IPlayerStatsDisplay statsDisplay)
    {
        this.statsDisplay = statsDisplay;
    }

    public void Execute()
    {
        statsDisplay.Display();
    }
}