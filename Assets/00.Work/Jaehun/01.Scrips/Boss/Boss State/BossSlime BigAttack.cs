using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.Jaehun._01.Scrips.Boss;
using UnityEngine;

public class BossSlimeBigAttack : SkillState
{
    private readonly float _detectRadius;     // ���� ���� �Ÿ�
    private DamageCaster _hitCaster; // <-- ����ġ�� Ÿ�� �ڽ�/��

    //private readonly Vector2 _hitBoxSize;      // Ÿ�� �ڽ� ũ��
    //private readonly Vector2 _hitBoxOffset;    // Ÿ�� �ڽ� ������(�¿� flip �ݿ�)
    //private readonly LayerMask _playerMask;

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

    }

    // �ִϸ��̼� ����Ʈ �����ӿ��� EnemyAnimatorTrigger.AttackCast()�� ȣ���
    public override void OnAnimationCast()
    {
        if (_hitCaster == null) return;

        _hitCaster.CastDamage(Enemy.CurrentAttackDamage, Enemy.knockbackPower);
        LastAttackTime = Time.time;
        /* if (_didHit) return;

         float face = (Enemy.SpriteRendererComponent && Enemy.SpriteRendererComponent.flipX) ? -1f : 1f;
         Vector2 center = (Vector2)Enemy.transform.position + new Vector2(_hitBoxOffset.x * face, _hitBoxOffset.y);

         Collider2D[] hits = Physics2D.OverlapBoxAll(center, _hitBoxSize, 0f, _playerMask);

         foreach (var h in hits)
         {
             // ���⼭ ���� ������/�˹��� �����ϼ���.
             // DamageCaster�� �ִٸ� �װ� �ᵵ OK. (��: _caster.CastDamage(...))
         }

         _didHit = true;*/
    }
    public void SetCaster(DamageCaster caster)      // 3����� ���� ������ ĳ���� ��ü
    {
        _hitCaster = caster;
    }
}
