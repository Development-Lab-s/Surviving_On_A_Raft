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
        public void JumpTakeoff()
        {
            if (_enemy is BossSlime b) b.OnSkillTakeoff();
        }

        public void SpawnPhase2Boss()             // 아시죠?
        {
            if (_enemy is SmallSlime s) s.AnimEvent_SpawnBossAndDespawnSelf();
        }
        public void ComboFlip()               // 연속 공격할 때 쓰는 방향뒤집기 이벤트
        {
            if (_enemy is BossSlime b) b.AnimEvent_ComboFlip();
        }
    }
}