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
        // ���� ��ٿ� ���̸� ���� ���·� ������ �ʵ��� ��� ��ȯ
        if (Enemy is BossSlime boss && !boss.IsGlobalSkillReady())
        {
            Debug.Log("[BossAttackState] Global lock active -> back to Chase");
            StateMachine.ChangeState(EnemyBehaviourType.Chase);
            return;
        }

        base.Enter();
        Enemy.MovementComponent.StopImmediately();
        Debug.Log("[BossAttackState] Enter");

        // ���� ���� ��ų �� ù ��°�� ��
        var selected = SelectSkill();
        if (selected.HasValue)
        {
            Debug.Log($"[BossAttackState] Change to skill: {selected.Value}");
            _skillStateMachine.ChangeState(selected.Value);
        }
        else
        {
            // �� ��ų�� ������ �ٷ� �߰�����
            Debug.LogWarning("[BossAttackState] No usable skill -> Chase");
            StateMachine.ChangeState(EnemyBehaviourType.Chase);
        }
    }

    public override void Update()
    {
        base.Update();
        _skillStateMachine.Update();

        // ���� ��ų�� �������� Idle�� ���� + ���� ��� ����
        if (_skillStateMachine.CurrentState != null && _skillStateMachine.CurrentState.IsCompleted)
        {
            Enemy.lastAttackTime = Time.time; // (���� ���ó ���)
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
