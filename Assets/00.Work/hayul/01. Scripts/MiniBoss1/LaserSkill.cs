using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.FSM.TestBoss
{
    public class LaserSkill : SkillState
    {
        private readonly GameObject _laserPrefab;
        private readonly Transform _spawnPos;
        private readonly float _range;

        public LaserSkill(
            Enemy enemy,
            string animBoolName,
            float coolDown,
            GameObject laserPrefab,
            Transform spawnPos,
            float range
        ) : base(enemy, animBoolName, coolDown)
        {
            _laserPrefab = laserPrefab;
            _spawnPos = spawnPos;
            _range = range;
        }

        // 공격 발동 (Animation Event에서 호출됨)
        public override void OnAnimationCast()
        {
            Debug.Log("보스 팔 공격 발동!");

            // 팔 프리팹 생성
            GameObject laser = Object.Instantiate(_laserPrefab, _spawnPos.position, _spawnPos.rotation);

            // 팔 Animator 트리거 실행
            Animator laserAnim = laser.GetComponent<Animator>();
            if (laserAnim)
            {
                laserAnim.SetTrigger("ArmAttack"); // 팔 애니메이션 실행
            }

            LastAttackTime = Time.time;
        }

        public override bool CanUse()
        {
            // 쿨타임 확인
            if (Time.time < LastAttackTime + CoolDown) return false;

            // 거리 체크
            float dist = Vector3.Distance(Enemy.transform.position, Enemy.targetTrm.position);
            if (dist > _range) return false;

            return true;
        }
    }
}