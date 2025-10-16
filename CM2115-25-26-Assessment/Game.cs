public class Game
{
    private bool isRunning;
    private bool isFinished;

    public bool IsRunning
    {
        get { return isRunning; }
        set { isRunning = value; }
    }
    public bool IsFinished
    {
        get { return isFinished; }
        set { isFinished = value; }
    }

    public Game()
    {
        this.isRunning = false;
        this.isFinished = false;
    }
}