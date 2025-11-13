using Commands;
using Observers;
using Observers.CombatObservers;


namespace Commands.CombatCommands
{
    public class AttackCommand : PlayerCommand
    {
        private Game game;
        private InputManager inputManager;
        private IGameObserver combatObserver;
        private IGameObserver combatUnknownCommandObserver;
        private IGameObserver gameCommandMoveObserver;
        private IGameObserver gameHandlerObserver;
        private IGameObserver inventoryObserver;
        private IGameObserver unknownCommandObserver;

        public AttackCommand(Game game, InputManager inputManager, IGameObserver combatObserver, IGameObserver combatUnknownCommandObserver, IGameObserver gameCommandMoveObserver, IGameObserver gameHandlerObserver, IGameObserver inventoryObserver, IGameObserver unknownCommandObserver)
        {
            this.game = game;
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
            CombatResult result = combatSystem.PlayerAttack();

            // Handle game ending if player died
            if (result.CombatOutcome == CombatOutcome.Defeat)
            {
                game.IsFinished = true;
            }
        }
    }
}