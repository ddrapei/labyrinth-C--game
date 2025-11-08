namespace Observers;

using Commands;

// manages user's input by adding and removing observers and notifing observers added to the list
public class InputManager
{
    private List<IGameObserver> observers;

    public InputManager()
    {
        this.observers = new List<IGameObserver>();
    }

    // add observers
    public void AddObserver(IGameObserver observer)
    {
        this.observers.Add(observer);
    }

    // remove observers
    public void RemoveObserver(IGameObserver observer)
    {
        this.observers.Remove(observer);
    }
    public void NotifyObservers(string command)
    {
        foreach (IGameObserver observer in this.observers.ToList())
        {
            observer.Update(command);
        }
    }

    public void ProcessInput()
    {
        // enshures that input is trimmed and lowercase
        string command = Console.ReadLine().ToLower().Trim();
        this.NotifyObservers(command);
    }
}