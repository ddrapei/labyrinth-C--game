namespace PuzzleTests;


using Puzzles;
using PlayerLevelUp;

[Collection("Sequential")]
public class OedipusPuzzleTests
{
    [Theory]
    [InlineData("man", true)]
    [InlineData("MAn", true)]
    [InlineData("MAN", true)]
    [InlineData("  maN", true)]
    [InlineData("men", false)]
    [InlineData("masdf", false)]   
    public void SolvePuzzle_CorrectAnswer_ReturnsTrue(string input, bool expectedOutcome)
    {
        // arrange
        var puzzle = new OedipusPuzzle();        
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
        player.Experience = 0;
    }


    [Theory]
    [InlineData("man", 100)]
    [InlineData("monkey", 70)]
    public void AnswerCorrectOrWrog_DamageIsDealtOrNot(string input, int expectedHealth)
    {
        // arrange
        var puzzle = new OedipusPuzzle();        
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
        player.Experience = 0;

    }    
}        