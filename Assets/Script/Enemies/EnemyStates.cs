//"Patrolling" o inimigo est� a patrulhar;
//"Chasing" o inimigo est� a perseguir o player;
//"Attacking" o inimigo est� a atacar o player;
//"Waiting" estado de espera entre waypoints de patrulha;
//"Sleeping" o inimigo foi afetado pelo efeito de dormir e n�o pode fazer nada.

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