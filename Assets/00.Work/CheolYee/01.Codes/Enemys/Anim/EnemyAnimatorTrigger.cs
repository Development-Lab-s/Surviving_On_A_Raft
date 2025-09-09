using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Anim
{
    public class EnemyAnimatorTrigger : MonoBehaviour
    {
        private Enemy _enemy;

        public void Initialize(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void AnimationEnd()
        {
            _enemy.AnimationEndTrigger();
        }

        public void AttackCast()
        {
            _enemy.Attack();
        }
        public void SpawnPhase2Boss()
        {
            if (_enemy is SmallSlime s) s.AnimEvent_SpawnBossAndDespawnSelf();
        }
    }
}