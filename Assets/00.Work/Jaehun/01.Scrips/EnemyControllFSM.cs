using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // Rigidbody2D ������ �ڵ� �߰� (�׳� �ֵ� ������ġ ����.)
public class EnemyControllFSM : MonoBehaviour
{
    [Header("SO(������) & Ÿ��")]
    public EnemyData data;   // SO(��ġ/����)
    [SerializeField] private Transform player;      // ���� �÷��̾� ��ġ

    [Header("Layers (����ũ�� ���͸�)")]
    [SerializeField] private LayerMask playerMask;   // ���� ������ ���� ���̾�(=Player��)
    [SerializeField] private LayerMask groundMask;   // ���� Ȯ��(��/�÷���)
    [SerializeField] private LayerMask obstacleMask; // ���� ��ֹ�(��/��� ���)

    [Header("����")]
    public float patrolDistance = 2f; // ������ġ ���� ��/�� �պ� �Ÿ�(������)

    [Header("���� �ݰ� (��Ʈ�ڽ�)")]
    [SerializeField] private Vector2 attackPointOffset = new Vector2(0.6f, 0f); // �� ��ġ ���� ���� ������
    [SerializeField] private float attackHitRadius = 0.6f;                      // �������� �� ��Ʈ�ڽ� �ݰ� (������ �̰� ���� �� �𸣴µ� �������� �Ǵ� ������ ����>? ���߿� ��ö������ �����.)

    [Header("���̵� �� (���� �� ��")]
    [SerializeField] private bool fadeInOnSpawn = true;               // ���������� �����
    [SerializeField] private float fadeInDuration = 0.4f;
    [SerializeField] private bool freezeAIWhileFading = true;         // ���̵� �� FSM �Ͻ�����
    [SerializeField] private bool disableCollidersWhileFading = true; // ���̵� �� �浹 ����

    public Rigidbody2D rb;     // ������ �̵�
    public Animator anim;      // �ִϸ�����
    [SerializeField] private SpriteRenderer sr;  // �ð�(������/���̵�)
    [SerializeField] private float hp;           // ���� ü��
    public Vector3 startPos;   // ������(���� ����)
    public int moveDir = 1;    // +1 ������, -1 ����
    public float atkCooldownTimer;  // ���� ��Ÿ��
    public float jumpCooldownTimer; // ���� ��Ÿ��

    IState _current;       // ���� ����
    bool _isFading;        // ���̵� �� ���� �ƴ��� �˷��ִ� ����
    Collider2D[] _cols;    // ���� �ڽ��� �ݶ��̴���

    static readonly int HashIsRunning = Animator.StringToHash("IsRunning"); // ���� �ð��� ��� �װ� ����.

    void Awake()           // �������ڸ��� ���� ä���ְ�
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        _cols = GetComponentsInChildren<Collider2D>();
    }

    public void Start()
    {
        // HP�ý����� ������ ��ö�̿��� �����.

        startPos = transform.position;    // �����Ҷ� ������ ��ġ����

        if (fadeInOnSpawn && sr) StartCoroutine(FadeInRoutine());

        ChangeState(new PatrolState(this)); // ���� ����: ����
    }

    void Update()
    {
        // Ÿ�̸� ����(��ٿ��)
        if (atkCooldownTimer > 0f) atkCooldownTimer -= Time.deltaTime;
        if (jumpCooldownTimer > 0f) jumpCooldownTimer -= Time.deltaTime;

        if (_isFading && freezeAIWhileFading) return; // ���̵� ���̸� FSM �� ����
        _current?.Tick();

        // �޸��� Bool: ����ӵ� ũ�� �޸��� ��
        // if (anim) anim.SetBool(HashIsRunning, Mathf.Abs(rb.linearVelocityX.x) > 0.05f);
    }

    private void FixedUpdate()
    {
        if (_isFading && freezeAIWhileFading) return; // ���̵� ���϶��� ������ �浹 ���ֱ�
        _current?.FixedTick();
        UpdateFlipByVelocity(); // �̵� �������� ��������Ʈ ������
    }

    // ���� ��ȯ(�׻� Exit �� ��ü �� Enter ����)
    public void ChangeState(IState next)
    {
        _current?.Exit();
        _current = next;
        _current?.Enter();
    }

    public float DistanceToPlayer()
    {
        if (!player) return Mathf.Infinity;
        return Vector2.Distance(transform.position, player.position);
    }

    public float DirToPlayerX() // �÷��̾ �������̸� +1, �����̸� -1.
    {
        if (!player) return 0f;  // �÷��̾ ������ 0�� ����.
        return Mathf.Sign(player.position.x - transform.position.x); // �÷��̾� x��ġ ���� �� ��ġx �����ϸ� ���󰡱� ����?
    }
    public void MoveX(float vx) // ���� �ӵ� ����
    {
        var v = rb.linearVelocity; v.x = vx; rb.linearVelocity = v;
    }
    public void StopX()   // ���߱�
    {
        var v = rb.linearVelocity; v.x = 0f; rb.linearVelocity = v;   // ���� �ӵ� 0
    }
    void UpdateFlipByVelocity() // �ӵ� ���� �¿� ������
    {
        if (!sr) return;
        if (Mathf.Abs(rb.linearVelocity.x) > 0.01f)
            sr.flipX = rb.linearVelocity.x < 0f; // ���� �̵��̸� flipX=true
    }

    // �ٴ�üũ: �� �Ʒ��� ª�� ���̸� ���� ���� �ִ��� Ȯ��.
    public bool IsGrounded()
    {
        Vector2 origin = (Vector2)transform.position + Vector2.down * 0.05f;
        float dist = 0.1f;
        var hit = Physics2D.Raycast(origin, Vector2.down, dist, groundMask);
        // Debug.DrawRay(origin, Vector2.down * dist, Color.green);
        return hit; // hit.collider != null �̸� true
    }

    // --- ���� ��ֹ� üũ: ���� �������� ���� �� ��(����/����) ---
    public bool HasObstacleAhead()
    {
        float face = (sr && sr.flipX) ? -1f : 1f; // ��:-1, ��:+1  ���� ���⿡ ���� �ٲ�.
        Vector2 dir = new Vector2(face, 0f);

        // ���� ����: ���� ���� ���� ��ֹ�
        Vector2 low = (Vector2)transform.position + new Vector2(0.1f * face, 0.1f);
        // ���� ����: Ű ū �� ���� ��ֹ�
        Vector2 high = (Vector2)transform.position + new Vector2(0.1f * face, 0.8f);

        var hitLow = Physics2D.Raycast(low, dir, data.obstacleCheckDist, obstacleMask);
        var hitHigh = Physics2D.Raycast(high, dir, data.obstacleCheckDist, obstacleMask);

        // Debug.DrawRay(low,  dir * data.obstacleCheckDist, Color.magenta);
        // Debug.DrawRay(high, dir * data.obstacleCheckDist, Color.magenta);

        return hitLow || hitHigh;
    }
    // --- ���� ����: ���� �ӵ��� �ٲ� ���� Ƣ����� + ��ٿ� ���� ---
    public void DoJump()
    {
        var v = rb.linearVelocity;
        v.y = data.jumpForceY;
        rb.linearVelocity = v;
        jumpCooldownTimer = data.jumpCooldown;
    }
    /*public void TakeDamage(float dmg)
    {
        hp -= dmg;
        if (hp <= 0f) Die();
    �׳� �ִϸ��̼Ǹ� �ϸ� �Ǵ°� �³�?
    }*/ // ���߿� ü�� �ý��� ���� �����.

    void Die()
    {
        SpawnDrops();
        gameObject.SetActive(false);
    }
    void SpawnDrops()
    {
        // if (!data || !data.dropPrefab) return;
        int count = data.RollDropCount();
        //for (int i = 0; i < count; i++)
        //  Instantiate(data.dropPrefab, transform.position, Quaternion.identity);
    }
    // ---- ���� ����: Attack �ִ� Ŭ�� �߰� �����ӿ� "AE_DealDamage" �̺�Ʈ �߰� ----
    public void AE_DealDamage() => DealDamageNow();

    void DealDamageNow()
    {
        float face = (sr && sr.flipX) ? -1f : 1f;
        Vector2 center = (Vector2)transform.position + new Vector2(attackPointOffset.x * face, attackPointOffset.y);

        // �÷��̾� ���̾ ����
        var hit = Physics2D.OverlapCircle(center, attackHitRadius, playerMask);
        if (hit)
        {
            // hit.GetComponent<PlayerHealth>()?.TakeDamage(data.attackPower);
        }
    }

    void OnDrawGizmosSelected()
    {
        // ����/��Ÿ� ����� ��
        if (data)
        {
            Gizmos.color = Color.yellow; Gizmos.DrawWireSphere(transform.position, data.detectRadius);
            Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, data.attackRange);
        }

        // ���� ���� ����� ��
        Gizmos.color = Color.magenta;
        float face = (sr && sr.flipX) ? -1f : 1f;
        Vector2 dir = new Vector2(face, 0f);
        Vector2 low = (Vector2)transform.position + new Vector2(0.1f * face, 0.1f);
        Vector2 high = (Vector2)transform.position + new Vector2(0.1f * face, 0.8f);
        Gizmos.DrawLine(low, low + dir * (data ? data.obstacleCheckDist : 0.6f));
        Gizmos.DrawLine(high, high + dir * (data ? data.obstacleCheckDist : 0.6f));

        // ���� ��Ʈ�ڽ� ����� ��
        Gizmos.color = Color.cyan;
        Vector2 c = (Vector2)transform.position + new Vector2(attackPointOffset.x * face, attackPointOffset.y);
        Gizmos.DrawWireSphere(c, attackHitRadius);
    }

    // ---- ���� ���̵���: ���� 0��1, �浹/AI ��� OFF ----
    IEnumerator FadeInRoutine()
    {
        _isFading = true;

        if (disableCollidersWhileFading)
            foreach (var c in _cols) if (c) c.enabled = false;

        if (sr)
        {
            var color = sr.color; color.a = 0f; sr.color = color;
            float t = 0f;
            while (t < fadeInDuration)
            {
                t += Time.deltaTime;
                color.a = Mathf.Clamp01(t / fadeInDuration);
                sr.color = color;
                yield return null;
            }
        }

        if (disableCollidersWhileFading)
            foreach (var c in _cols) if (c) c.enabled = true;

        _isFading = false;
    }
}

