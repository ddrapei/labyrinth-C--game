namespace Observers.PuzzleObservers;

using Commands;

// this observer handles unknown command input for puzzles
public class UnknownCommandPuzzleObserver : IGameObserver
{
    private Game game;
    private Dictionary<string, PlayerCommand> commands;

    public UnknownCommandPuzzleObserver(Game game)
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