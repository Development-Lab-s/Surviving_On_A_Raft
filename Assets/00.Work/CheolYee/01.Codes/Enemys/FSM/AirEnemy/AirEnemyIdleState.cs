using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.FSM.AirEnemy
{
    public class AirEnemyIdleState : EnemyAirState
    {
        private readonly Coroutine _delayCoroutine = null;

        public AirEnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string boolName) : base(enemy, stateMachine, boolName)
        {
        }

        public override void Enter()
        {
            base.Enter(); // Animator Bool(IDLE) = true 세팅됨
            Enemy.MovementComponent.RbCompo.gravityScale = 0;
        }

        public override void Update()
        {
            base.Update();
            if (Enemy.isDead) return;

            StateMachine.ChangeState(EnemyBehaviourType.Chase);
        }
        
        public override void Exit()
        {
            if (_delayCoroutine != null)
                Enemy.StopCoroutine(_delayCoroutine);
            
            base.Exit();
        }
    }
}

