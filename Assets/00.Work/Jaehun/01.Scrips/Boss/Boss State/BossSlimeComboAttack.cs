using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
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
        // 쿨타임
        if (Time.time < LastAttackTime + CoolDown) return false;

        // 보스/3페이즈 제한: HP 50% 이하일 때만 허용
        // (BossSlime에 공개 프로퍼티가 있으면 그걸 써도 됨. 여기선 HP 기반으로 체크)
        var boss = Enemy as BossSlime;
        if (boss == null) return false;
        if (boss.HealthComponent == null) return false;
        if (boss.HealthComponent.NormalizedHealth > 0.5f) return false;

        // 전역 락(다른 스킬 직후 딜레이)도 쓰고 있다면 체크
        if (!boss.IsGlobalSkillReady()) return false;

        return true;
    }

    public override void Enter()
    {
        base.Enter();

        _rb = Enemy.MovementComponent?.RbCompo;

        // 속도 정지(연속찍기 동안 제자리 느낌)
        if (_rb != null)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }

        // 스팸 방지: 시작과 동시에 쿨다운 + 전역락
        LastAttackTime = Time.time;
        (Enemy as BossSlime)?.StartGlobalSkillLock();

        Debug.Log("[Skill6 Combo] Enter. Waiting for AttackCast / Takeoff events.");
    }


    public override void OnAnimationCast()
    {
        if (_hitCaster == null) return;

        bool hit = _hitCaster.CastDamage(Enemy.CurrentAttackDamage, Enemy.knockbackPower);
        // 쿨다운 갱신(연속 중에도 갱신해두면 다음 선택 타이밍에서 더 안전)
        LastAttackTime = Time.time;
        Debug.Log(hit ? "[Skill6 Combo] HIT" : "[Skill6 Combo] MISS");
    }

    public override void ComboFlip()
    {
        // 공격 사이사이에 방향 반전(좌/우 번갈아)
        var t = Enemy.transform;
        float y = Mathf.Approximately(t.eulerAngles.y, 0f) ? 180f : 0f;
        t.eulerAngles = new Vector3(0f, y, 0f);

        // 이동 속도 잔류 방지
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
