namespace Observers.PuzzleObservers;

using Commands;
using Commands.PuzzleCommands;

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
        // Parse multi-word commands to get the base command
        string[] parts = commandString.Split(' ', 2);
        string commandKey = parts[0];
        string? argument = parts.Length > 1 ? parts[1] : null;

        if (commands.ContainsKey(commandKey))
        {
            // Special handling for AnswerRiddleCommand
            if (commands[commandKey] is AnswerRiddleCommand answerCommand && argument != null)
            {
                answerCommand.SetAnswer(argument);
            }

            commands[commandKey].Execute();
        }
    }
}