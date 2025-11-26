namespace Combat;
public class CombatResult
{
    private CombatOutcome combatOutcome;
    private string message;

    public CombatOutcome CombatOutcome
    {
        get { return combatOutcome; }
        set { combatOutcome = value; }
    }

    public string Message
    {
        get { return message; }
        set { message = value; }
    }

    public CombatResult(CombatOutcome combatOutcome, string message = "")
    {
        this.combatOutcome = combatOutcome;
        this.message = message;
    }
}