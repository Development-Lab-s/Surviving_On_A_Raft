using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using System.Collections.Generic;
using _00.Work.Jaehun._01.Scrips.Boss;
using UnityEngine;

public class BossAttackState : EnemyGroundState
{
    private readonly SkillStateMachine _skillStateMachine;
    private readonly HashSet<SkillType> _moveAllowed = new HashSet<SkillType>();

    private Rigidbody2D rb;
    private RigidbodyConstraints2D _originalConstraints;
    private bool _frozenApplied;
    private bool cachedFilp;
    public bool IsSkillRunning =>
    _skillStateMachine.CurrentState != null &&
    !_skillStateMachine.CurrentState.IsCompleted;

    public BossAttackState(Enemy enemy, EnemyStateMachine sm, string boolName) : base(enemy, sm, boolName)
    {
        _skillStateMachine = new SkillStateMachine(enemy);
    }

    public void SetAllowsMovement(SkillType type, bool allow = true)
    {
        if (allow) _moveAllowed.Add(type);
        else _moveAllowed.Remove(type);
    }

    public void AddSkill(SkillType type, SkillState state)
    {
        _skillStateMachine.AddState(type, state);
    }

    public override void Enter()
    {
        // ���� ��ٿ� ���̸� ���� ���·� ������ �ʵ��� ��� ��ȯ
        if (Enemy is BossSlime boss && !boss.IsGlobalSkillReady())
        {
            StateMachine.ChangeState(EnemyBehaviourType.Chase);
            return;
        }

        base.Enter();
        Enemy.lockFlip = true;
        cachedFilp = Enemy.SpriteRendererComponent.flipX;
        Enemy.SpriteRendererComponent.flipX = cachedFilp;
        Enemy.MovementComponent.StopImmediately();


        var selected = SelectSkill();
        if (!selected.HasValue)
        {
            StateMachine.ChangeState(EnemyBehaviourType.Chase);
            return;
        }

        bool allowMove = _moveAllowed.Contains(selected.Value);

        SetGroundTransitionSuppressed(allowMove);

        ApplyFreezeForSkill(allowMove);

        _skillStateMachine.ChangeState(selected.Value);
    }

    public override void Update()
    {
        base.Update();
        Enemy.SpriteRendererComponent.flipX = cachedFilp;

        _skillStateMachine.Update();

        if (_skillStateMachine.CurrentState != null)
        {
            // ��ų ���� ��Ȳ ����͸�
            var cs = _skillStateMachine.CurrentState;
        }

        // ���� ��ų�� �������� Idle�� ���� + ���� ��� ����
        if (_skillStateMachine.CurrentState != null && _skillStateMachine.CurrentState.IsCompleted)
        {
            Enemy.lastAttackTime = Time.time; // (���� ���ó ����)
            if (Enemy is BossSlime boss) boss.StartGlobalSkillLock();
            {

                RevertFreezeForSkill();
                SetGroundTransitionSuppressed(false);
                StateMachine.ChangeState(EnemyBehaviourType.Chase);

            }

        }
    }

    public override void Exit()
    {
        base.Exit();

        SetGroundTransitionSuppressed(false);

        // ������ ���󺹱� ȣ��
        RevertFreezeForSkill();
        Enemy.lockFlip = false;
    }

    public void OnAnimationTakeoff()
    {
        // ���� ��ų�� ���� Ÿ���� ���� ����
        if (_skillStateMachine.CurrentState is BossSlimeJumpAttack jump)
        {
            jump.OnTakeoffEvent();
        }
    }

    public void OnAnimationCast() => _skillStateMachine.OnAnimationCast();
    public override void AnimationEndTrigger() => _skillStateMachine.OnAnimationEnd();

    private SkillType? SelectSkill()
    {
        if (Enemy is BossSlime boss && !boss.IsGlobalSkillReady())
        {
            return null;
        }

        foreach (var kv in _skillStateMachine.AllStates())
        {
            var st = kv.Value;
            bool can = st.CanUse();
            if (can) return kv.Key;
        }
        return null;
    }

    private void ApplyFreezeForSkill(bool allowMove) // ��ų ������ x��ǥ�� �����ؼ� �������� ���ϰ� �ϱ�����. ��� ���߰� �ϴ°� �̹� ����. // �������� ��ų�� �������� �ϴ� ���� �ִ��ʿ�.
    {
        if (_frozenApplied) return;

        rb = Enemy.MovementComponent?.RbCompo;
        if (rb != null)
        {
            _originalConstraints = rb.constraints;
            // ��ġ�� ���� ����
            var cons = _originalConstraints | RigidbodyConstraints2D.FreezeRotation;
            if (!allowMove)
                cons |= RigidbodyConstraints2D.FreezePosition; // ��ġ ���� ����

            rb.constraints = cons;
            rb.angularVelocity = 0f;
        }

        // Ȥ�� �𸣴ϱ� AgentMovement ������ ���߰� �ϱ�. �ٵ� �׳� ������ �ٵ� ������Ű�°Ŷ� ���ص� �ȴٴ� �̾߱Ⱑ....
        if (Enemy.MovementComponent != null)
        {
            Enemy.MovementComponent.canMove = false;
            Enemy.MovementComponent.SetMovement(0f);
        }

        if (!allowMove)
        {
            var v = rb.linearVelocity; v.x = 0f; rb.linearVelocity = v;
        }
        _frozenApplied = true;
    }

    private void RevertFreezeForSkill()   // ���󺹱�
    {
        if (!_frozenApplied) return;

        if (rb != null)
        {
            rb.constraints = _originalConstraints;
        }

        if (Enemy.MovementComponent != null)
        {
            Enemy.MovementComponent.canMove = true;
        }

        _frozenApplied = false;
    }
}
