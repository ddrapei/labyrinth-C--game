namespace Commands.PuzzleCommands;

using Puzzles;
using Rooms;


public class AnswerRiddleCommand : PlayerCommand
{
    private string? answer;

    public AnswerRiddleCommand()
    {
        
    }

    public void SetAnswer(string answer)
    {
        this.answer = answer;
    }

    public void Execute()
    {
        Player player = Player.GetInstance();
        Room? currentRoom = RoomChecker.GetInstance().GetCurrentRoom(player);

        if (currentRoom == null)
        {
            Console.WriteLine("You are not in a valid room.");
            return;
        }

        if (string.IsNullOrWhiteSpace(answer))
        {
            Console.WriteLine("Usage: answer [your answer]");
            return;
        }

        currentRoom.Puzzle?.CheckAnswer(answer);
    }
}