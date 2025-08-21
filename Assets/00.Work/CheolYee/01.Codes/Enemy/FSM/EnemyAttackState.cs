using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemy.FSM
{
    public class EnemyAttackState : EnemyState
    {
        public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, string boolName) : base(enemy, stateMachine, boolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Enemy.MovementComponent.StopImmediately(); //움직임 멈추고
            
            Vector2 direction = Enemy.targetTrm.position - Enemy.transform.position;
            Enemy.MovementComponent.SetMovement(direction.x);
            Enemy.Attack();
        }

        public override void Exit()
        {
            Enemy.lastAttackTime = Time.time; //마지막 공격 시간 기록
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (IsEndTriggerCall)
            {
                StateMachine.ChangeState(EnemyBehaviourType.Idle); //공격 끝나면 아이들로 변경
            }
        }
    }
}