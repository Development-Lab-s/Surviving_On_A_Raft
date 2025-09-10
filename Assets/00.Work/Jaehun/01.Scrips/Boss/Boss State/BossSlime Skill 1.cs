using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using UnityEngine;

public class BossSlimeSkill1 : SkillState
{
    private readonly DamageCaster _caster;   // ������ �����ڵ�
    private readonly float _range;    // ����

    //������ �ȿ� ���ϴ°� �־ �޾ƿ��� ����
    public BossSlimeSkill1(Enemy enemy,
        string animBoolName,
        float coolDown,
        DamageCaster caster,   // ĳ���� �ޱ�
        float range)
        : base(enemy, animBoolName, coolDown)
    {
        _caster = caster;
        _range = range;
    }

    public override void OnAnimationCast()
    {
        if (_caster == null) return;

        bool hit = _caster.CastDamage(Enemy.CurrentAttackDamage, Enemy.knockbackPower);
        LastAttackTime = Time.time; // ��Ÿ�� ����
        Debug.Log(hit ? "[BossSlimeSkill1] HIT" : "[BossSlimeSkill1] MISS");
    }

    //��ų�� ����� �� �ִ°�? (�ι����̵��ؼ� ���)
    public override bool CanUse()
    {
        //��Ÿ�� ���� (�ݵ�� �ʿ�)
        if (Time.time < LastAttackTime + CoolDown) return false;

        // ��Ÿ� üũ(�翬�� ���� �߽� �����̰���?)
        float dist = Vector3.Distance(Enemy.transform.position, Enemy.targetTrm.position);
        if (dist > _range) return false;

        return true;
    }
}

