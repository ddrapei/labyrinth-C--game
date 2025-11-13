using Commands;
using Observers;
using Observers.CombatObservers;

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