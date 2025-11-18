using Puzzles;
using Rooms;

namespace Commands.PuzzleCommands;

public class EnterPuzzleCommand : PlayerCommand
{

    public EnterPuzzleCommand()
    {
        
    }

    public void Execute()
    {
        Player player = Player.GetInstance();
        Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);
        IPuzzle? puzzle = currentRoom.Puzzle;
        
        if (puzzle == null)
        {
            Console.WriteLine("There is no puzzle in this room.");
            return;
        }
        
        PuzzleSystem.GetInstance().EnterPuzzle(puzzle);
    }
}