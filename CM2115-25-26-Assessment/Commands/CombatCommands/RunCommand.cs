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
        }
    }
}