namespace Observers.MainMenuObservers;

using Commands;

// the observer that is active before the game starts
// currently it is here only to start a game
// once the game has started, it gets removed from the observers list
public class MainMenuObserver : IGameObserver
{
    private Game game;
    private Dictionary<string, PlayerCommand> commands;

    public MainMenuObserver(Game game)
    {
        this.game = game;
        this.commands = new Dictionary<string, PlayerCommand>();
    }

    public void AddCommand(string commandString, PlayerCommand command)
    {
        commands[commandString] = command;
    }

    public void Update(string commandString)
    {
        if (commands.ContainsKey(commandString))
        {
            commands[commandString].Execute();
        }
    }
}