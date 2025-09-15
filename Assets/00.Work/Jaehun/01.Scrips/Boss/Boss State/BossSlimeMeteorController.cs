using _00.Work.CheolYee._01.Codes.Projectiles;
using _00.Work.CheolYee._01.Codes.SO;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using System.Collections;
using _00.Work.Jaehun._01.Scrips.Boss;
using UnityEngine;

[DisallowMultipleComponent]
public class BossSlimeMeteorController : MonoBehaviour
{
    [Header("Owner & Stats")]
    [SerializeField] private BossSlime owner;              // ������ �� �ڵ� Ž��
    [SerializeField] private EnemyDataSo bossData;         // ������ �� owner.data ���
    [SerializeField] private float meteorDamageScale = 1f; // = owner.CurrentAttackDamage * scale
    [SerializeField] private float meteorKnockback = 12f;

    [Header("Pool & Prefab")]
    [SerializeField] private GameObject meteorPrefab;      // MeteorProjectile ������(Ǯ ���)

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;      // 5�� ���� (õ�� �� ��)

    [Header("Timing")]
    [SerializeField] private float cooldown = 6f;          // �� ���̺� ��Ÿ��
    [SerializeField] private Vector2 betweenSpawnDelay = new Vector2(1f, 3f); // ���� ���� ���
    [SerializeField] private Vector2Int countPerWave = new Vector2Int(1, 5);  // ���̺�� ���� ��

    [Header("Rules")]
    [SerializeField] private bool allowWhileAttacking = true; // �ٸ� ��ų �߿��� ���?
    [SerializeField] private bool autoStart = true;           // ���� �� �ڵ� ����

    private string _poolName;
    private bool _running;

    private void Awake()
    {
        if (owner == null) owner = GetComponentInParent<BossSlime>();
        if (bossData == null && owner != null) bossData = owner.data;

        var poolable = meteorPrefab != null ? meteorPrefab.GetComponent<IPoolable>() : null;
        if (poolable == null)
        {
            Debug.LogError("[MeteorController] meteorPrefab�� IPoolable�� �ʿ��մϴ�.");
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
            Debug.LogWarning("[MeteorController] spawnPoints �������");
            return;
        }
        if (string.IsNullOrEmpty(_poolName))
        {
            Debug.LogError("[MeteorController] Ǯ �̸��� ������� (IPoolable �̼���?)");
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
            // ���� ��� ������� ���� �� ȸ��
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
            Debug.LogWarning("[MeteorController] �� spawnPoint");
            return;
        }

        // Ǯ���� ���׿� ������
        var proj = PoolManager.Instance.Pop(_poolName) as Projectile;
        if (proj == null)
        {
            Debug.LogError("[MeteorController] Ǯ Pop ����");
            return;
        }

        // ������ ���
        float baseDamage = (owner != null) ? owner.CurrentAttackDamage
                           : (bossData != null ? bossData.attackDamage : 10f);
        float damage = baseDamage * meteorDamageScale;

        // Initialize (������ ���õ�, ���ο��� �Ʒ� ����)
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
