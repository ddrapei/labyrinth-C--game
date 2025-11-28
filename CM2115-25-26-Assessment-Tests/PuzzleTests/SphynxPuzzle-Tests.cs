namespace PuzzleTests;

using Puzzles;
using PlayerLevelUp;

[Collection("Sequential")]
public class SphynxPuzzleTests
{
    [Theory]
    [InlineData("echo", true)]
    [InlineData("Echo", true)]
    [InlineData("ECHO", true)]
    [InlineData("  ECHO", true)]
    [InlineData("sound", false)]
    [InlineData("echorium", false)]   
    public void SolvePuzzle_CorrectAnswer_ReturnsTrue(string input, bool expectedOutcome)
    {
        // arrange
        var puzzle = new SphynxPuzzle();        
        Player player = Player.GetInstance();
        PuzzleSystem puzzleSystem = PuzzleSystem.GetInstance();
        puzzleSystem.EnterPuzzle(puzzle);
        player.RegisterLevelUpBehavior("level up", new PlayerLevelUpBehavior());
        player.RegisterLevelUpBehavior("check level up", new PlayerCheckLevelUpBehavior());

        // act
        puzzle.CheckAnswer(input);

        // assert
        Assert.Equal(puzzle.IsSolved, expectedOutcome);
    }
}        