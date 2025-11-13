using Commands;
using Observers;
using Observers.CombatObservers;


namespace Commands.CombatCommands
{
    public class AttackCommand : PlayerCommand
    {
        private Game game;
        private InputManager inputManager;


        public AttackCommand(Game game, InputManager inputManager)
        {
            this.game = game;
            this.inputManager = inputManager;

        }

        public void Execute()
        {
            CombatSystem combatSystem = CombatSystem.GetInstance();
            CombatResult result = combatSystem.PlayerAttack();

            // Handle game ending if player died
            if (result.CombatOutcome == CombatOutcome.Defeat)
            {
                game.IsFinished = true;
            }
        }
    }
}