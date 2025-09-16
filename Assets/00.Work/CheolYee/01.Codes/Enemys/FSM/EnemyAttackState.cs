using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.FSM
{
    public class EnemyAttackState : State
    {
        public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, string boolName) : base(enemy, stateMachine, boolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Enemy.MovementComponent.StopImmediately(); //움직임 멈추고
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