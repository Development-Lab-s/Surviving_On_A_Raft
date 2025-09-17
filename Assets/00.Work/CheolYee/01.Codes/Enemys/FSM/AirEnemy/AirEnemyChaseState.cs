using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.FSM.AirEnemy
{
    public class AirEnemyChaseState : EnemyAirState
    {
        private const float BobAmplitude = 0.25f;
        private const float BobFrequency = 2.0f;
        private float _seed;

        public AirEnemyChaseState(Enemy enemy, EnemyStateMachine sm, string boolName)
            : base(enemy, sm, boolName) { }

        public override void Enter()
        {
            base.Enter(); // Animator Bool(CHASE) = true
            Enemy.MovementComponent.RbCompo.gravityScale = 0f;
            _seed = Random.value * 10f;
        }

        public override void Update()
        {
            base.Update();
            if (Enemy.isDead) return;
            float dist = (Enemy.targetTrm.position - Enemy.transform.position).magnitude;

            // 접근 + 살짝 상하
            Vector2 target = Enemy.targetTrm.transform.position;
            target.y += Mathf.Sin((Time.time + _seed) * BobFrequency) * BobAmplitude;
            MoveTowardsSmooth(target, Enemy.MovementComponent.CurrnetMoveSpeed * 1.1f);
            
            if (Enemy.lastAttackTime + Enemy.CurrentAttackSpeed < Time.time && dist < Enemy.attackRadius)
            {
                StateMachine.ChangeState(EnemyBehaviourType.Attack);
            }
        }

        private void MoveTowardsSmooth(Vector2 target, float maxSpeed)
        {
            var rb = Enemy.MovementComponent.RbCompo;
            Vector2 p = rb.position;

            Vector2 dir = target - p;
            float dist = dir.magnitude;
            if (dist < 0.01f) { rb.linearVelocity = Vector2.zero; return; }
            dir /= dist;
            float lerp = 1f - Mathf.Exp(-10f * Time.deltaTime);
            Vector2 desired = dir * maxSpeed;
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, desired, lerp);
        }
    }
}
