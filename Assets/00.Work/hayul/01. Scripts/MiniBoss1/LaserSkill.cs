using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.CheolYee._01.Codes.Projectiles;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.FSM.TestBoss
{
    public class LaserSkill : SkillState
    {
        private readonly GameObject _laserPrefab;
        private readonly Transform _spawnPos;
        private readonly float _range;
        private readonly float _shotSpeed;

        public LaserSkill(
            Enemy enemy,
            string animBoolName,
            float coolDown,
            GameObject laserPrefab,
            Transform spawnPos,
            float range,
            float shotSpeed
        ) : base(enemy, animBoolName, coolDown)
        {
            _laserPrefab = laserPrefab;
            _spawnPos = spawnPos;
            _range = range;
            _shotSpeed = shotSpeed;
        }

        // 공격 발동 (Animation Event에서 호출됨)
        public override void OnAnimationCast()
        {
            Debug.Log("보스 레이저 공격 발동!");

            string poolName = _laserPrefab.GetComponent<IPoolable>().ItemName;
            Projectile laser = PoolManager.Instance.Pop(poolName) as Projectile;
            if (laser)
            {
                Vector2 dir = Enemy.targetTrm.position - _spawnPos.position;
                laser.Initialize(_spawnPos, dir, Enemy.CurrentAttackDamage, 0, _shotSpeed);
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