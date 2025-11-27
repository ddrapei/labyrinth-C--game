using Puzzles;
using PlayerLevelUp;

[Collection("Sequential")]
public class SphynxPuzzleTests
{
    [Fact]
    public void SolvePuzzle_CorrectAnswer_ReturnsTrue()
    {
        // arrange
        var puzzle = new SphynxPuzzle();        
        Player player = Player.GetInstance();
        PuzzleSystem puzzleSystem = PuzzleSystem.GetInstance();
        puzzleSystem.EnterPuzzle(puzzle);
        player.RegisterLevelUpBehavior("level up", new PlayerLevelUpBehavior());
        player.RegisterLevelUpBehavior("check level up", new PlayerCheckLevelUpBehavior());

        // act
        puzzle.CheckAnswer("echo");

        // assert
        Assert.True(puzzle.IsSolved);
    }
}        