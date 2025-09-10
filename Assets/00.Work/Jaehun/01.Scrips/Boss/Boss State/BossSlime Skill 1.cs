using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using UnityEngine;

public class BossSlimeSkill1 : SkillState
{
    private readonly DamageCaster _caster;   // 데미지 관리코드
    private readonly float _range;    // 범위

    public BossSlimeSkill1(Enemy enemy, string animBoolName, float coolDown, DamageCaster caster, float range)
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

        // 2) 보스 전역 쿨다운(3초 락) - 보스 상태에서 관리하지만 이중 체크(안전)
        if (Enemy is BossSlime boss && !boss.IsGlobalSkillReady()) return false;

        // 사거리 체크(당연히 보스 중심 기준이겠죠?)
        if (Enemy.targetTrm == null) return false;  // 일단 위치 없으면 실행할 이유는 없지?
        float dist = Vector3.Distance(Enemy.transform.position, Enemy.targetTrm.position);
        if (dist > _range) return false;

        return true;
    }
}

