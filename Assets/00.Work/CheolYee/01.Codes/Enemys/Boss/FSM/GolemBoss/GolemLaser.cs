using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.FSM.GolemBoss
{
    public class GolemLaser : MonoBehaviour, IPoolable
    {
        [SerializeField] private DamageCaster damageCaster;
        public string ItemName => gameObject.name;
        public GameObject GameObject => gameObject;
        
        public void SetMoveDirection(Vector3 dir)
        {
            // transform 회전으로 방향 맞춤 (플립 X)
            if (dir == Vector3.left)
                transform.rotation = Quaternion.Euler(0, 180, 0); // 왼쪽
            else
                transform.rotation = Quaternion.identity; // 오른쪽
        }


        public void ResetItem()
        {
            _damage = 0;
            _knockback = 0;
            _coolTime = 0;
            StopAllCoroutines();
        }

        private float _damage;
        private float _knockback;
        private float _coolTime;
        private float _timer;
        
        private SkillState _skill;
        
        public void Initialize(float damage, float knockback, float cooltime, SkillState skillState)
        {
            _damage = damage;
            _knockback = knockback;
            _coolTime = cooltime;
            _skill = skillState;
            _timer = 0;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _coolTime)
            {
                _timer = 0;
                _skill.AnimationEndTrigger();
                PoolManager.Instance.Push(this);
            }
        }

        public void LaserDamageCast()
        {
            damageCaster.CastDamage(_damage, _knockback);
        }


    }
}