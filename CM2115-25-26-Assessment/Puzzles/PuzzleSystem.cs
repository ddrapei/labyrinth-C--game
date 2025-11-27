namespace Puzzles;


using Observers;
using Observers.PuzzleObservers;
using Puzzles;

// shuffles observers
public class PuzzleSystem
{
    private static PuzzleSystem? instance = null;

    public static PuzzleSystem GetInstance()
    {
        if (instance == null)
        {
            instance = new PuzzleSystem();
        }
        return instance;
    }

    private bool inPuzzle;
    private IPuzzle? currentPuzzle;
    private InputManager? inputManager;
    private IGameObserver? puzzleObserver;
    private IGameObserver? gameCommandMoveObserver;
    private IGameObserver? gameHandlerObserver;
    private IGameObserver? inventoryObserver;
    private IGameObserver? unknownCommandObserver;

    public bool InPuzzle
    {
        get { return inPuzzle; }
    }

    public IPuzzle? CurrentPuzzle
    {
        get { return currentPuzzle; }
    }

    private PuzzleSystem()
    {
        this.inPuzzle = false;
        this.currentPuzzle = null;
    }

    public void Initialize(
        InputManager inputManager,
        IGameObserver puzzleObserver,
        IGameObserver gameCommandMoveObserver,
        IGameObserver gameHandlerObserver,
        IGameObserver inventoryObserver,
        IGameObserver unknownCommandObserver)
    {
        this.inputManager = inputManager;
        this.puzzleObserver = puzzleObserver;
        this.gameCommandMoveObserver = gameCommandMoveObserver;
        this.gameHandlerObserver = gameHandlerObserver;
        this.inventoryObserver = inventoryObserver;
        this.unknownCommandObserver = unknownCommandObserver;
    }

    // start puzzle
    public void EnterPuzzle(IPuzzle puzzle)
    {
        if (inPuzzle || inputManager == null)
        {
            return;
        }

        currentPuzzle = puzzle;
        inPuzzle = true;

        // removes observers from the main game
        // checks are needed to prevent null reference warnings
        if (gameCommandMoveObserver != null)
            inputManager.RemoveObserver(gameCommandMoveObserver);
        if (gameHandlerObserver != null)
            inputManager.RemoveObserver(gameHandlerObserver);
        if (inventoryObserver != null)
            inputManager.RemoveObserver(inventoryObserver);
        if (unknownCommandObserver != null)
            inputManager.RemoveObserver(unknownCommandObserver);

        // add puzzle observers
        // checks are needed to prevent null reference warnings

        if (puzzleObserver != null)
            inputManager.AddObserver(puzzleObserver);

        // Start the puzzle
        puzzle.StartPuzzle();
    }

    // finish puzzle
    public void ExitPuzzle()
    {
        if (!inPuzzle || inputManager == null)
        {
            return;
        }

        inPuzzle = false;
        currentPuzzle = null;

        // Remove puzzle observers
        if (puzzleObserver != null)
            inputManager.RemoveObserver(puzzleObserver);


        // Restore game observers
        if (gameCommandMoveObserver != null)
            inputManager.AddObserver(gameCommandMoveObserver);
        if (gameHandlerObserver != null)
            inputManager.AddObserver(gameHandlerObserver);
        if (inventoryObserver != null)
            inputManager.AddObserver(inventoryObserver);
        if (unknownCommandObserver != null)
            inputManager.AddObserver(unknownCommandObserver);
    }
}