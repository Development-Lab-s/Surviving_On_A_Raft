using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using System.Collections.Generic;
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
        Debug.Log($"[BossAttackState] MoveAllowed set -> {type} : {allow}");
    }

    public void AddSkill(SkillType type, SkillState state)
    {
        _skillStateMachine.AddState(type, state);
        Debug.Log($"[BossAttackState] AddSkill {type} ({state.GetType().Name})");
    }

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
        Enemy.lockFlip = true;
        cachedFilp = Enemy.SpriteRendererComponent.flipX;
        Enemy.SpriteRendererComponent.flipX = cachedFilp;
        Enemy.MovementComponent.StopImmediately();


        var selected = SelectSkill();
        if (!selected.HasValue)
        {
            Debug.LogWarning("[BossAttackState] No usable skill -> Chase");
            Debug.LogWarning("[BossAttackState] No usable skill (모든 CanUse=false) -> Chase");
            StateMachine.ChangeState(EnemyBehaviourType.Chase);
            return;
        }

        bool allowMove = _moveAllowed.Contains(selected.Value);
        Debug.Log($"[BossAttackState] Selected={selected.Value} allowMove={allowMove}");

        SetGroundTransitionSuppressed(allowMove);

        ApplyFreezeForSkill(allowMove);

        _skillStateMachine.ChangeState(selected.Value);
        Debug.Log($"[BossAttackState] Change to skill: {selected.Value}, allowMove={allowMove}");
    }

    public override void Update()
    {
        base.Update();
        Enemy.SpriteRendererComponent.flipX = cachedFilp;

        _skillStateMachine.Update();

        if (_skillStateMachine.CurrentState != null)
        {
            // 스킬 진행 상황 모니터링
            var cs = _skillStateMachine.CurrentState;
        }

        // 현재 스킬이 끝났으면 Idle로 복귀 + 전역 잠금 시작
        if (_skillStateMachine.CurrentState != null && _skillStateMachine.CurrentState.IsCompleted)
        {
            Enemy.lastAttackTime = Time.time; // (기존 사용처 고려)
            if (Enemy is BossSlime boss) boss.StartGlobalSkillLock();
            {
                Debug.Log("[BossAttackState] Skill complete -> Chase");

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

        // 끝날때 원상복구 호출
        RevertFreezeForSkill();
        Enemy.lockFlip = false;
    }

    public void OnAnimationTakeoff()
    {
        // 현재 스킬이 점프 타입일 때만 전달
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
            Debug.Log("[BossAttackState] SelectSkill: BLOCK by GlobalLock");
            return null;
        }

        foreach (var kv in _skillStateMachine.AllStates())
        {
            var st = kv.Value;
            bool can = st.CanUse();
            Debug.Log($"[BossAttackState] CanUse? {kv.Key} ({kv.Value.GetType().Name}) = {can}");
            if (can) return kv.Key;
        }
        return null;
    }

    private void ApplyFreezeForSkill(bool allowMove) // 스킬 쓸때는 x좌표를 고정해서 방향조절 못하게 하기위함. 사실 멈추게 하는건 이미 있음. // 점프같은 스킬을 움직여야 하니 껏다 켯다필요.
    {
        if (_frozenApplied) return;

        rb = Enemy.MovementComponent?.RbCompo;
        if (rb != null)
        {
            _originalConstraints = rb.constraints;
            // 위치나 방향 고정
            var cons = _originalConstraints | RigidbodyConstraints2D.FreezeRotation;
            if (!allowMove)
                cons |= RigidbodyConstraints2D.FreezePosition; // 위치 완전 고정

            Debug.Log($"[BossAttackState] Freeze apply: allowMove={allowMove}  before={_originalConstraints}  after={cons}");
            rb.constraints = cons;
            rb.angularVelocity = 0f;
        }

        // 혹시 모르니까 AgentMovement 에서도 멈추게 하기. 근데 그냥 리지드 바디 고정시키는거라 안해도 된다는 이야기가....
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

    private void RevertFreezeForSkill()   // 원상복구
    {
        if (!_frozenApplied) return;

        if (rb != null)
        {
            Debug.Log($"[BossAttackState] Freeze revert: restore constraints {_originalConstraints}");
            rb.constraints = _originalConstraints;
        }

        if (Enemy.MovementComponent != null)
        {
            Enemy.MovementComponent.canMove = true;
        }

        _frozenApplied = false;
    }
}
