using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.FSM
{
    public class EnemyChaseState : EnemyGroundState
    {
        public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine, string boolName) : base(enemy, stateMachine, boolName)
        {
        }

        public override void Update()
        {
            base.Update();
            
            Vector3 dir =  Enemy.targetTrm.position - Enemy.transform.position; //방향 설정
            Enemy.MovementComponent.SetMovement(Mathf.Sign(dir.x)); //움직일 곳 설정
            
            float distance = dir.magnitude; //거리 가져와서
            //공격 사거리보가 짧고, 쿨타임이 지났으면
            if (distance < Enemy.attackRadius && Enemy.lastAttackTime + Enemy.attackCooldown < Time.time)
            {
                StateMachine.ChangeState(EnemyBehaviourType.Attack); //공격으로 설정  
            }
        }
    }
}