//"Patrolling" o inimigo está a patrulhar;
//"Chasing" o inimigo está a perseguir o player;
//"Attacking" o inimigo está a atacar o player;
//"Waiting" estado de espera entre waypoints de patrulha;
//"Sleeping" o inimigo foi afetado pelo efeito de dormir e não pode fazer nada.

/// <summary>
/// Estados do inimigo.
/// </summary>
public enum EnemyStates
{
    Patrolling,
    Chasing,
    Attacking,
    Waiting,
    Sleeping
}