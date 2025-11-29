using Commands.MainMenuCommands;
using Observers;
using Observers.MainMenuObservers;


[Collection("Sequential")]

public class StartGameTest
{

    [Fact]
    public void StartCommand_TheGameStarts_ReturnsTrueForIsRunning_ReturnsFalseForIsFinished()
    {
        // arrange
        var game = new Game();
        var InputManager = new InputManager();
        var mainMenuObserver = new MainMenuObserver(game);
        var mainMenuUnknownCommandObserver = new MainMenuUnknownCommandObserver(game);
        var gameHandlerObserver = new GameHandlerObserver(game);
        var gameCommandMoveObserver = new GameCommandMoveObserver(game);
        var inventoryObserver = new InventoryObserver(game);
        var unknownCommandObserver = new UnknownCommandObserver(game);
        var startGameCommand = new StartGameCommand(game, InputManager, mainMenuObserver, mainMenuUnknownCommandObserver, gameCommandMoveObserver, gameHandlerObserver, inventoryObserver ,unknownCommandObserver);

        // act
        startGameCommand.Execute();

        // assert
        Assert.True(game.IsRunning);
        Assert.False(game.IsFinished);
    } 
}