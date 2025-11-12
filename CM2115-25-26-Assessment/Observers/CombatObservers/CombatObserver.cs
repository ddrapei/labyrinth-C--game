namespace Observers.CombatObservers;

using Commands;

public class CombatObserver : IGameObserver
{
    private Game game;
    private Dictionary<string, PlayerCommand> commands;

    public CombatObserver(Game game)
    {
        this.game = game;
        this.commands = new Dictionary<string, PlayerCommand>();
    }

    public void AddCommand(string commandString, PlayerCommand command)
    {
        commands[commandString] = command;
    }

    public void Update(string command)
    {
        if (commands.ContainsKey(command))
        {
            CombatSystem combatSystem = CombatSystem.GetInstance();

            commands[command].Execute();
            
            
            // closes the program when the player dies
            if (game.IsFinished)
            {
                return;
            }
        }
    }
}