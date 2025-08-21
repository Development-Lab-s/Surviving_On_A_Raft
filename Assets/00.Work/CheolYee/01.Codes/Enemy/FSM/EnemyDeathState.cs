using _00._Work._02._Scripts.Manager.Pooling;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemy.FSM
{
    public class EnemyDeathState : EnemyState
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
            /*Debug.Log("<color=red>Boom!</color>");
            const string deadEffectName = "ZombieExplosion";
            EffectPlayer effect = PoolManager.Instance.Pop(deadEffectName) as EffectPlayer;
            Debug.Assert(effect != null, "effect != null");
            effect.SetPositionAndPlay(_enemy.transform.position);*/
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