using Commands;
using Enemies;
using Observers;
using Observers.CombatObservers;
using Rooms;
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
            if (currentRoom.Enemy != null)
            {
                CombatSystem.GetInstance().StartCombat(currentRoom.Enemy);
            }
            else
            {
                System.Console.WriteLine("There is no enemy here to fight!".Pastel("#ff7700ff"));
            }
            
        }
    }
}