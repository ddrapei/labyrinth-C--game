using Commands;
using Commands.CombatCommands;
using Observers;
using Observers.CombatObservers;
using Pastel;

namespace Commands.CombatCommands
{
    public class FightCommand : PlayerCommand
    {
        private Game game;
        private InputManager inputManager;
        private CombatObserver combatObserver;
        private CombatUnknownCommandObserver combatUnknownCommandObserver;
        private IGameObserver gameCommandMoveObserver;
        private IGameObserver gameHandlerObserver;
        private IGameObserver inventoryObserver;
        private IGameObserver unknownCommandObserver;

        public FightCommand(Game game, InputManager inputManager, CombatObserver combatObserver, CombatUnknownCommandObserver combatUnknownCommandObserver, IGameObserver gameCommandMoveObserver, IGameObserver gameHandlerObserver, IGameObserver inventoryObserver, IGameObserver unknownCommandObserver)
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
            Player player = Player.GetInstance();
            Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);

            // Store current position as previous position before combat
            player.StorePreviousPosition();

            // Remove observers from the main game
            inputManager.RemoveObserver(gameCommandMoveObserver);
            inputManager.RemoveObserver(gameHandlerObserver);
            inputManager.RemoveObserver(inventoryObserver);
            inputManager.RemoveObserver(unknownCommandObserver);

            // Add combat observers
            inputManager.AddObserver(combatObserver);
            inputManager.AddObserver(combatUnknownCommandObserver);

            // Start combat
            Enemy enemy = currentRoom.Enemy;
            CombatSystem combatSystem = CombatSystem.GetInstance();
            CombatResult result = combatSystem.StartCombat(enemy);
        }
    }
}