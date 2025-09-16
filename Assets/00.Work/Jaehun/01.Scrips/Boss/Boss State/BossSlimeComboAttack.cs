using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.Jaehun._01.Scrips.Boss;
using UnityEngine;

public class BossSlimeComboAttack : SkillState
{
    private DamageCaster _hitCaster;
    private Rigidbody2D _rb;

    public BossSlimeComboAttack(
        Enemy enemy,
        string animBoolName,
        float coolDown,
        DamageCaster hitCaster
    ) : base(enemy, animBoolName, coolDown)
    {
        _hitCaster = hitCaster;
    }

    public override bool CanUse()
    {
        // ��Ÿ��
        if (Time.time < LastAttackTime + CoolDown) return false;

        // ����/3������ ����: HP 50% ������ ���� ���
        // (BossSlime�� ���� ������Ƽ�� ������ �װ� �ᵵ ��. ���⼱ HP ������� üũ)
        var boss = Enemy as BossSlime;
        if (boss == null) return false;
        if (boss.HealthComponent == null) return false;
        if (boss.HealthComponent.NormalizedHealth > 0.5f) return false;

        // ���� ��(�ٸ� ��ų ���� ������)�� ���� �ִٸ� üũ
        if (!boss.IsGlobalSkillReady()) return false;

        return true;
    }

    public override void Enter()
    {
        base.Enter();

        _rb = Enemy.MovementComponent?.RbCompo;

        // �ӵ� ����(������� ���� ���ڸ� ����)
        if (_rb != null)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }

        // ���� ����: ���۰� ���ÿ� ��ٿ� + ������
        LastAttackTime = Time.time;
        (Enemy as BossSlime)?.StartGlobalSkillLock();

        Debug.Log("[Skill6 Combo] Enter. Waiting for AttackCast / Takeoff events.");
    }


    public override void OnAnimationCast()
    {
        if (_hitCaster == null) return;

        bool hit = _hitCaster.CastDamage(Enemy.CurrentAttackDamage, Enemy.knockbackPower);
        // ��ٿ� ����(���� �߿��� �����صθ� ���� ���� Ÿ�ֿ̹��� �� ����)
        LastAttackTime = Time.time;
        Debug.Log(hit ? "[Skill6 Combo] HIT" : "[Skill6 Combo] MISS");
    }

    public override void ComboFlip()
    {
        // ���� ���̻��̿� ���� ����(��/�� ������)
        var t = Enemy.transform;
        float y = Mathf.Approximately(t.eulerAngles.y, 0f) ? 180f : 0f;
        t.eulerAngles = new Vector3(0f, y, 0f);

        // �̵� �ӵ� �ܷ� ����
        if (_rb != null) _rb.linearVelocity = Vector2.zero;

        Debug.Log("[Skill6 Combo] Flip side.");
    }

    public override void AnimationEndTrigger()
    {
        base.AnimationEndTrigger(); // IsCompleted = true
        Debug.Log("[Skill6 Combo] AnimationEnd.");
    }
    public void SetCaster(DamageCaster caster)
    {
        _hitCaster = caster;
    }
}
