using System.Collections;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
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

        public void Initialize(float damage, float knockback, float cooltime)
        {
            _damage = damage;
            _knockback = knockback;
            _coolTime = cooltime;
            StartCoroutine(LifeTimeRoutine());
        }

        private IEnumerator LifeTimeRoutine()
        {
            yield return new WaitForSeconds(_coolTime);
            PoolManager.Instance.Push(this);
        }

        public void LaserDamageCast()
        {
            damageCaster.CastDamage(_damage, _knockback);
        }


    }
}