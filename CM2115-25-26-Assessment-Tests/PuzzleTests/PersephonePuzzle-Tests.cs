namespace PuzzleTests;

using Puzzles;
using PlayerLevelUp;

[Collection("Sequential")]
public class PersephonePuzzleTests
{
    [Theory]
    [InlineData("coffin", true)]
    [InlineData("Coffin", true)]
    [InlineData("coffIN", true)]
    [InlineData("  cOFfin", true)]
    [InlineData("pen", false)]
    [InlineData("paper", false)]
    [InlineData("dOG", false)]   
   
    public void SolvePuzzle_CorrectAnswer_ReturnsTrue(string input, bool expectedOutcome)
    {
        // arrange
        var puzzle = new PersephonePuzzle();        
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