using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using UnityEngine;

public class BossSlimeBigAttack : SkillState
{
    private readonly float _detectRadius;     // ���� ���� �Ÿ�
    private DamageCaster _hitCaster; // <-- ����ġ�� Ÿ�� �ڽ�/��

    //private readonly Vector2 _hitBoxSize;      // Ÿ�� �ڽ� ũ��
    //private readonly Vector2 _hitBoxOffset;    // Ÿ�� �ڽ� ������(�¿� flip �ݿ�)
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

        // ��ų ���� ��� ��Ÿ��/���� ��(���� ����)
        LastAttackTime = Time.time;
        if (Enemy is BossSlime boss) boss.StartGlobalSkillLock();

        // Ȥ�� �������� �� �ִ� �ӵ� ����(�ڸ� ���� ���� ��ȭ)
        if (_rb != null)
        {
            var v = _rb.linearVelocity; v.x = 0f; v.y = 0f;
            _rb.linearVelocity = v;
        }

        Debug.Log("[BigAttack] Enter. Wait for AttackCast (slam hit).");
    }

    // �ִϸ��̼� ����Ʈ �����ӿ��� EnemyAnimatorTrigger.AttackCast()�� ȣ���
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
             // ���⼭ ���� ������/�˹��� �����ϼ���.
             // DamageCaster�� �ִٸ� �װ� �ᵵ OK. (��: _caster.CastDamage(...))
             Debug.Log("[BigAttack] HIT " + h.name);
         }

         _didHit = true;*/
    }

    public override void AnimationEndTrigger()
    {
        base.AnimationEndTrigger(); // IsCompleted=true ó��
        Debug.Log("[BigAttack] AnimationEnd");
    }
    public void SetCaster(DamageCaster caster)      // 3����� ���� ������ ĳ���� ��ü
    {
        _hitCaster = caster;
    }
}
