using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using UnityEngine;

public class BossSlimeBigAttack : SkillState
{
    private readonly float _detectRadius;     // 시전 조건 거리
    private DamageCaster _hitCaster; // <-- 내려치기 타격 박스/원

    //private readonly Vector2 _hitBoxSize;      // 타격 박스 크기
    //private readonly Vector2 _hitBoxOffset;    // 타격 박스 오프셋(좌우 flip 반영)
    //private readonly LayerMask _playerMask;

    private bool _didHit;
    private Rigidbody2D _rb;

    public BossSlimeBigAttack(
        Enemy enemy, string animBoolName, float coolDown,
        float detectRadius, DamageCaster hitCaster
        )
        : base(enemy, animBoolName, coolDown)
    {
        _detectRadius = detectRadius;
        _hitCaster = hitCaster;
        // _hitBoxSize = hitBoxSize;
        //_hitBoxOffset = hitBoxOffset;
        //_playerMask = enemy.whatIsPlayer.layerMask;
    }

    public override bool CanUse()
    {
        if (Time.time < LastAttackTime + CoolDown) return false;
        if (Enemy is BossSlime b && !b.IsGlobalSkillReady()) return false;
        if (Enemy.targetTrm == null) return false;

        float dist = Vector2.Distance(Enemy.transform.position, Enemy.targetTrm.position);
        return dist <= _detectRadius;
    }

    public override void Enter()
    {
        base.Enter();
        _didHit = false;
        _rb = Enemy.MovementComponent?.RbCompo;

        // 스킬 시작 즉시 쿨타임/전역 락(스팸 방지)
        LastAttackTime = Time.time;
        if (Enemy is BossSlime boss) boss.StartGlobalSkillLock();

        // 혹시 남아있을 수 있는 속도 제거(자리 고정 느낌 강화)
        if (_rb != null)
        {
            var v = _rb.linearVelocity; v.x = 0f; v.y = 0f;
            _rb.linearVelocity = v;
        }

        Debug.Log("[BigAttack] Enter. Wait for AttackCast (slam hit).");
    }

    // 애니메이션 임팩트 프레임에서 EnemyAnimatorTrigger.AttackCast()가 호출됨
    public override void OnAnimationCast()
    {
        if (_hitCaster == null) return;

        bool hit = _hitCaster.CastDamage(Enemy.CurrentAttackDamage, Enemy.knockbackPower);
        LastAttackTime = Time.time;
        Debug.Log(hit ? "[Skill4] BIG SLAM HIT" : "[Skill4] BIG SLAM MISS");
        /* if (_didHit) return;

         float face = (Enemy.SpriteRendererComponent && Enemy.SpriteRendererComponent.flipX) ? -1f : 1f;
         Vector2 center = (Vector2)Enemy.transform.position + new Vector2(_hitBoxOffset.x * face, _hitBoxOffset.y);

         Collider2D[] hits = Physics2D.OverlapBoxAll(center, _hitBoxSize, 0f, _playerMask);
         Debug.Log($"[BigAttack] HitBox center={center} size={_hitBoxSize} hit={hits.Length}");

         foreach (var h in hits)
         {
             // 여기서 실제 데미지/넉백을 적용하세요.
             // DamageCaster가 있다면 그걸 써도 OK. (예: _caster.CastDamage(...))
             Debug.Log("[BigAttack] HIT " + h.name);
         }

         _didHit = true;*/
    }

    public override void AnimationEndTrigger()
    {
        base.AnimationEndTrigger(); // IsCompleted=true 처리
        Debug.Log("[BigAttack] AnimationEnd");
    }
    public void SetCaster(DamageCaster caster)      // 3페이즈를 위한 데미지 캐스터 교체
    {
        _hitCaster = caster;
    }
}
