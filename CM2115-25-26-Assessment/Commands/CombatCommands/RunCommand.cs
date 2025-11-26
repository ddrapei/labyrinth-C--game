using Commands;
using Enemies;
using Observers;
using Observers.CombatObservers;
using Rooms;using Combat;


namespace Commands.CombatCommands
{
    public class RunAwayCommand : PlayerCommand
    {
        private InputManager inputManager;


        public RunAwayCommand(InputManager inputManager)
        {
            this.inputManager = inputManager;
        }

        public void Execute()
        {

            CombatSystem combatSystem = CombatSystem.GetInstance();
            CombatResult result = combatSystem.TryToRun();
        }
    }
}