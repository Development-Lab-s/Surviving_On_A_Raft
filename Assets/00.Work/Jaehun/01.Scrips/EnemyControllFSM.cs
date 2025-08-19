using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // Rigidbody2D 없으면 자동 추가 (그냥 애도 안전장치 느낌.)
public class EnemyControllFSM : MonoBehaviour
{
    [Header("SO(데이터) & 타겟")]
    public EnemyData data;   // SO(수치/설정)
    [SerializeField] private Transform player;      // 따라갈 플레이어 위치

    [Header("Layers (마스크로 필터링)")]
    [SerializeField] private LayerMask playerMask;   // 공격 판정에 맞을 레이어(=Player만)
    [SerializeField] private LayerMask groundMask;   // 접지 확인(땅/플랫폼)
    [SerializeField] private LayerMask obstacleMask; // 전방 장애물(벽/블록 등등)

    [Header("순찰")]
    public float patrolDistance = 2f; // 시작위치 기준 좌/우 왕복 거리(순찰용)

    [Header("공격 반경 (히트박스)")]
    [SerializeField] private Vector2 attackPointOffset = new Vector2(0.6f, 0f); // 내 위치 기준 앞쪽 오프셋
    [SerializeField] private float attackHitRadius = 0.6f;                      // 원형으로 된 히트박스 반경 (솔직히 이건 나도 잘 모르는데 원형으로 되는 이유가 뭐지>? 나중에 민철이한테 물어볼것.)

    [Header("페이드 인 (스폰 될 때")]
    [SerializeField] private bool fadeInOnSpawn = true;               // 생성중으로 만들고
    [SerializeField] private float fadeInDuration = 0.4f;
    [SerializeField] private bool freezeAIWhileFading = true;         // 페이드 중 FSM 일시정지
    [SerializeField] private bool disableCollidersWhileFading = true; // 페이드 중 충돌 끄기

    public Rigidbody2D rb;     // 물리적 이동
    public Animator anim;      // 애니메이터
    [SerializeField] private SpriteRenderer sr;  // 시각(뒤집기/페이드)
    [SerializeField] private float hp;           // 현재 체력
    public Vector3 startPos;   // 시작점(순찰 기준)
    public int moveDir = 1;    // +1 오른쪽, -1 왼쪽
    public float atkCooldownTimer;  // 공격 쿨타임
    public float jumpCooldownTimer; // 점프 쿨타임

    IState _current;       // 현재 상태
    bool _isFading;        // 페이드 중 인지 아닌지 알려주는 역할
    Collider2D[] _cols;    // 나와 자식의 콜라이더들

    static readonly int HashIsRunning = Animator.StringToHash("IsRunning"); // 엔진 시간에 배운 그거 맞음.

    void Awake()           // 시작하자마자 전부 채워주고
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        _cols = GetComponentsInChildren<Collider2D>();
    }

    public void Start()
    {
        // HP시스템은 내일쯤 민철이에게 물어보자.

        startPos = transform.position;    // 시작할때 정해진 위치에서

        if (fadeInOnSpawn && sr) StartCoroutine(FadeInRoutine());

        ChangeState(new PatrolState(this)); // 최초 상태: 순찰
    }

    void Update()
    {
        // 타이머 감소(쿨다운들)
        if (atkCooldownTimer > 0f) atkCooldownTimer -= Time.deltaTime;
        if (jumpCooldownTimer > 0f) jumpCooldownTimer -= Time.deltaTime;

        if (_isFading && freezeAIWhileFading) return; // 페이드 중이면 FSM 논리 정지
        _current?.Tick();

        // 달리기 Bool: 수평속도 크면 달리는 중
        // if (anim) anim.SetBool(HashIsRunning, Mathf.Abs(rb.linearVelocityX.x) > 0.05f);
    }

    private void FixedUpdate()
    {
        if (_isFading && freezeAIWhileFading) return; // 페이드 중일때는 물리적 충돌 없애기
        _current?.FixedTick();
        UpdateFlipByVelocity(); // 이동 방향으로 스프라이트 뒤집기
    }

    // 상태 전환(항상 Exit → 교체 → Enter 순서)
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

    public float DirToPlayerX() // 플레이어가 오른쪽이면 +1, 왼쪽이면 -1.
    {
        if (!player) return 0f;  // 플레이어가 없으면 0을 리턴.
        return Mathf.Sign(player.position.x - transform.position.x); // 플레이어 x위치 빼기 적 위치x 빼기하면 따라가기 맞지?
    }
    public void MoveX(float vx) // 수평 속도 세팅
    {
        var v = rb.linearVelocity; v.x = vx; rb.linearVelocity = v;
    }
    public void StopX()   // 멈추기
    {
        var v = rb.linearVelocity; v.x = 0f; rb.linearVelocity = v;   // 수평 속도 0
    }
    void UpdateFlipByVelocity() // 속도 기준 좌우 뒤집기
    {
        if (!sr) return;
        if (Mathf.Abs(rb.linearVelocity.x) > 0.01f)
            sr.flipX = rb.linearVelocity.x < 0f; // 왼쪽 이동이면 flipX=true
    }

    // 바닥체크: 발 아래로 짧은 레이를 쏴서 땅이 있는지 확인.
    public bool IsGrounded()
    {
        Vector2 origin = (Vector2)transform.position + Vector2.down * 0.05f;
        float dist = 0.1f;
        var hit = Physics2D.Raycast(origin, Vector2.down, dist, groundMask);
        // Debug.DrawRay(origin, Vector2.down * dist, Color.green);
        return hit; // hit.collider != null 이면 true
    }

    // --- 전방 장애물 체크: 보는 방향으로 레이 두 개(낮은/높은) ---
    public bool HasObstacleAhead()
    {
        float face = (sr && sr.flipX) ? -1f : 1f; // 왼:-1, 오:+1  보는 방향에 따라 바뀜.
        Vector2 dir = new Vector2(face, 0f);

        // 낮은 레이: 낮은 상자 같은 장애물
        Vector2 low = (Vector2)transform.position + new Vector2(0.1f * face, 0.1f);
        // 높은 레이: 키 큰 벽 같은 장애물
        Vector2 high = (Vector2)transform.position + new Vector2(0.1f * face, 0.8f);

        var hitLow = Physics2D.Raycast(low, dir, data.obstacleCheckDist, obstacleMask);
        var hitHigh = Physics2D.Raycast(high, dir, data.obstacleCheckDist, obstacleMask);

        // Debug.DrawRay(low,  dir * data.obstacleCheckDist, Color.magenta);
        // Debug.DrawRay(high, dir * data.obstacleCheckDist, Color.magenta);

        return hitLow || hitHigh;
    }
    // --- 점프 실행: 수직 속도만 바꿔 위로 튀어오름 + 쿨다운 설정 ---
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
    그냥 애니메이션만 하면 되는거 맞나?
    }*/ // 나중에 체력 시스템 보고서 만들것.

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
    // ---- 공격 판정: Attack 애니 클립 중간 프레임에 "AE_DealDamage" 이벤트 추가 ----
    public void AE_DealDamage() => DealDamageNow();

    void DealDamageNow()
    {
        float face = (sr && sr.flipX) ? -1f : 1f;
        Vector2 center = (Vector2)transform.position + new Vector2(attackPointOffset.x * face, attackPointOffset.y);

        // 플레이어 레이어만 맞춤
        var hit = Physics2D.OverlapCircle(center, attackHitRadius, playerMask);
        if (hit)
        {
            // hit.GetComponent<PlayerHealth>()?.TakeDamage(data.attackPower);
        }
    }

    void OnDrawGizmosSelected()
    {
        // 감지/사거리 디버그 원
        if (data)
        {
            Gizmos.color = Color.yellow; Gizmos.DrawWireSphere(transform.position, data.detectRadius);
            Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, data.attackRange);
        }

        // 전방 레이 디버그 선
        Gizmos.color = Color.magenta;
        float face = (sr && sr.flipX) ? -1f : 1f;
        Vector2 dir = new Vector2(face, 0f);
        Vector2 low = (Vector2)transform.position + new Vector2(0.1f * face, 0.1f);
        Vector2 high = (Vector2)transform.position + new Vector2(0.1f * face, 0.8f);
        Gizmos.DrawLine(low, low + dir * (data ? data.obstacleCheckDist : 0.6f));
        Gizmos.DrawLine(high, high + dir * (data ? data.obstacleCheckDist : 0.6f));

        // 공격 히트박스 디버그 원
        Gizmos.color = Color.cyan;
        Vector2 c = (Vector2)transform.position + new Vector2(attackPointOffset.x * face, attackPointOffset.y);
        Gizmos.DrawWireSphere(c, attackHitRadius);
    }

    // ---- 스폰 페이드인: 알파 0→1, 충돌/AI 잠깐 OFF ----
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

