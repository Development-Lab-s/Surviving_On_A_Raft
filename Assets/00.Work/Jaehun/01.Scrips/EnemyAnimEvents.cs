using _00.Work.CheolYee._01.Codes.Enemys;
using UnityEngine;

namespace _00.Work.Jaehun._01.Scrips
{
    public class EnemyAnimEvents : MonoBehaviour
    {
        private Enemy _enemy;

        void Awake()
        {
            _enemy = GetComponentInParent<AirEnemy>();
        }

        public void Attack()
        {
            if (_enemy != null) _enemy.Attack();
        }

        public void AnimationEndTrigger()
        {
            if (_enemy != null) _enemy.AnimationEndTrigger();
        }
    }
}
