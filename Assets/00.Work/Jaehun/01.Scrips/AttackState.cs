public class AttackState : IState
{
    readonly EnemyControllFSM e;
    public AttackState(EnemyControllFSM e) => this.e = e;

    public void Enter()
    {
        e.StopX(); // 제자리
        if (e.atkCooldownTimer > 0f) return; // 공격 쿨타임 상태면 기다리기

        // Attack 트리거 → 애니 중간 프레임에 AnimationEvent(함수명: AE_DealDamage)
        if (e.anim) e.anim.SetTrigger("Attack");
        else e.AE_DealDamage(); // 애니 없으면 즉시 판정

        e.atkCooldownTimer = e.data.attackCooldown;
    }

    public void Tick()
    {
        float dist = e.DistanceToPlayer();
        if (dist > e.data.attackRange)
        {
            e.ChangeState(new ChaseState(e)); // 멀어지면 추격
            return;
        }
        if (e.atkCooldownTimer <= 0f && dist <= e.data.attackRange)
        {
            e.ChangeState(new AttackState(e)); // 쿨 끝나면 재공격
        }
    }

    public void FixedTick() { e.StopX(); }
    public void Exit() { }
}
