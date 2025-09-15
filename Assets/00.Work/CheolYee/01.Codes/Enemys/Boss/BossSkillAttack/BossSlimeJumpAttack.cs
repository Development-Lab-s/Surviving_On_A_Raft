using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using UnityEngine;

public class BossSlimeJumpAttack : SkillState
{
    private readonly float _detectRadius;
    private readonly float _slamRadius;
    private readonly float _jumpPower;
    private readonly float _forwardPower;

    private Rigidbody2D _rb;
    private bool _tookOff; // 중복 점프 방지
    private bool _slamDone;

    private DamageCaster _hitCaster;

    public BossSlimeJumpAttack(Enemy enemy, string animBoolName, float coolDown, float detectRadius, DamageCaster hitCaster, float jumpPower, float forwardPower)
        : base(enemy, animBoolName, coolDown)
    {
        _detectRadius = detectRadius;
        _hitCaster = hitCaster;
        _jumpPower = jumpPower;
        _forwardPower = forwardPower;
    }

    public override bool CanUse()
    {
        float now = Time.time;

        if (now < LastAttackTime + CoolDown)
        {
            Debug.Log($"[Skill3.CanUse] BLOCK cooldown  now={now:F2}  ready={LastAttackTime + CoolDown:F2}");
            return false;
        }

        if (Enemy is BossSlime b && !b.IsGlobalSkillReady())
        {
            Debug.Log("[Skill3.CanUse] BLOCK global lock");
            return false;
        }

        if (Enemy.targetTrm == null)
        {
            Debug.Log("[Skill3.CanUse] BLOCK targetTrm is null");
            return false;
        }

        float dist = Vector2.Distance(Enemy.transform.position, Enemy.targetTrm.position);
        bool ok = dist <= _detectRadius;
        Debug.Log($"[Skill3.CanUse] dist={dist:F2}  detect={_detectRadius:F2}  -> {ok}");
        return ok;
    }

    public override void Enter()
    {
        base.Enter();
        _rb = Enemy.MovementComponent?.RbCompo;
        _tookOff = false;
        LastAttackTime = Time.time;


        /*_takeoffDone = false;

        _slamDone = false;

        if (_rb == null || Enemy.targetTrm == null)
        {
            Debug.Log("이러면 안움직여요");

            return;
        }

        if (Enemy.MovementComponent != null)
            Enemy.MovementComponent.enabled = false;

        if (Enemy is BossSlime boss) boss.StartGlobalSkillLock();
        Debug.Log("[Skill3.Enter] Waiting for takeoff event...");*/
    }

    public void OnTakeoffEvent()
    {
        if (_tookOff) return;
        if (!_tookOff)
        {
            // XY 이동 가능 보장(혹시 남아있을 프리즈 해제)
            var c0 = _rb.constraints;
            var c1 = (c0 | RigidbodyConstraints2D.FreezeRotation);
            c1 &= ~RigidbodyConstraints2D.FreezePositionX;
            c1 &= ~RigidbodyConstraints2D.FreezePositionY;
            if (c1 != c0) _rb.constraints = c1;

            // 포물선 이륙 속도 계산
            Vector2 p0 = Enemy.transform.position;
            Vector2 pt = Enemy.targetTrm.position;
            float dx = pt.x - p0.x;
            float dir;
            if (Mathf.Abs(dx) < 0.1f)
            {
                bool flipped = Enemy.SpriteRendererComponent != null && Enemy.SpriteRendererComponent.flipX;
                dir = flipped ? -1f : 1f;
                dx = dir * 0.1f;
            }
            else dir = Mathf.Sign(dx);

            float g = Mathf.Abs(Physics2D.gravity.y * _rb.gravityScale);
            float extraH = Mathf.Max(0.5f, _jumpPower);
            float apexY = Mathf.Max(p0.y, pt.y) + extraH;

            float vy0 = Mathf.Sqrt(Mathf.Max(0.0001f, 2f * g * (apexY - p0.y)));
            float tUp = vy0 / g;
            float tDown = Mathf.Sqrt(Mathf.Max(0.0001f, 2f * (apexY - pt.y) / g));
            float T = tUp + tDown;

            float vx = dx / Mathf.Max(0.0001f, T);
            float minVX = 3f;
            float maxVX = Mathf.Max(minVX + 0.1f, _forwardPower);
            vx = Mathf.Sign(vx) * Mathf.Clamp(Mathf.Abs(vx), minVX, maxVX);

            _rb.linearVelocity = new Vector2(vx, vy0);
            _tookOff = true;
            Debug.Log($"[Skill3] Takeoff: vx={vx:F2}, vy={vy0:F2}");
            return;
        }
        /*if (_rb == null || Enemy.targetTrm == null) return;

        Vector2 p0 = Enemy.transform.position;
        Vector2 pt = Enemy.targetTrm.position;
        float dx = pt.x - p0.x;

        if (Mathf.Abs(dx) < 0.1f)
            dx = (Enemy.SpriteRendererComponent && Enemy.SpriteRendererComponent.flipX) ? -0.1f : 0.1f;

        float g = Mathf.Abs(Physics2D.gravity.y * _rb.gravityScale);
        float vy0 = Mathf.Sqrt(Mathf.Max(0.0001f, 2f * g * (_jumpPower))); // 여유 높이를 _jumpPower로 사용
        float tUp = vy0 / g;
        float tDn = Mathf.Sqrt(Mathf.Max(0.0001f, 2f * (_jumpPower + Mathf.Max(0f, p0.y - pt.y)) / g));
        float T = tUp + tDn;

        float vx = dx / Mathf.Max(0.0001f, T);
        float minVX = 3f;
        float maxVX = Mathf.Max(minVX + 0.1f, _forwardPower);
        vx = Mathf.Sign(vx) * Mathf.Clamp(Mathf.Abs(vx), minVX, maxVX);

        _rb.linearVelocity = new Vector2(vx, vy0);

        _tookOff = true;
        LastAttackTime = Time.time; // 쿨타임 시작은 도약 시점
        Debug.Log($"[Skill3.Takeoff] vx={vx:F2}, vy={vy0:F2}, cdStart={LastAttackTime:F2}");*/
    }

    public override void OnAnimationCast()
    {
        if (_hitCaster == null) return;

        bool hit = _hitCaster.CastDamage(Enemy.CurrentAttackDamage, Enemy.knockbackPower);
        Debug.Log(hit ? "[Skill3] SLAM HIT" : "[Skill3] SLAM MISS");
        /* if (_rb == null || Enemy.targetTrm == null) return;

         if (!_takeoffDone)
         {
             // XY 이동 가능 보장(혹시 남아있을 프리즈 해제)
             var c0 = _rb.constraints;
             var c1 = (c0 | RigidbodyConstraints2D.FreezeRotation);
             c1 &= ~RigidbodyConstraints2D.FreezePositionX;
             c1 &= ~RigidbodyConstraints2D.FreezePositionY;
             if (c1 != c0) _rb.constraints = c1;

             // 포물선 이륙 속도 계산
             Vector2 p0 = Enemy.transform.position;
             Vector2 pt = Enemy.targetTrm.position;
             float dx = pt.x - p0.x;
             float dir;
             if (Mathf.Abs(dx) < 0.1f)
             {
                 bool flipped = Enemy.SpriteRendererComponent != null && Enemy.SpriteRendererComponent.flipX;
                 dir = flipped ? -1f : 1f;
                 dx = dir * 0.1f;
             }
             else dir = Mathf.Sign(dx);

             float g = Mathf.Abs(Physics2D.gravity.y * _rb.gravityScale);
             float extraH = Mathf.Max(0.5f, _jumpPower);
             float apexY = Mathf.Max(p0.y, pt.y) + extraH;

             float vy0 = Mathf.Sqrt(Mathf.Max(0.0001f, 2f * g * (apexY - p0.y)));
             float tUp = vy0 / g;
             float tDown = Mathf.Sqrt(Mathf.Max(0.0001f, 2f * (apexY - pt.y) / g));
             float T = tUp + tDown;

             float vx = dx / Mathf.Max(0.0001f, T);
             float minVX = 3f;
             float maxVX = Mathf.Max(minVX + 0.1f, _forwardPower);
             vx = Mathf.Sign(vx) * Mathf.Clamp(Mathf.Abs(vx), minVX, maxVX);

             _rb.linearVelocity = new Vector2(vx, vy0);
             _takeoffDone = true;
             Debug.Log($"[Skill3] Takeoff: vx={vx:F2}, vy={vy0:F2}");
             return;
         }

         // 2) 두 번째 AttackCast: 착지 히트
         if (!_slamDone)
         {
             // Overlap으로 플레이어 히트 체크
             Collider2D[] hits = Physics2D.OverlapCircleAll(
                 Enemy.transform.position, _slamRadius, Enemy.whatIsPlayer.layerMask);
             foreach (var h in hits)
             {
                 // 여기에 실제 데미지/넉백 적용 루틴 호출 (DamageCaster 있으면 활용)
                 Debug.Log("[Skill3] Slam hit: " + h.name);
             }

             _slamDone = true;
             Debug.Log("[Skill3] Slam damage applied.");
         }*/
    }

    public override void AnimationEndTrigger()
    {
        base.AnimationEndTrigger();

        if (Enemy.MovementComponent != null)
            Enemy.MovementComponent.enabled = true;



        Debug.Log("[BossSlimeJumpAttack] Animation ended");
    }
    public void SetCaster(DamageCaster caster)
    {
        _hitCaster = caster;
    }


#if UNITY_EDITOR
    public void DrawGizmos()
    {
        if (Enemy == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(Enemy.transform.position, _detectRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Enemy.transform.position, _slamRadius);
    }
#endif
}
