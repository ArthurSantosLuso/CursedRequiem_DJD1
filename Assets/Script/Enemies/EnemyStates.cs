// "Patrolling" the enemy is patrolling;
// "Chasing" the enemy is chasing the player;
// "Attacking" the enemy is attacking the player;
// "Waiting" waiting state between patrol waypoints;
// "Sleeping" the enemy was affected by the sleep effect and can't do anything.

/// <summary>
/// Enemy states.
/// </summary>
public enum EnemyStates
{
    Patrolling,
    Chasing,
    Attacking,
    Waiting,
    Sleeping
}
