using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using UnityEngine;

public class BossSlimeSkill1 : SkillState
{
    private readonly DamageCaster _caster;   // 데미지 관리코드
    private readonly float _range;    // 범위

    //생성자 안에 원하는거 넣어서 받아오기 가능
    public BossSlimeSkill1(Enemy enemy,
        string animBoolName,
        float coolDown,
        DamageCaster caster,   // 캐스터 받기
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
        LastAttackTime = Time.time; // 쿨타임 시작
        Debug.Log(hit ? "[BossSlimeSkill1] HIT" : "[BossSlimeSkill1] MISS");
    }

    //스킬을 사용할 수 있는가? (로버라이딩해서 사용)
    public override bool CanUse()
    {
        //쿨타임 로직 (반드시 필요)
        if (Time.time < LastAttackTime + CoolDown) return false;

        // 사거리 체크(당연히 보스 중심 기준이겠죠?)
        float dist = Vector3.Distance(Enemy.transform.position, Enemy.targetTrm.position);
        if (dist > _range) return false;

        return true;
    }
}

