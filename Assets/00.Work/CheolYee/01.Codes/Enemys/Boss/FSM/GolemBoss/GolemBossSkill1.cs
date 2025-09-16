using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.FSM.GolemBoss
{
    public class GolemBossSkill1 : SkillState
    {
        private readonly GameObject _skillPrefab;
        private readonly Transform _firePos;
        private readonly float _range;
        private readonly float _lifeTime;

        private float _timer;

        public GolemBossSkill1(Enemy enemy,
            string animBoolName,
            float coolDown,
            GameObject skillPrefab,
            Transform firePos,
            float range,
            float lifeTime)
            : base(enemy, animBoolName, coolDown)
        {
            _skillPrefab = skillPrefab;
            _firePos = firePos;
            _range = range;
            _lifeTime = lifeTime;
        }

        public override void OnAnimationCast()
        {
            string poolName = _skillPrefab.GetComponent<IPoolable>().ItemName;
            GolemLaser laser = PoolManager.Instance.Pop(poolName) as GolemLaser;

            if (laser != null)
            {
                laser.transform.position = _firePos.position;

                // 플레이어 위치 기준 좌/우 방향 결정
                Vector3 moveDir = (Enemy.targetTrm.position.x < _firePos.position.x) ? Vector3.left : Vector3.right;
                laser.SetMoveDirection(moveDir);

                // 레이저 초기화
                laser.Initialize(Enemy.CurrentAttackDamage, Enemy.knockbackPower, _lifeTime);
            }

            LastAttackTime = Time.time;
        }



        public override void Update()
        {
            base.Update();

            _timer += Time.deltaTime;

            if (_timer > _lifeTime)
            {
                _timer = 0;
                Enemy.isFliping = false;
                AnimationEndTrigger();
            }
        }

        public override bool CanUse()
        {
            if (Time.time < LastAttackTime + CoolDown) return false;

            float dist = Vector3.Distance(Enemy.transform.position, Enemy.targetTrm.position);
            if (dist > _range) return false;

            return true;
        }
    }
}
