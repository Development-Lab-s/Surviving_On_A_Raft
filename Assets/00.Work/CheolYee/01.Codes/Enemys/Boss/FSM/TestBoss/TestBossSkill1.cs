using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.CheolYee._01.Codes.Projectiles;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.FSM.TestBoss
{
    public class TestBossSkill1 : SkillState
    {
        private readonly GameObject _skillPrefab;
        private readonly Transform _firePos;
        private readonly float _range;

        //생성자 안에 원하는거 넣어서 받아오기 가능
        public TestBossSkill1(Enemy enemy, 
            string animBoolName, 
            float coolDown, 
            GameObject skillPrefab, 
            Transform firePos,
            float range) 
            : base(enemy, animBoolName, coolDown)
        {
            _skillPrefab = skillPrefab;
            _firePos = firePos;
            _range = range;
        }

        public override void OnAnimationCast()
        {
            //공격 로직 예시 (투사체)
            string poolName = _skillPrefab.GetComponent<IPoolable>().ItemName;
            Projectile projectile = PoolManager.Instance.Pop(poolName) as Projectile;
            if (projectile)
            {
                Vector2 dir = Enemy.targetTrm.position - _firePos.position;
                projectile.Initialize(_firePos, dir, Enemy.CurrentAttackDamage, 0, 10);
            }
            
            LastAttackTime = Time.time; //마지막 어택 시간 기록 (쿨타임)
        }
        
        //스킬을 사용할 수 있는가? (로버라이딩해서 사용)
        public override bool CanUse()
        {
            //쿨타임 로직 (반드시 필요)
            if (Time.time < LastAttackTime + CoolDown) return false;
            
            //거리 감지
            float dist = Vector3.Distance(Enemy.transform.position, Enemy.targetTrm.position);
            Debug.Log(dist > _range);
            if (dist > _range) return false;

            return true;
        }
    }
}