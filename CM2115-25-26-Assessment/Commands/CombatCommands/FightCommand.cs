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
        public FightCommand(Game game, InputManager inputManager)
        {
            this.game = game;
            this.inputManager = inputManager;
        }

        public void Execute()
        {
            Player player = Player.GetInstance();
            Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);
            CombatSystem.GetInstance().StartCombat(currentRoom.Enemy);        
        }
    }
}