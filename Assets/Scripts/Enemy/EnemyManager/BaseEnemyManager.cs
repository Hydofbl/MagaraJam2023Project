using UnityEngine;
using static GameManager;

public enum EnemyState
{
    Idle,
    GetHit,
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
            case EnemyState.GetHit:
                PlayHitEffect();
                break;
            case EnemyState.PreAttack:
                //
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Die:
                Room.SpawnedEnemyCount--;

                DungeonController.Instance.CurrentEnemyCount--;
                GameDataManager.Instance.AddCoin(_enemyStats.CurrentCoinAward);
                IngameUIManager.Instance.SetCoinAmount(GameDataManager.Instance.GetCoin());

                Destroy(gameObject);
                break;
            default:
                Debug.LogWarning("STATE DOES NOT EXIST.");
                break;
        }

        enemyStateTimer += Time.deltaTime;

        if (enemyStateTimer >= preAttackTime && CurrentState != EnemyState.PreAttack)
        {
            ChangeState(EnemyState.PreAttack);
        }

        if (enemyStateTimer >= attackTime)
        {
            enemyStateTimer = 0f;

            ChangeState(EnemyState.Attack);
        }
    }

    public void ChangeState(EnemyState state)
    {
        CurrentState = state;
    }

    public void GetHit(int hitDamage)
    {
        _enemyStats.CurrentHealth -= hitDamage;

        if (_enemyStats.CurrentHealth <= 0)
        {
            ChangeState(EnemyState.Die);
        }

        PreviousState = CurrentState;
        ChangeState(EnemyState.GetHit);
    }

    private void PlayHitEffect()
    {


        ChangeState(PreviousState);
    }

    protected virtual void Attack()
    {
        ChangeState(EnemyState.Idle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            GetHit(CursorManager.Instance.CursorDamage);
        }
    }
}
