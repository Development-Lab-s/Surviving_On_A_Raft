using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.Jaehun._01.Scrips.Boss;
using UnityEngine;

public class BossSlimeSkill1 : SkillState
{
    private DamageCaster _hitCaster;   // ������ �����ڵ�
    private readonly float _range;    // ����

    private Rigidbody2D rb;
    private RigidbodyConstraints2D _orginalConstraints;
    private bool _FrozenApplied;

    public BossSlimeSkill1(Enemy enemy, string animBoolName, float coolDown, DamageCaster hitCaster, float range)
         : base(enemy, animBoolName, coolDown)
    {
        _hitCaster = hitCaster;
        _range = range;
    }

    public override void OnAnimationCast()
    {
        if (_hitCaster == null) return;

        bool hit = _hitCaster.CastDamage(Enemy.CurrentAttackDamage, Enemy.knockbackPower);
        LastAttackTime = Time.time; // ��Ÿ�� ����
    }

    //��ų�� ����� �� �ִ°�? (�ι����̵��ؼ� ���)
    public override bool CanUse()
    {
        //��Ÿ�� ���� (�ݵ�� �ʿ�)
        if (Time.time < LastAttackTime + CoolDown) return false;

        // 2) ���� ���� ��ٿ�(3�� ��) - ���� ���¿��� ���������� ���� üũ(����)
        if (Enemy is BossSlime boss && !boss.IsGlobalSkillReady()) return false;

        // ��Ÿ� üũ(�翬�� ���� �߽� �����̰���?)
        if (Enemy.targetTrm == null) return false;  // �ϴ� ��ġ ������ ������ ������ ����?
        float dist = Vector3.Distance(Enemy.transform.position, Enemy.targetTrm.position);
        if (dist > _range) return false;

        return true;
    }
    public void SetCaster(DamageCaster caster)
    {
        _hitCaster = caster;
    }
}

