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

        private void AnimationEnd()
        {
            _enemy.AnimationEndTrigger();
        }

        private void AttackCast()
        {
            _enemy.Attack();
        }
    }
}