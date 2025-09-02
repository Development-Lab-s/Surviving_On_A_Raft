using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;

public class AirEnemyDeathState : EnemyAirState
{
    public AirEnemyDeathState(Enemy enemy, EnemyStateMachine sm, string boolName)
            : base(enemy, sm, boolName) { }

    public override void Enter()
    {
        base.Enter(); // Animator Bool(DEATH) = true
        Enemy.isDead = true;
        Enemy.MovementComponent.RbCompo.linearVelocity = Vector2.zero;
        // �ܺ� ����(���/����/Ǯ��ȯ ��)
        Enemy.onDeath?.Invoke();
        Debug.Log("Death ����");
    }
}

