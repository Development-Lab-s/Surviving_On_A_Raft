using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;

public class BossAttackState : EnemyGroundState
{
    private readonly SkillStateMachine _skillStateMachine;

    public BossAttackState(Enemy enemy, EnemyStateMachine sm, string boolName) : base(enemy, sm, boolName)
    {
        _skillStateMachine = new SkillStateMachine(enemy);
    }

    public void AddSkill(SkillType type, SkillState state) => _skillStateMachine.AddState(type, state);

    public override void Enter()
    {
        // 전역 쿨다운 중이면 공격 상태로 들어오지 않도록 즉시 반환
        if (Enemy is BossSlime boss && !boss.IsGlobalSkillReady())
        {
            Debug.Log("[BossAttackState] Global lock active -> back to Chase");
            StateMachine.ChangeState(EnemyBehaviourType.Chase);
            return;
        }

        base.Enter();
        Enemy.MovementComponent.StopImmediately();
        Debug.Log("[BossAttackState] Enter");

        // 선택 가능 스킬 중 첫 번째를 고름
        var selected = SelectSkill();
        if (selected.HasValue)
        {
            Debug.Log($"[BossAttackState] Change to skill: {selected.Value}");
            _skillStateMachine.ChangeState(selected.Value);
        }
        else
        {
            // 쓸 스킬이 없으면 바로 추격으로
            Debug.LogWarning("[BossAttackState] No usable skill -> Chase");
            StateMachine.ChangeState(EnemyBehaviourType.Chase);
        }
    }

    public override void Update()
    {
        base.Update();
        _skillStateMachine.Update();

        // 현재 스킬이 끝났으면 Idle로 복귀 + 전역 잠금 시작
        if (_skillStateMachine.CurrentState != null && _skillStateMachine.CurrentState.IsCompleted)
        {
            Enemy.lastAttackTime = Time.time; // (기존 사용처 고려)
            if (Enemy is BossSlime boss) boss.StartGlobalSkillLock();
            {
                Debug.Log("[BossAttackState] Skill complete -> Idle");
                StateMachine.ChangeState(EnemyBehaviourType.Idle);

            }

        }
    }

    public void OnAnimationCast() => _skillStateMachine.OnAnimationCast();
    public override void AnimationEndTrigger() => _skillStateMachine.OnAnimationEnd();

    private SkillType? SelectSkill()
    {
        if (Enemy is BossSlime boss && !boss.IsGlobalSkillReady())
        {
            Debug.Log("[BossAttackState] SelectSkill blocked by global lock");
            return null;
        }

        foreach (var kv in _skillStateMachine.AllStates())
        {
            var st = kv.Value;
            bool can = st.CanUse();
            Debug.Log($"[BossAttackState] CanUse {kv.Key} = {can}");
            if (can) return kv.Key;
        }
        return null;
    }
}
