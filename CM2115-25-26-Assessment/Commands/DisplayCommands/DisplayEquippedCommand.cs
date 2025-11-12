namespace Commands.DisplayCommands;

using PlayerDisplay;

// concrete command to display stats of the player
public class DisplayEquippedCommand : PlayerCommand
{
    private IPlayerDisplay equippedDisplay;

    public DisplayEquippedCommand(IPlayerDisplay equippedDisplay)
    {
        this.equippedDisplay = equippedDisplay;
    }

    public void Execute()
    {
        equippedDisplay.Display();
    }
} 