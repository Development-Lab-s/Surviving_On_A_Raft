using _00.Work.Jaehun._01.Scrips.Boss;
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

        public void SpawnPhase2Boss()             // �ƽ���?
        {
            if (_enemy is SmallSlime s) s.AnimEvent_SpawnBossAndDespawnSelf();
        }
        public void ComboFlip()               // ���� ������ �� ���� ��������� �̺�Ʈ
        {
            if (_enemy is BossSlime b) b.AnimEvent_ComboFlip();
        }

        public void GameClear()
        {
            VictoryScene.Instance.ActivateVictoryUI();
        }
    }
}