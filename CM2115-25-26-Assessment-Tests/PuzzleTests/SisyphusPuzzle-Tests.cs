namespace PuzzleTests;

using Puzzles;
using PlayerLevelUp;

[Collection("Sequential")]
public class SisyphusPuzzleTests
{
    [Theory]
    [InlineData("tomorrow", true)]
    [InlineData("tomorrOW", true)]
    [InlineData("Tomorrow", true)]
    [InlineData("  ToMorrow", true)]
    [InlineData("yesterday", false)]
    [InlineData("monday", false)]
    [InlineData("dog", false)]   
   
    public void SolvePuzzle_CorrectAnswer_ReturnsTrue(string input, bool expectedOutcome)
    {
        // arrange
        var puzzle = new SisyphusPuzzle();        
        Player player = Player.GetInstance();
        PuzzleSystem puzzleSystem = PuzzleSystem.GetInstance();
        puzzleSystem.EnterPuzzle(puzzle);
        player.RegisterLevelUpBehavior("level up", new PlayerLevelUpBehavior());
        player.RegisterLevelUpBehavior("check level up", new PlayerCheckLevelUpBehavior());
        player.Health = 100;
        player.Experience = 0;


        // act
        puzzle.CheckAnswer(input);

        // assert
        Assert.Equal(puzzle.IsSolved, expectedOutcome);

        //reset
        player.Health = 100;
    }

    [Theory]
    [InlineData("tomorrow", 100)]
    [InlineData("sound", 60)]
    public void AnswerCorrectOrWrog_DamageIsDealtOrNot(string input, int expectedHealth)
    {
        // arrange
        var puzzle = new SisyphusPuzzle();        
        Player player = Player.GetInstance();
        PuzzleSystem puzzleSystem = PuzzleSystem.GetInstance();
        puzzleSystem.EnterPuzzle(puzzle);
        player.RegisterLevelUpBehavior("level up", new PlayerLevelUpBehavior());
        player.RegisterLevelUpBehavior("check level up", new PlayerCheckLevelUpBehavior());
        player.Health = 100;
        player.Experience = 0;


        // act
        puzzle.CheckAnswer(input);

        // assert
        Assert.Equal(expectedHealth, player.Health);

        //reset
        player.Health = 100;
    }    
}        