namespace Commands;

public class LookAroundCommand : PlayerCommand
{
    public void Execute()
    {
        Player.GetInstance().LookAround();
    }
}