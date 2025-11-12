using Commands;
using Observers;
using Observers.CombatObservers;

namespace Commands.CombatCommands
{
    public class RunAwayCommand : PlayerCommand
    {
        private InputManager inputManager;
        private IGameObserver gameCommandMoveObserver;
        private IGameObserver gameHandlerObserver;
        private IGameObserver inventoryObserver;
        private IGameObserver unknownCommandObserver;
        private IGameObserver combatObserver;
        private IGameObserver combatUnknownCommandObserver;

        public RunAwayCommand(InputManager inputManager, IGameObserver combatObserver, IGameObserver combatUnknownCommandObserver, IGameObserver gameCommandMoveObserver, IGameObserver gameHandlerObserver, IGameObserver inventoryObserver,IGameObserver unknownCommandObserver)
        {
            this.inputManager = inputManager;
            this.combatObserver = combatObserver;
            this.combatUnknownCommandObserver = combatUnknownCommandObserver;
            this.gameCommandMoveObserver = gameCommandMoveObserver;
            this.gameHandlerObserver = gameHandlerObserver;
            this.inventoryObserver = inventoryObserver;
            this.unknownCommandObserver = unknownCommandObserver;
        }

        public void Execute()
        {
            CombatSystem combatSystem = CombatSystem.GetInstance();
            CombatResult result = combatSystem.TryToRun();

            // If player successfully escaped, exit combat mode
            if (result.CombatOutcome == CombatOutcome.Escaped)
            {
                // Remove combat observers
                inputManager.RemoveObserver(combatObserver);
                inputManager.RemoveObserver(combatUnknownCommandObserver);

                // Restore normal game observers
                inputManager.AddObserver(gameCommandMoveObserver);
                inputManager.AddObserver(gameHandlerObserver);
                inputManager.AddObserver(inventoryObserver);
                inputManager.AddObserver(unknownCommandObserver);

                combatSystem.EndCombat();
            }
            // If player failed to escape or died, continue in combat or handle death
            else if (result.CombatOutcome == CombatOutcome.Defeat)
            {
                // Remove combat observers
                inputManager.RemoveObserver(combatObserver);
                inputManager.RemoveObserver(combatUnknownCommandObserver);

                combatSystem.EndCombat();
            }
        }
    }
}