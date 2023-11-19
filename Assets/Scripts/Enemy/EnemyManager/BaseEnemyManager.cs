using UnityEngine;

public enum EnemyState
{
    Idle,
    PreAttack,
    Attack,
    Die
}

public class BaseEnemyManager : MonoBehaviour
{
    public Room Room;

    [Header("Enemy State")]
    // Store the current state of the game
    public EnemyState CurrentState;
    // Store the previous state of the game
    public EnemyState PreviousState;

    [SerializeField] 
    [Range(0f, 15f)]
    private float attackTime = 3f;
    [SerializeField]
    [Range(0f, 15f)]
    private float preAttackTime = 1.5f;
    private float enemyStateTimer = 0f;

    private EnemyStats _enemyStats;

    private void Start()
    {
        _enemyStats = GetComponent<EnemyStats>();
    }

    void Update()
    {
        switch (CurrentState)
        {
            case EnemyState.Idle:

                break;
            case EnemyState.PreAttack:
                //
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Die:
                Room.SpawnedEnemyCount--;
                Destroy(gameObject);
                break;
            default:
                Debug.LogWarning("STATE DOES NOT EXIST.");
                break;
        }

        enemyStateTimer += Time.deltaTime;

        if(enemyStateTimer >= preAttackTime && CurrentState != EnemyState.PreAttack)
        {
            ChangeState(EnemyState.PreAttack);
        }

        if(enemyStateTimer >= attackTime)
        {
            enemyStateTimer = 0f;

            ChangeState(EnemyState.Attack);
        }
    }

    public void ChangeState(EnemyState state)
    {
        CurrentState = state;
    }

    public void GetHit(float hitDamage)
    {
        _enemyStats.CurrentHealth -= hitDamage;

        if (_enemyStats.CurrentHealth <= 0)
        {
            _enemyStats.CurrentHealth = 0;
            ChangeState(EnemyState.Die);
        }
    }

    protected virtual void Attack()
    {
        ChangeState(EnemyState.Idle);
    }
}
