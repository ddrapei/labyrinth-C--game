namespace Observers;
using Commands;
public class GameCommandMoveObserver : IGameObserver
{
    private Game game;
    private Dictionary<string, PlayerCommand> commands;

    public GameCommandMoveObserver(Game game)
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
        if (game.IsRunning && commands.ContainsKey(commandString))
        {
            this.commands[commandString].Execute();
        }
    }
}