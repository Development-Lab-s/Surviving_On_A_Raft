public class AttackState : IState
{
    readonly EnemyControllFSM e;
    public AttackState(EnemyControllFSM e) => this.e = e;

    public void Enter()
    {
        e.StopX(); // ���ڸ�
        if (e.atkCooldownTimer > 0f) return; // ���� ��Ÿ�� ���¸� ��ٸ���

        // Attack Ʈ���� �� �ִ� �߰� �����ӿ� AnimationEvent(�Լ���: AE_DealDamage)
        if (e.anim) e.anim.SetTrigger("Attack");
        else e.AE_DealDamage(); // �ִ� ������ ��� ����

        e.atkCooldownTimer = e.data.attackCooldown;
    }

    public void Tick()
    {
        float dist = e.DistanceToPlayer();
        if (dist > e.data.attackRange)
        {
            e.ChangeState(new ChaseState(e)); // �־����� �߰�
            return;
        }
        if (e.atkCooldownTimer <= 0f && dist <= e.data.attackRange)
        {
            e.ChangeState(new AttackState(e)); // �� ������ �����
        }
    }

    public void FixedTick() { e.StopX(); }
    public void Exit() { }
}
