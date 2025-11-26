namespace Combat;

// enum is used since CombatOutcome is not going to be changed and 
// it is more descriptive than array
public enum CombatOutcome
{
    Victory,
    Defeat,
    Escaped,
    Ongoing
}