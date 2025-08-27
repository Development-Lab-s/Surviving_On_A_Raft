using _00.Work.CheolYee._01.Codes.Enemy;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.FSM
{
    public class EnemyIdleState : EnemyGroundState
    {
        
        //아이들 상태
        
        private Coroutine _delayCoroutine = null;
        
        public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string boolName) : base(enemy, stateMachine, boolName)
        {
        }

        public override void Update()
        {
            base.Update();
            
            StateMachine.ChangeState(EnemyBehaviourType.Chase); // 상태 쫒기로 변경
            /*Collider2D player = Enemy.GetPlayerInRange(); //플레이어가 감지 범위 안에 있는지 확인
            if (player != null) //있으면
            {
                Enemy.targetTrm = player.transform; //타겟 위치 설정 후
            }*/
        }

        public override void Exit()
        {
            if (_delayCoroutine != null)
                Enemy.StopCoroutine(_delayCoroutine);
            
            base.Exit();
        }
    }
}