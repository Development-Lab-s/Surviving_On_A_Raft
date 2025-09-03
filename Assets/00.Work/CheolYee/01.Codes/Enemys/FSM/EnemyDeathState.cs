using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.FSM
{
    public class EnemyDeathState : State
    {
        private readonly int _deadLayer = LayerMask.NameToLayer("DeadBody");
        private bool _isDeadEffect = false;
        
        public EnemyDeathState(Enemy enemy, EnemyStateMachine stateMachine, string boolName) : base(enemy, stateMachine, boolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Enemy.gameObject.layer = _deadLayer; //레이어를 죽은 레이어로 옮기기
            Enemy.MovementComponent.StopImmediately();
            Enemy.isDead = true;
            _isDeadEffect = false;
            Enemy.CanStateChangeable = false;
        }

        public override void Update()
        {
            base.Update();
            
            if (IsEndTriggerCall && _isDeadEffect == false)
            {
                _isDeadEffect = true;
                InvokeDeadEvent();
            }
        }
        private void InvokeDeadEvent()
        {
            Enemy.onDeath?.Invoke();
             
            if (Enemy is IPoolable poolable)
            {
                PoolManager.Instance.Push(poolable);
            }
            else
            {
                Object.Destroy(Enemy.gameObject);
            }
        }
        
    }
}