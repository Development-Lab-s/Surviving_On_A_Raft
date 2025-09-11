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

        //생성자 안에 원하는거 넣어서 받아오기 가능
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
            //공격 로직 예시 (투사체)
            // 팔 프리팹 생성
            string poolName = _skillPrefab.GetComponent<IPoolable>().ItemName;
            GolemLaser laser = PoolManager.Instance.Pop(poolName) as GolemLaser;

            if (laser != null)
            {
                laser.Initialize(Enemy.CurrentAttackDamage, Enemy.knockbackPower, _lifeTime);
                // 플레이어 방향으로 팔 회전
                if (Enemy.targetTrm != null)
                {
                    Vector3 dir = (Enemy.targetTrm.position - _firePos.position).normalized;
                    laser.transform.right = dir;
                }
            }

        }

        public override void Update()
        {
            base.Update();
            
            _timer += Time.deltaTime;

            if (_timer > _lifeTime)
            {
                AnimationEndTrigger();
                LastAttackTime = Time.time; //마지막 어택 시간 기록 (쿨타임)
            }
        }

        //스킬을 사용할 수 있는가? (로버라이딩해서 사용)
        public override bool CanUse()
        {
            //쿨타임 로직 (반드시 필요)
            if (Time.time < LastAttackTime + CoolDown) return false;

            //거리 감지
            float dist = Vector3.Distance(Enemy.transform.position, Enemy.targetTrm.position);
            if (dist > _range) return false;

            return true;
        }
    }
}