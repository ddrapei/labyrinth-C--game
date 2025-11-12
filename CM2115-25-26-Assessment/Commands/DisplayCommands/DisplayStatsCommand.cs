namespace Commands.DisplayCommands;

using PlayerDisplay;

// concrete command to display stats of the player
public class DisplayStatsCommand : PlayerCommand
{
    private IPlayerDisplay statsDisplay;

    public DisplayStatsCommand(IPlayerDisplay statsDisplay)
    {
        this.statsDisplay = statsDisplay;
    }

    public void Execute()
    {
        statsDisplay.Display();
    }
}