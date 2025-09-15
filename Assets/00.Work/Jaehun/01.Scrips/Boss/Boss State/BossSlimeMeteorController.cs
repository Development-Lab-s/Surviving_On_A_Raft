using _00.Work.CheolYee._01.Codes.Projectiles;
using _00.Work.CheolYee._01.Codes.SO;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class BossSlimeMeteorController : MonoBehaviour
{
    [Header("Owner & Stats")]
    [SerializeField] private BossSlime owner;              // 미지정 시 자동 탐색
    [SerializeField] private EnemyDataSo bossData;         // 미지정 시 owner.data 사용
    [SerializeField] private float meteorDamageScale = 1f; // = owner.CurrentAttackDamage * scale
    [SerializeField] private float meteorKnockback = 12f;

    [Header("Pool & Prefab")]
    [SerializeField] private GameObject meteorPrefab;      // MeteorProjectile 프리팹(풀 대상)

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;      // 5개 권장 (천장 위 등)

    [Header("Timing")]
    [SerializeField] private float cooldown = 6f;          // 한 웨이브 쿨타임
    [SerializeField] private Vector2 betweenSpawnDelay = new Vector2(1f, 3f); // 낙하 사이 대기
    [SerializeField] private Vector2Int countPerWave = new Vector2Int(1, 5);  // 웨이브당 낙하 수

    [Header("Rules")]
    [SerializeField] private bool allowWhileAttacking = true; // 다른 스킬 중에도 허용?
    [SerializeField] private bool autoStart = true;           // 켜질 때 자동 시작

    private string _poolName;
    private bool _running;

    private void Awake()
    {
        if (owner == null) owner = GetComponentInParent<BossSlime>();
        if (bossData == null && owner != null) bossData = owner.data;

        var poolable = meteorPrefab != null ? meteorPrefab.GetComponent<IPoolable>() : null;
        if (poolable == null)
        {
            Debug.LogError("[MeteorController] meteorPrefab에 IPoolable이 필요합니다.");
        }
        else
        {
            _poolName = poolable.ItemName;
        }
    }

    private void OnEnable()
    {
        if (autoStart) StartMeteor();
    }

    private void OnDisable()
    {
        StopMeteor();
    }

    public void StartMeteor()
    {
        if (_running) return;

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("[MeteorController] spawnPoints 비어있음");
            return;
        }
        if (string.IsNullOrEmpty(_poolName))
        {
            Debug.LogError("[MeteorController] 풀 이름이 비어있음 (IPoolable 미설정?)");
            return;
        }

        _running = true;
        StartCoroutine(RunLoop());
    }

    public void StopMeteor()
    {
        _running = false;
        StopAllCoroutines();
    }

    private IEnumerator RunLoop()
    {
        var waitCooldown = new WaitForSeconds(cooldown);

        while (_running)
        {
            // 동시 사용 금지라면 공격 중 회피
            if (!allowWhileAttacking && owner != null && owner.IsInAttack)
            {
                yield return null;
                continue;
            }

            int count = Random.Range(countPerWave.x, countPerWave.y + 1);

            for (int i = 0; i < count && _running; i++)
            {
                if (!allowWhileAttacking && owner != null && owner.IsInAttack)
                    break;

                SpawnOne();

                float d = Random.Range(betweenSpawnDelay.x, betweenSpawnDelay.y);
                yield return new WaitForSeconds(d);
            }

            yield return waitCooldown;
        }
    }

    private void SpawnOne()
    {
        if (spawnPoints == null || spawnPoints.Length == 0) return;

        int idx = Random.Range(0, spawnPoints.Length);
        Transform p = spawnPoints[idx];
        if (p == null)
        {
            Debug.LogWarning("[MeteorController] 빈 spawnPoint");
            return;
        }

        // 풀에서 메테오 꺼내기
        var proj = PoolManager.Instance.Pop(_poolName) as Projectile;
        if (proj == null)
        {
            Debug.LogError("[MeteorController] 풀 Pop 실패");
            return;
        }

        // 데미지 계산
        float baseDamage = (owner != null) ? owner.CurrentAttackDamage
                           : (bossData != null ? bossData.attackDamage : 10f);
        float damage = baseDamage * meteorDamageScale;

        // Initialize (방향은 무시됨, 내부에서 아래 낙하)
        proj.Initialize(p, Vector2.down, damage, meteorKnockback, 0f);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (spawnPoints == null) return;
        Gizmos.color = new Color(1f, 0.5f, 0.1f, 0.8f);
        foreach (var t in spawnPoints)
        {
            if (t == null) continue;
            Gizmos.DrawWireSphere(t.position, 0.25f);
            Gizmos.DrawLine(t.position, t.position + Vector3.down * 2f);
        }
    }
#endif
}
